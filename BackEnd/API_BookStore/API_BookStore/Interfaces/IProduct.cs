using API_BookStore.DTOs;
using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IProduct
    {
        Task<List<Product?>> GetAllProductsAsync();
        Task<Product?> GetProductByCodeAsync(string productCode);
        Task<string> GenerateProductCode();
        Task<bool> CreateProduct(ProductDTO productDto);
        Task<bool> UpdateProduct(ProductDTO productDto, string productCode);
        Task<bool> DeleteProduct(string productCode);
    }
}
