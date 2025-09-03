using Microsoft.EntityFrameworkCore;
using API_BookStore.Dbcontext;
using API_BookStore.Entites;
using API_BookStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace API_BookStore.Auth
{
    public class JwtAuth
    {
        private readonly IConfiguration _configuration;
        private string generateJwtToken(AccountModel accountModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var claims = new List<Claim>
            { 
                new Claim(ClaimTypes.NameIdentifier, accountModel.UserId),
                new Claim(ClaimTypes.Name, accountModel.UserName),
                new Claim("EmployeeId", accountModel.EmployeeId.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Các claims của token
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes
                (int.Parse(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
                ,SecurityAlgorithms.HmacSha256Signature)
            };

           var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token); 

        }
    }
}
