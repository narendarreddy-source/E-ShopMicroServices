using EShop.CatalogService.Application.Dtos.Request;
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
        private ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<GetProductsDto>>> GetProducts(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all products");
            var products = await _productService.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("GetProductsById/{id}")]
        public async Task<ActionResult<GetProductsDto>> GetProductbyId(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetProductsById{id}");

            var product = await _productService.GetProductByIdAsync(id, cancellationToken);

            if (product == null) 
                return NotFound();

            return Ok(product);
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult<GetProductsDto>> AddProduct([FromBody]AddProductDto product, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"AddProduct");
          
            var addedProduct = await _productService.AddProductAsync(product, cancellationToken);
            return CreatedAtAction(nameof(GetProductbyId), new { id = addedProduct.Id }, addedProduct);
        }
        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<GetProductsDto>> UpdateProduct([FromBody]UpdateProductDto product, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateProduct");
            var updatedProduct = await _productService.UpdateProductAsync(product, cancellationToken);

            if (updatedProduct == null)
                return NotFound();

            return Ok(updatedProduct);
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<ActionResult> DeleteProduct(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DeleteProduct{id}");
            var deleted = await _productService.DeleteProductAsync(id, cancellationToken);

            if (!deleted)
                return NotFound();
            return Ok();
        }
    }
}
