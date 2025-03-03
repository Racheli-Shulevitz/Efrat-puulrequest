using Entities;

namespace Services
{
    public interface IProductsService
    {
        Task<Product> GetProductById(int id);
        Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}