using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OurStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductsService productsService;
        IMapper mapper;

        public ProductsController(IProductsService _productsService, IMapper _mapper)
        {
            productsService = _productsService;
            mapper = _mapper;
        }
        // GET: api/<ProductsController>
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> Get([FromQuery] string? desc, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] int?[] categoryIds)
        {
            List<Product> productList = await productsService.GetProducts(desc, minPrice,maxPrice,categoryIds);
            List<ProductDTO> productryDTOList = mapper.Map<List<Product>,List<ProductDTO>>(productList);
            return productryDTOList != null ? Ok(productryDTOList) : BadRequest();
        }
    }
}
