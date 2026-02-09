using API_BookStore.DTOs;
using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IProduct
    {
        Task<List<Product?>> GetAllProductsAsync(string productName, string productType, int pageNumber, int pageSize);
        Task<Product?> GetProductByCodeAsync(string productCode);
        Task<int> GetTotalProductsAsync(string productName, string productType);
        Task<string> GenerateProductCode();
        Task<bool> CreateProduct(ProductDTO productDto);
        Task<bool> UpdateProduct(ProductDTO productDto, string productCode);
        Task<bool> DeleteProduct(string productCode);
    }
}
