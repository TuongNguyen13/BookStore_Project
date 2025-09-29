using Microsoft.EntityFrameworkCore;
using API_BookStore.Interfaces;
using API_BookStore.Dbcontext;
using API_BookStore.Entites;
namespace API_BookStore.Services
{
    public class AccountService : IAccount
    {
        private readonly MyDbContext _context;
        public AccountService( MyDbContext context) 
        {
           _context = context;  
        }

        public async Task<Account?> GetAccountInfor(string username)
        {
            return await _context.Accounts.AsNoTracking().Where(u=> u.Username == username).
                Select(u => new Account
                {
                    Username = u.Username,
                    Pass = u.Pass,
                    //EmployeeID = u.EmployeeID
                })
                .SingleOrDefaultAsync();
        }


        public async Task CreateTokenLogin(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }


    }
}
