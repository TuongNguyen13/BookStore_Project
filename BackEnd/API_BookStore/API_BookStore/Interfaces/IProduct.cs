using API_BookStore.DTOs;
using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IProduct
    {
        Task<List<Product?>> GetAllProductsAsync();
        Task<Product?> GetProductByCodeAsync(string productCode);
        Task<bool> CreateProduct(ProductDTO productDto);
        Task<bool> UpdateProduct(ProductDTO productDto);
        Task<bool> DeleteProduct(string productCode);
    }
}
