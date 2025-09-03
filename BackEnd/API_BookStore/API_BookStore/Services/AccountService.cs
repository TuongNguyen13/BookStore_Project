using Microsoft.EntityFrameworkCore;
using API_BookStore.Interfaces;
using API_BookStore.Dbcontext;
namespace API_BookStore.Services
{
    public class AccountService
    {
        private readonly MyDbContext _context;
        private AccountService(MyDbContext context)
        {
            _context = context;
        }

    }
}
