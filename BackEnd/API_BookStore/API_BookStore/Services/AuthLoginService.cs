using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using API_BookStore.Interfaces;

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
        public async Task<string?> AuthLoginAsync(string username, string password)
        {
            var user = await _account.GetAccountInfor(username);
            if (user  == null || user.Pass != password)
            {
                return null;
            }
            //if (user.Pass!= password)
            //{
            //    return null;
            //}    
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("EmployeeId", user.EmployeeID.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Các claims của token
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes
                (int.Parse(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                , SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],        // thêm
                Audience = _configuration["Jwt:Audience"]     // thêm
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
