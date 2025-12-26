using API_BookStore.Models;
namespace API_BookStore.DTOs.EmployeeDto
{
    public class UpdateEmployeeDto
    {
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string EmployeeAddress { get; set; }
        public string Email { get; set; }

        public string EmployeeRole { get; set; }
    }
}
