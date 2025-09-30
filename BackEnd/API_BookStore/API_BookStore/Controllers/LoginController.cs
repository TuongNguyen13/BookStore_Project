using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_BookStore.Services;
using API_BookStore.Models;
using API_BookStore.Interfaces;
using System.Diagnostics;

namespace API_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthAccount _jwtAuth;
        public LoginController(IAuthAccount jwtAuth)
        {
            _jwtAuth = jwtAuth;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AccountRequestModel accountRequestModel)
        {
            var token = await _jwtAuth.AuthLoginAsync(accountRequestModel);
            var sw = new Stopwatch();
            sw.Start();

            // đo thời gian chạy AuthLoginAsync
            var authStart = sw.ElapsedMilliseconds;
            var authEnd = sw.ElapsedMilliseconds;
            Debug.WriteLine($"AuthLoginAsync: {authEnd - authStart} ms");

            sw.Stop();
            Debug.WriteLine($"Tổng thời gian Login API: {sw.ElapsedMilliseconds} ms");

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
                message = "Đăng nhập thành công",
                token = "Đã sinh ra token"
            });
        }
    }
}
