using API_BookStore.Models;
using API_BookStore.Entites;

namespace API_BookStore.Interfaces
{
    public interface IAccount
    {
        Task<Account> GetAccountInfor(string username);
        Task CreateTokenLogin(Account account);
    }
}
