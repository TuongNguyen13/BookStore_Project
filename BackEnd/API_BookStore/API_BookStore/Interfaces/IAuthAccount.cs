using API_BookStore.Models;
namespace API_BookStore.Interfaces
{
    public interface IAuthAccount
    {
        Task<string?> AuthLoginAsync (AccountRequestModel accountRequestModel);
    }
}
