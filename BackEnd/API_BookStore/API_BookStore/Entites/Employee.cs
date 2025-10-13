using System.ComponentModel.DataAnnotations.Schema;
namespace API_BookStore.Entites
{
    [Table("Employee")]
    public class Employee
    {
        private int Id { get; set; }
        private string EmployeeCode{ get; set; }
        private string EmployeeName{ get; set; }
        private string Gender{ get; set; }
        private DateTime Birthday { get; set; }
        private string EmployeeAddress { get; set; }
        public string Email { get; set; }
    }
}
