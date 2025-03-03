using Entities;

namespace Repositories
{
    public interface IProductsRepository
    {
        Task<Product> GetProductById(int id);
        
      Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}