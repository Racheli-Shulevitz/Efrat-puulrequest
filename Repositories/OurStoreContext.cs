using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entities;

namespace Repositories;

public partial class OurStoreContext : DbContext
{
    public OurStoreContext()
    {
    }

    public OurStoreContext(DbContextOptions<OurStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SRV2\\PUPILS;Database=OurStore;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(12);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
