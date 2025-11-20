using API_BookStore.DTOs;

namespace API_BookStore.Interfaces
{
    public interface IAuthAccount
    {
        Task<string?> AuthLoginAsync (AccountRequestModel accountRequestModel);
    }
}
