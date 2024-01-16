using EcommerceAPI_StoredProcedures_.Models;
using EcommerceAPI_StoredProcedures_.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI_StoredProcedures_.Controllers
{
    [Route("api")]

    public class CustomersController : ControllerBase
    {

        private readonly ICustomerService  _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        ///////////////////////////////////////////// GET //////////////////////////////////////////
        [HttpGet("Customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomers();
            if (customers.Count == 0)
            {
                return NotFound("Customers do not exist");
            }
            return this.Ok(customers);
        }

        ///////////////////////////////////////////// GET By Id ////////////////////////////////////
        [HttpGet("Customers/{CustomerId}")]
        public async Task<IActionResult> GetCustomerById(int CustomerId)
        {
            var customers = await _customerService.GetCustomerById(CustomerId);
            if (customers == null)
            {
                return NotFound($"Customer {CustomerId} do not exist");
            }
            return this.Ok(customers);
        }

        ///////////////////////////////////////////// CREATE ////////////////////////////////////
        [HttpPost("Create Customers")]
        public async Task<IActionResult> CreateCustomer( [FromBody] Customers customer)
        {
            try
            {
                int result = await _customerService.CreateCustomer(customer);
                if (result > 0)
                    return this.Ok("Customer Sucesfully Added ...!");
                else
                    return this.BadRequest("Error While Adding Customer...!");
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex.Message);
            }
        }

        ///////////////////////////////////////////// UPDATE ////////////////////////////////////
        [HttpPut("Update Customer")]
        public async Task<IActionResult> UpdateCustomer (int Id, [FromBody] Customers customer)
        {
            try
            {
                var dbCustomer = await _customerService.GetCustomerById(Id);
                if (dbCustomer == null)
                {
                    return this.BadRequest($"Customer {Id} not found ...!");
                }

                customer.CustomerId = Id;
                int result = await _customerService.UpdateCustomer(customer);
                if (result > 0)
                    return this.Ok("Customer Updated Sucessfully ...!");
                else
                    return this.BadRequest("Error While Updating Customer ...!");
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        ///////////////////////////////////////////// DELETE ////////////////////////////////////
        [HttpDelete("Delete Customer")]

        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            try
            {
                var dbCustomer = await _customerService.GetCustomerById(Id);
                if (dbCustomer == null)
                {
                    return this.BadRequest($"Customer {Id} not founds...!");
                }

                int result = await _customerService.DeleteCustomer(Id);
                if (result > 0)
                    return this.Ok("Customer Deleted Sucessfully ...!");
                else
                    return this.BadRequest("Error While Deleting Customer...!");
            }
            catch(Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
