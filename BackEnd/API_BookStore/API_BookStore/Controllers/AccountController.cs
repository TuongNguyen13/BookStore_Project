using API_BookStore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_BookStore.Models;
using API_BookStore.DTOs.AccountDto;

namespace API_BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccount _accountService;
        public AccountController(IAccount accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            if (accounts == null)
            {
                return Ok(new
                {
                    status = -1,
                    message = "Không có tài khoản nào"
                });
            }
            return Ok(new
            {
                status = 1,
                message = accounts
            });
        }

        [HttpGet("GetAccountByUserCode/{userCode}")]
        public async Task<IActionResult> GetAccountByUserCode(string userCode)
        {
            var account = await _accountService.GetAccountByUserCodeAsync(userCode);
            if (account == null)
            {
                return Ok(new
                {
                    status = -1,
                    message = "Không tìm thấy tài khoản"
                });
            }
            return Ok(new
            {
                status = 1,
                message = account
            });
        }

        [HttpPost("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount([FromBody] Account account)
        {
            var result = await _accountService.CreateAccount(account);
            if (!result)
            {
                return Ok(new
                {
                    status = -1,
                    message = "Tạo tài khoản thất bại"
                });
            }
            return Ok(new
            {
                status = 1,
                message = "Tạo tài khoản thành công"
            });
        }
    }
}
