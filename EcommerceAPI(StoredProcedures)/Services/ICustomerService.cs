using EcommerceAPI_StoredProcedures_.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI_StoredProcedures_.Services
{
    public interface ICustomerService
    {
        public Task<List<Customers>> GetCustomers();
        public Task<Customers> GetCustomerById(int CustomerId);
        public Task<int> CreateCustomer(Customers customer);
        public Task<int> UpdateCustomer(Customers customer);
        public Task<int> DeleteCustomer(int CustomerId);
    }
}
