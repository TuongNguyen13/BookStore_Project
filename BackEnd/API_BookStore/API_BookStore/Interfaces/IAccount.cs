using API_BookStore.DTOs;

namespace API_BookStore.Interfaces
{
    public interface IAccount
    {
        Task<AccountLoginModel?> GetAccountInfor(string username, string password);
        //Task CreateTokenLogin(Account account);
    }
}
