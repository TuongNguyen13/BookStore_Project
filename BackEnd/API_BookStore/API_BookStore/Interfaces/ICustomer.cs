using API_BookStore.Models;
using API_BookStore.DTOs;

namespace API_BookStore.Interfaces
{
    public interface ICustomer
    {
        Task<List<Customer?>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(string customerCode);
        Task<bool> CreateCustomerAsync(CustomerDTO customerDto);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(string customerCode);
    }
}
