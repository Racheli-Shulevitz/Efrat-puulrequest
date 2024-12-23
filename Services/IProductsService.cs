using Entities;

namespace Services
{
    public interface IProductsService
    {
        Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}