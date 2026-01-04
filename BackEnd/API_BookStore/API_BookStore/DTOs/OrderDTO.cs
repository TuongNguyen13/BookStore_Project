namespace API_BookStore.DTOs
{
    public class OrderDTO
    {
        public string CustomerCode { get; set; } = null!;
        public string? EmployeeCode { get; set; }
        public string? Payment { get; set; }
        public string? OrderStatus { get; set; } = "InCart";
        public decimal? OrderTotal { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
