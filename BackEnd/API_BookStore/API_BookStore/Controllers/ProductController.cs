using API_BookStore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_BookStore.Services;
using API_BookStore.DTOs;

namespace API_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        public ProductController(IProduct product)
        {
            _productService = product;
        }

        [HttpGet("get-product")]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _productService.GetAllProductsAsync();
            if (products == null)
            {
                return Ok(
                    new
                    {
                        StatusCode = -1,
                        Message = "No products found"
                    });
            }
            return Ok(
                new
                {
                    StatusCode = 1,
                    Message = products
                });
        }

        [HttpGet("get-product-by-id/{id}")]
        public async Task<IActionResult> GetProductById(string produtCode)
        {
            var product = await _productService.GetProductByCodeAsync(produtCode);
            if (product == null)
            {
                return Ok(
                    new
                    {
                        StatusCode = -1,
                        Message = "Product not found"
                    });
            }
            return Ok(
                new
                {
                    StatusCode = 1,
                    Message = product
                });
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO productModel)
        {
            var result = await _productService.CreateProduct(productModel);
            if (!result)
            {
                return Ok(
                    new
                    {
                        StatusCode = -1,
                        Message = "Failed to add product"
                    });
            }
            return Ok(
                new
                {
                    StatusCode = 1,
                    Message = "Product added successfully"
                });
        }


        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO productModel)
        {
            var result = await _productService.UpdateProduct(productModel);
            if (!result)
            {
                return Ok(
                    new
                    {
                        StatusCode = -1,
                        Message = "Failed to update product"
                    });
            }
            return Ok(
                new
                {
                    StatusCode = 1,
                    Message = "Product updated successfully"
                });
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(string produtCode)
        {
            var result = await _productService.DeleteProduct(produtCode);
            if (!result)
            {
                return Ok(
                    new
                    {
                        StatusCode = -1,
                        Message = "Failed to delete product"
                    });
            }
            return Ok(
                new
                {
                    StatusCode = 1,
                    Message = "Product deleted successfully"
                });
        }
    }
}
