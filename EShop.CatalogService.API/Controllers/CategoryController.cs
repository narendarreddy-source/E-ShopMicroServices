using EShop.CatalogService.Application.Dtos.Request;
using EShop.CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoriesService;

        public CategoryController(ICategoryService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            var categories = await _categoriesService.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] AddCategoryDto addCategoryDto, CancellationToken cancellationToken)
        {
            var category = await _categoriesService.AddCategoryAsync(addCategoryDto, cancellationToken);
            return Ok(category);
        }
    }
}
