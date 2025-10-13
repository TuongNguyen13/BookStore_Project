using System.ComponentModel.DataAnnotations.Schema;
namespace API_BookStore.Entites
{
    [Table("Products")]
    public class Products
    {
        private int Id { get; set; }
        private string ProductCode { get; set; }
        private string ProductName { get; set; }
        private string Price { get; set; }
        private string ProductYear { get; set; }
        private string StockQuatity { get; set; }
        private string Category { get; set; }
        private string ProductImageUrl { get; set; }

    }
}
