using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_BookStore.Auth;
using API_BookStore.Models;

namespace API_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtAuth _jwtAuth;
        public LoginController(JwtAuth jwtAuth) 
        {
            _jwtAuth = jwtAuth;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] AccountModel accountModel)
        {
            if( accountModel.UserName = )
        }
    }
}
