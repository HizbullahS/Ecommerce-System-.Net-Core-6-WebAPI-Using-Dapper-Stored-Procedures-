using CoreWebapi.Services;
using Dapper;
using EcommerceAPI_StoredProcedures_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace EcommerceAPI_StoredProcedures_.Services
{
    public class CustomerService : ICustomerService
    {
        ///////////////////////////////////////////// GET //////////////////////////////////////////
        public async Task<List<Customers>> GetCustomers()
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var customers = await connection.QueryAsync<Customers>("SP_GetAllCustomers", null, commandType: CommandType.StoredProcedure);
                return customers.ToList();
            }
        }

        ///////////////////////////////////////////// GET By Id ////////////////////////////////////
        public async Task<Customers> GetCustomerById(int CustomerId)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", CustomerId);
                var customers = await connection.QueryAsync<Customers>("SP_GetCustomerById", parameters, commandType: CommandType.StoredProcedure);
                return customers.FirstOrDefault();
            }
        }

        ///////////////////////////////////////////// POST /////////////////////////////////////////
        public async Task<int> CreateCustomer(Customers customer)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FirstName", customer.FirstName);
                parameters.Add("@LastName", customer.LastName);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@Phone", customer.Phone);
                parameters.Add("@Address", customer.Address);
                parameters.Add("@City", customer.City);
                parameters.Add("@State", customer.State);
                parameters.Add("@PostalCode", customer.PostalCode);

                var result = await connection.ExecuteAsync("SP_CreateCustomer", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        ///////////////////////////////////////////// UPDATE BY ID /////////////////////////////////
        public async Task<int> UpdateCustomer(Customers customer)
        {
         using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", customer.CustomerId);
                parameters.Add("@FirstName", customer.FirstName);
                parameters.Add("@LastName", customer.LastName);
                parameters.Add("@Email", customer.Email);
                parameters.Add("@Phone", customer.Phone);
                parameters.Add("@Address", customer.Address);
                parameters.Add("@City", customer.City);
                parameters.Add("@State", customer.State);
                parameters.Add("@PostalCode", customer.PostalCode);

                var result = await connection.ExecuteAsync("SP_UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        ///////////////////////////////////////////// DELETE /////////////////////////////////
        public async Task<int> DeleteCustomer(int CustomerId)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CustomerId", CustomerId);

                var result = await connection.ExecuteAsync("SP_DeleteCustomer", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

    }
}
