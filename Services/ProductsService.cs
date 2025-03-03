using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services;

public class ProductsService:IProductsService
{
    IProductsRepository productsRepository;

    public ProductsService(IProductsRepository _productRepository)
    {
        productsRepository = _productRepository;
    }

    public async Task<Product> GetProductById(int id)
    {
        return await productsRepository.GetProductById(id);
    }
        public async Task<List<Product>> GetProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
    {
        return await productsRepository.GetProducts(desc,minPrice, maxPrice, categoryIds);
    }
}
