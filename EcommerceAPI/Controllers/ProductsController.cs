using CoreWebapi.Models;
using CoreWebapi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace CoreWebapi.Controllers
{
    [Route("api/")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        ///////////////////////////////////////////// GET //////////////////////////////////////////
        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            if (products.Count == 0)
            {
                return NotFound("Products do not exist");
            }

            return this.Ok(products);
        }

        ///////////////////////////////////////////// GET BY ID/////////////////////////////////////
        [HttpGet("Product/{ProductId}")]
        public async Task<IActionResult> GetProducts(int ProductId)
        {
            var products = await _productService.GetProducts(ProductId);
            if (products == null)
            {
                return NotFound($"Product{ProductId} not exist");
            }


            return this.Ok(products);
        }

        ///////////////////////////////////////////// POST ////////////////////////////////////////
        [HttpPost("Create Product")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                int result = await _productService.CreateProduct(product);
                if (result > 0)
                    return this.Ok("Product Sucessfully Added");
                else
                    return this.BadRequest("Error Adding New Product");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }

        }

        ///////////////////////////////////////////// UPDATE BY ID /////////////////////////////////
        [HttpPut("Update Product/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            try
            {
                var dbProduct = await _productService.GetProducts(id);
                if (dbProduct == null)
                {
                    return this.NotFound($"Product Id {id} not found ...!");
                }
                product.ProductId = id;
                int result = await _productService.UpdateProduct(product);
                if (result > 0)
                    return this.Ok("Product Updated Sucessfully...!");
                else
                    return this.BadRequest("Error While Updating The Product ...!");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        ///////////////////////////////////////////// DELETE /////////////////////////////////
        [HttpDelete("Delete Product/{id}")]
        public async Task<IActionResult> DeleteProduct (int id)
        {
            try
            {
                var dbProduct = await _productService.GetProducts(id);
                if (dbProduct == null)
                {
                    return this.NotFound($"Product id {id} not founds ...!");
                }
                int result = await _productService.DeleteProduct(id);
                if (result > 0)
                    return this.Ok("Product Deleted Sucessfully ...!");
                else
                    return this.BadRequest("Error While Updating The Product ...! ");
            }
            catch(Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

    }
}
