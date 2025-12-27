using API_BookStore.DTOs.AccountDto;
using API_BookStore.Interfaces;
using API_BookStore.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_BookStore.Services
{
    public class AuthLoginService : IAuthAccount
    {

        private readonly IConfiguration _configuration;
    
        private readonly MyDbContext _context;

        public AuthLoginService( IConfiguration configuration, MyDbContext myDbContext )
        {

            _configuration = configuration;
  
            _context = myDbContext;

        }

        public static class CustomClaimTypes
        {
            public const string EmployeeCode = "EmployeeCode";
            public const string EmployeeName = "EmployeeName";
        }

        public async Task<string?> AuthLoginAsync(AccountRequestDTO accountRequestModel)
        {
            using var _dbConnection = _context.Database.GetDbConnection();
            string sql = @"SELECT a.Username, e.EmployeeName, e.EmployeeRole
                   FROM Account AS a
                   JOIN Employee AS e ON a.EmployeeCode = e.EmployeeCode
                   WHERE a.Username = @username AND a.Pass = @password";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<AccountLoginDTO>(
                sql,
                new { accountRequestModel.userName, accountRequestModel.pass });


            if (user == null)
            {
                return null;
            }

            var loginModel = new AccountLoginDTO
            {
                UserName = user.UserName,
                EmployeeCode = user.EmployeeCode,
                EmployeeName = user.EmployeeName
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);


            var claims = new[]
            {
                 new Claim(ClaimTypes.Name, loginModel.UserName),
                 new Claim(CustomClaimTypes.EmployeeCode, loginModel.EmployeeCode),
                 new Claim(CustomClaimTypes.EmployeeName, loginModel.EmployeeName)
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Các claims của token
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes
                (int.Parse(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }



    }
}
