using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using API_BookStore.Interfaces;
using API_BookStore.DTOs.AccountDto;

namespace API_BookStore.Services
{
    public class AuthLoginService : IAuthAccount
    {
        private readonly IAccount _account;
        private readonly IConfiguration _configuration;

        public AuthLoginService(IAccount account, IConfiguration configuration)
        {
            _account = account;
            _configuration = configuration;

        }

        public static class CustomClaimTypes
        {
            public const string EmployeeCode = "EmployeeCode";
            public const string EmployeeName = "EmployeeName";
        }

        public async Task<string?> AuthLoginAsync(AccountRequestDTO accountRequestModel)
        {
            var user = await _account.GetAccountInfor(accountRequestModel.userName, accountRequestModel.pass);
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
