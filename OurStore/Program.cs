using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OurStoreContext>(options => options.UseSqlServer("Server=SRV2\\PUPILS;Database=OurStore;Trusted_Connection=True;TrustServerCertificate=True"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
