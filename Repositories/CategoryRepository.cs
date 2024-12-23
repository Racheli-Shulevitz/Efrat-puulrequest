using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories;
public class CategoryRepository : ICategoryRepository
{
    OurStoreContext _OurStoreContext;
    public CategoryRepository(OurStoreContext ourStoreContext)
    {
        _OurStoreContext = ourStoreContext;
    }
    public async Task<List<Category>> getAll()
    {
        return await _OurStoreContext.Categories.ToListAsync();
    }
}
