using API_BookStore.Models;
using System.Data;
using Dapper;
using System.Data.SqlClient;
using API_BookStore.Interfaces;
using API_BookStore.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Services
{
    public class CustomerServices : ICustomer
    {
        private readonly MyDbContext _context;
        private readonly IDbConnection _dbConnection;

        public CustomerServices(MyDbContext myDbContext, IDbConnection dbConnection)
        {
            _context = myDbContext;
            _dbConnection = dbConnection;

        }

        public async Task<bool> CreateCustomerAsync(CustomerDTO customerDto)
        {
            string sql = @"SELECT TOP 1 CustomerCode
                       FROM Customer
                       ORDER BY CustomerCode DESC";
            var lastCustomerCode = _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql).Result;
            string newCustomerCode;
            if (lastCustomerCode != null)
            {
                int numericPart = int.Parse(lastCustomerCode.CustomerCode.Substring(2));
                numericPart++;
                newCustomerCode = "KH" + numericPart.ToString("D4");
            }
            else
            {
                newCustomerCode = "KH0001";
            }
            var newCustomer = new Customer
            {
                CustomerCode = newCustomerCode,
                CustomerName = customerDto.CustomerName,
                CustomerAddress = customerDto.CustomerAddress,
                Gender = customerDto.Gender,
                PhoneNumber = customerDto.PhoneNumber,
            };
            _context.Customers.Add(newCustomer);
            var result = await _context.SaveChangesAsync();
            if (result != null)
                return true;
            return false;

        }

        public async Task<bool> DeleteCustomerAsync(string customerCode)
        {
            var existingCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerCode == customerCode);
            if (existingCustomer == null)
                return false;
            _context.Customers.Remove(existingCustomer);
            var result = await _context.SaveChangesAsync();
            if (result != null)
                return true;
            return false;
        }

        public async Task<List<Customer?>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public Task<Customer?> GetCustomerByIdAsync(string customerCode)
        {
            return _context.Customers.FirstOrDefaultAsync(c => c.CustomerCode == customerCode);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _context.Customers.AnyAsync(c => c.CustomerCode == customer.CustomerCode);
            if (existingCustomer == false)
                return false;
            _context.Customers.Update(customer);
            var result = await _context.SaveChangesAsync();
            if (result != null)
                return true;
            return false;

        }
    }
}
