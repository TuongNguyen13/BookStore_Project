using API_BookStore.Models;
using API_BookStore.Entites;
using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IAccount
    {
        Task<AccountLoginModel?> GetAccountInfor(string username, string password);
        //Task CreateTokenLogin(Account account);
    }
}
