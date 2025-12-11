namespace API_BookStore.DTOs
{
    public class ProductDTO
    {
        public int? Id { get; set; }
        public string? ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string ProductYear { get; set; }
        public string StockQuatity { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
