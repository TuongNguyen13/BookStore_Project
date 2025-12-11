using Dapper;
using System.Data;
using API_BookStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using API_BookStore.Models;
using API_BookStore.DTOs.AccountDto;


namespace API_BookStore.Services
{
    public class AccountService : IAccount
    {
        private readonly MyDbContext _context;
        private readonly IDbConnection _dbConnection;

        public AccountService(MyDbContext context, IDbConnection dbConnection)
        {
           _context = context;
            _dbConnection = dbConnection;
        }

        public async Task<AccountLoginDTO?> GetAccountInfor(string username, string password)
        {
            string sql = @"SELECT a.Username, e.EmployeeCode, e.EmployeeName
                   FROM Account AS a
                   JOIN Employee AS e ON a.EmployeeId = e.ID
                   WHERE a.Username = @username AND a.Pass = @password";

            return await _dbConnection.QueryFirstOrDefaultAsync<AccountLoginDTO>(
                sql,
                new { username, password }
            );
        }



        //public async Task CreateTokenLogin(Account account)
        //{
        //    _context.Accounts.Add(account);
        //    await _context.SaveChangesAsync();
        //}


    }
}
