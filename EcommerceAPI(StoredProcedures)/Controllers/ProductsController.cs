using CoreWebapi.Models;
using EcommerceAPI_StoredProcedures_.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebapi.Controllers
{
    [Route("api/")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productServiceStoredProcedure;
        public ProductsController(IProductService productServiceStoredProcedure)
        {
            _productServiceStoredProcedure = productServiceStoredProcedure;
        }

        ///////////////////////////////////////////// GET //////////////////////////////////////////
        [HttpGet("Products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productServiceStoredProcedure.GetProducts();
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
            var products = await _productServiceStoredProcedure.GetProducts(ProductId);
            if (products == null)
            {
                return NotFound($"Product{ProductId} not exist");
            }
            return this.Ok(products);
        }

        ///////////////////////////////////////////// POST ////////////////////////////////////////
        [HttpPost("Create Product")]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                int result = await _productServiceStoredProcedure.CreateProduct(product);
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
                var dbProduct = await _productServiceStoredProcedure.GetProducts(id);
                if (dbProduct == null)
                {
                    return this.NotFound($"Product Id {id} not found ...!");
                }
                product.ProductId = id;
                int result = await _productServiceStoredProcedure.UpdateProduct(product);
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
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var dbProduct = await _productServiceStoredProcedure.GetProducts(id);
                if (dbProduct == null)
                {
                    return this.NotFound($"Product id {id} not founds ...!");
                }
                int result = await _productServiceStoredProcedure.DeleteProduct(id);
                if (result != null)
                {
                    return this.Ok("Product Deleted Sucessfully ...!");
                }

                else
                {
                    return this.BadRequest("Error While Deleting The Product ...! ");
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

    }
}
