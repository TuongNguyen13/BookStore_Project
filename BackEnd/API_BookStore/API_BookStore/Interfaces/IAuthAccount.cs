namespace API_BookStore.Interfaces
{
    public interface IAuthAccount
    {
        Task<string?> AuthLoginAsync (string username, string password);
    }
}
