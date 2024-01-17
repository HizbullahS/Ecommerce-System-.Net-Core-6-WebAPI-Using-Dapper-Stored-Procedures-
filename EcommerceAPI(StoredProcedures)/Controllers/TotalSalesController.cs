using EcommerceAPI_StoredProcedures_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI_StoredProcedures_.Controllers
{
    [Route("api/")]
    public class TotalSalesController : ControllerBase
    {
        private readonly ITotalSalesService _totalSalesService;
        public TotalSalesController(ITotalSalesService totalSalesService)
        {
            _totalSalesService = totalSalesService;
        }

        ///////////////////////////////////////////// GET //////////////////////////////////////////
        [HttpGet("Get All Sales")]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _totalSalesService.GetAllSales();
            if (sales == null)
                return NotFound("Sales do not exist");
            else
                return Ok(sales);
        }

        ///////////////////////////////////////////// GET BY ID ////////////////////////////////////
        [HttpGet("Get Sale By Id")]
        public async Task<IActionResult> GetSaleById(int SaleId)
        {
            var result = await _totalSalesService.GetSaleById(SaleId);
            if (result == null)
            {
                return NotFound($"Sale Id {SaleId} do not exist");
            }
            else
            {
                return Ok(result);
            }
        }

        ///////////////////////////////////////////// POST ///////////////////////////////////////
        [HttpPost("Create New Sale")]
        public async Task<IActionResult> CreateSale([FromBody] TotalSales sales)
        {
            int result = await _totalSalesService.CreateSale(sales);
            if (result > 0)
                return Ok("Sale Added Sucessfully ...!");
            else
                return BadRequest("Error While Adding Sale ...!");
        }

        ///////////////////////////////////////////// UPDATE ///////////////////////////////////////
        [HttpPut("Update Sale")]
        public async Task<IActionResult> UpdateSale(int SaleId, [FromBody] TotalSales sales)
        {
            var dbSale = await _totalSalesService.GetSaleById(SaleId);
            if (dbSale == null)
            {
                return BadRequest($"Sale Id {SaleId} not found ...!");
            }
            sales.SaleId = SaleId;
            int result = await _totalSalesService.UpdateSale(sales);
            if (result > 0)
                return Ok("Sale Updated Sucessfully ...!");
            else
                return BadRequest("Error While Updating Sale ...!");
        }

        ///////////////////////////////////////////// DELETE ///////////////////////////////////////
        [HttpDelete("Delete Sale")]
        public async Task<IActionResult> DeleteSale(int SaleId)
        {
            var dbSales = await _totalSalesService.GetSaleById(SaleId);
            if (dbSales == null)
                return BadRequest($"Sale Id {SaleId} not found ...!");
            int result = await _totalSalesService.DeleteSale(SaleId);
            if (result > 0) return Ok("Sale Deleted Sucessfully...!");
            else return BadRequest("Error While Deleting Sale...!");
        }
    }
}
