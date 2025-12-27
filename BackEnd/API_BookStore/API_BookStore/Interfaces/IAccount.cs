using API_BookStore.DTOs.AccountDto;
using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IAccount
    {
   
        Task <bool> CreateAccount(Account account);

        Task<List<Account?>> GetAllAccountsAsync();
        Task<Account?> GetAccountByUserCodeAsync(string userCode);
        //Task<Employee?> GetEmployeeNoAccountAsync(string accountCode);
        Task<bool> UpdateAccountAsync(string hashPass, string userCode);
        Task<bool> DeleteAccountAsync(string userCode);

    }
}
