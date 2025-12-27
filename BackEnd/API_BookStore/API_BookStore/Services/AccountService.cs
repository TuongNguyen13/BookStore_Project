using System;
using System.Security.Cryptography;
using Dapper;
using System.Data;
using API_BookStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using API_BookStore.Models;
using API_BookStore.DTOs.AccountDto;
using System.Text;
using System.Data.Entity.ModelConfiguration.Conventions;


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

        public async Task<bool> CreateAccount(Account account)
        {
            try
            {
                string sqlCheck = "SELECT UserCode FROM Account Order by UserCode DESC Limit 1";
                var lastUserCode = await _dbConnection.QueryFirstOrDefaultAsync<string>(sqlCheck);
                if (lastUserCode != null)
                {
                    int numericPart = int.Parse(lastUserCode.Substring(2));
                    numericPart++;
                    account.UserCode = "NV" + numericPart.ToString("D4");
                }
                else
                {
                    account.UserCode = "NV0001";
                }

                var employeeExists = await GetEmployeeNoAccountAsync(account.UserCode);
                if (employeeExists == null)
                    return false;

                account.EmployeeCode = employeeExists.EmployeeCode;
               account.Pass = HashPassword(account.Pass);

                _context.Accounts.Add(account);
                var success = await _context.SaveChangesAsync();
                if (success != 0)
                    return false;
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo tài khoản: {ex.Message}");
                return false;
            }

        }

        public async Task<Account?> GetAccountByUserCodeAsync(string userCode)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.UserCode == userCode);
        }

        public async Task<List<Account?>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        private async Task<Employee?> GetEmployeeNoAccountAsync(string accountCode)
        {
            try
            {
                string sql = @"SELECT e.*
                   FROM Employee AS e
                   LEFT JOIN Account AS a ON e.EmployeeCode = a.EmployeeCode
                   WHERE a.EmployeeCode IS NULL AND e.EmployeeCode = @accountCode";
                var employee = await _dbConnection.QueryFirstOrDefaultAsync<Employee>(sql, new { accountCode });
                if (employee == null)
                {
                    return null;
                }
                return employee;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy nhân viên chưa có tài khoản: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateAccountAsync(string hashPass, string userCode)
        {
            var account = await _context.Accounts.AnyAsync(a => a.UserCode == userCode);
            if (account == null)
                return false;

            var updateAccount = new Account
            {
                // hash password
                Pass = HashPassword(hashPass)
            };
            _context.Accounts.Update(updateAccount);
            var success = await _context.SaveChangesAsync();
            if (success != 0)
                return false;
            return true;
        }

        public async Task<bool> DeleteAccountAsync(string userCode)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserCode == userCode);
             if (account == null)
                return false;
             _context.Accounts.Remove(account);
                var success = await _context.SaveChangesAsync();
                if (success != 0)
                    return false;
                return true;
        }


        // Hash password method
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
