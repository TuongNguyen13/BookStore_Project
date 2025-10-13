using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using API_BookStore.Entites;
namespace API_BookStore.Dbcontext
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Products> Products { get; set; }

    }

}
