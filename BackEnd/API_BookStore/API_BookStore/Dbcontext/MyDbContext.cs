using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API_BookStore.Entites;
namespace API_BookStore.Dbcontext
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-TUONG; Database = Book_Manage; User Id= sa; Password = 123");
        }

        DbSet<Account> Accounts { get; set; }

        
    }

}
