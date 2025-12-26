using API_BookStore.DTOs.AccountDto;
using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IAccount
    {
        Task<AccountLoginDTO?> GetAccountInfor(string username, string password);
        Task CreateAccount(Account account);
    }
}
