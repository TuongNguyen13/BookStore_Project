using API_BookStore.DTOs.AccountDto;

namespace API_BookStore.Interfaces
{
    public interface IAuthAccount
    {
        Task<string?> AuthLoginAsync (AccountRequestDTO accountRequestModel);
    }
}
