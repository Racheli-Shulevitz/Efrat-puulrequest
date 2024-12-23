using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class CategoryService : ICategoryService
{
    ICategoryRepository CategoryRepository;
    public CategoryService(ICategoryRepository _CategoryRepository)
    {
        CategoryRepository = _CategoryRepository;
    }
    public async Task<List<Category>> getAll()
    {
        return await CategoryRepository.getAll();
    }
}
