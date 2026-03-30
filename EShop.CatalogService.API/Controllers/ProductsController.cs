using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShop.CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("GetProducts")]
        public string GetProducts()
        {
            return "This is the products controller";
        }

        [HttpGet("GetProductsById/{id}")]
        public string GetProductbyId(int id) { 
            return $"This is the product with id {id}";
        }

    }
}
