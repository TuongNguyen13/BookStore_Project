namespace API_BookStore.DTOs
{
    public class ProductDTO
    {

        public string ProductName { get; set; }
        public string Price { get; set; }

        public string ProductType { get; set; }
        public string ProductYear { get; set; }
        public string StockQuantity { get; set; }
        public IFormFile? ProductImageUrl { get; set; }
    }
}
