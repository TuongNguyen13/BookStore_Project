using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_BookStore.Services;
using API_BookStore.Models;

namespace API_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AuthLoginService _jwtAuth;
        public LoginController(AuthLoginService jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginModel accountModel)
        {
            var token = await _jwtAuth.AuthLoginAsync(accountModel.UserName, accountModel.Pass);
            if (token == null)
            {
                return Ok(new
                {
                    status = -1,
                    message = "Sai tài khoản hoặc mật khẩu"
                }
                   );
            }
            return Ok(new
            {
                status = 1,
                message = token.ToString()
            });
        }
    }
}
