using EShop.CatalogService.Application.Dtos.Response;
using EShop.CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService) { 
            _productService = productService;
        }


        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<GetProductsDto>>> GetProducts(CancellationToken cancellationToken)
        {
           var products = await  _productService.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("GetProductsById/{id}")]
        public string GetProductbyId(int id) { 
            return $"This is the product with id {id}";
        }

    }
}
