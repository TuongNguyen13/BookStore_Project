namespace API_BookStore.DTOs.EmployeeDto
{
    public class EmployeeListDto
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string EmployeeAddress { get; set; }
        public string Email { get; set; }
    }
}
