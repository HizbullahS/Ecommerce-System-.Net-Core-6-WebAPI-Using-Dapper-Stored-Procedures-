using EcommerceAPI_StoredProcedures_.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI_StoredProcedures_.Controllers
{
    [Route("api/")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _orderService;
        public OrdersController(IOrdersService ordersService)
        {
            _orderService = ordersService;
        }

        ///////////////////////////////////////////// GET //////////////////////////////////////////
        [HttpGet("Get All Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrders();
            if (orders == null)
                return NotFound("Orders do not exist");
            else 
             return Ok(orders); 
        }

        ///////////////////////////////////////////// GET BY ID ////////////////////////////////////
        [HttpGet("Get Order By Id")]
        public async Task<IActionResult> GetOrderById(int OrderId)
        {
            var result = await _orderService.GetOrderById(OrderId);
            if (result == null)
            {
                return NotFound($"Order Id {OrderId} do not exist");
            }
            else
            {
                return Ok(result);
            }
        }

        ///////////////////////////////////////////// POST ///////////////////////////////////////
        [HttpPost("Create New Order")]
        public async Task<IActionResult> CreateOrder( [FromBody] Orders order)
        {
            int result = await _orderService.CreateOrder(order);
            if(result > 0)
                return Ok("Order Added Sucessfully ...!");
            else
                return BadRequest("Error While Adding Order ...!");
        }

        ///////////////////////////////////////////// UPDATE ///////////////////////////////////////
        [HttpPut("Update Order")]
        public async Task<IActionResult> UpdateOrder(int OrderId, [FromBody] Orders order)
        {
            var dbOrder = await _orderService.GetOrderById(OrderId);
            if (dbOrder == null)
            {
                return BadRequest($"Order Id {OrderId} not found ...!");
            }
            order.OrderId = OrderId;
            int result = await _orderService.UpdateOrder(order);
                if (result > 0)
                return Ok("Order Updated Sucessfully ...!");
            else
                return BadRequest("Error While Updating Order ...!");
        }

        ///////////////////////////////////////////// DELETE ///////////////////////////////////////
        [HttpDelete("Delete Order")]
        public async Task<IActionResult> DeleteOrder(int OrderId)
        {
            var dbOrders = await _orderService.GetOrderById(OrderId);
            if (dbOrders == null)
                return BadRequest($"Order Id {OrderId} not found ...!");
            int result = await _orderService.DeleteOrder(OrderId);
            if (result > 0) return Ok("Order Deleted Sucessfully...!");
            else return BadRequest("Error While Deleting Order...!");
        }
    }
}
