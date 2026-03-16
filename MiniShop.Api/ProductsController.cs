using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniShop.Api.Contracts.Product;
using MiniShop.Application.Interfaces;
using MiniShop.Domain.Entities;

namespace MiniShop.Api
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;
        public ProductsController(IProductService productService,
                ILogger<ProductsController> logger)
        {
            _logger = logger;
            _productService = productService;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var products = await _productService.GetAllAsync();
            return Ok( products);

        }

        [Authorize(Roles = "Customer")]
        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(string? search)
        {
            var products = await _productService.GetProducts(search);
            return Ok(products);

        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductRequest request)
        {

            var productId = await _productService.CreateProductAsync(request.Name, request.Price, request.Quantity);

            _logger.LogInformation("product with {productId} saved.", productId);

            return Ok(new { ProductId = productId });

        }



    }
}
