using API_BookStore.DTOs.AccountDto;

namespace API_BookStore.Interfaces
{
    public interface IAccount
    {
        Task<AccountLoginDTO?> GetAccountInfor(string username, string password);
        //Task CreateTokenLogin(Account account);
    }
}
