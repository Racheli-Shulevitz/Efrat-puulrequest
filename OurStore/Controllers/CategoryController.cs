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
    public class CategoryController : ControllerBase
    {
        ICategoryService categoryService;
        IMapper mapper;
        public CategoryController(ICategoryService _categoryService,IMapper _mapper)
        {
            categoryService = _categoryService;
            mapper = _mapper;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            List<Category> categoryList = await categoryService.getAll();
            List<CategoryDTO> categoryDTOList = mapper.Map<List<Category>, List<CategoryDTO>>(categoryList);
            return categoryDTOList != null ? Ok(categoryDTOList) : BadRequest();
        }

    }
}
