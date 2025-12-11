using API_BookStore.Models;
namespace API_BookStore.DTOs.AccountDto
{
    public class AccountLoginDTO
    {
        public string UserName { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
    }
}
