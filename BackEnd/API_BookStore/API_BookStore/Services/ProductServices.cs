using API_BookStore.DTOs;
using API_BookStore.Interfaces;
using API_BookStore.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API_BookStore.Services
{
    public class ProductServices : IProduct
    {
        private readonly MyDbContext _context;
        private readonly IDbConnection _dbConnection;
        public ProductServices( MyDbContext myDbContext, IDbConnection dbConnection)
        {
           
            _context = myDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<bool> CreateProduct(ProductDTO productDto)
        {
           var productExist = await _context.Products.AnyAsync(p => p.ProductName == productDto.ProductName);
              if (productExist)
                return false;

            string newProductCode;

                string sql = @"SELECT TOP 1 ProductCode
                           FROM Product
                           ORDER BY ProductCode DESC";
                var lastProductCode = await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql);
                if (lastProductCode != null)
                {
                    int numericPart = int.Parse(lastProductCode.ProductCode.Substring(2));
                    numericPart++;
                     newProductCode = "PR" + numericPart.ToString("D4");
                }
                else
                {
                    newProductCode = "PR0001";
                }

            var newProduct = new Product
            {
                 ProductCode = newProductCode,
                    ProductName = productDto.ProductName,
                    Price = decimal.Parse(productDto.Price),
                    ProductYear = int.Parse(productDto.ProductYear),
                    StockQuantity = int.Parse(productDto.StockQuatity),
                    ProductImageUrl = productDto.ProductImageUrl
            };
            _context.Products.Add(newProduct);
            var result = await _context.SaveChangesAsync();
            if(result != null)
                return true;
            return false;
        }

        public Task<bool> DeleteProduct(string productCode)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product?>> GetAllProductsAsync()
        {
           return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByCodeAsync(string productCode)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductCode == productCode);
        }

        public async Task<bool> UpdateProduct(ProductDTO productDto)
        {
           var productExist = await _context.Products.AnyAsync(p => p.ProductName == productDto.ProductName);
            if (productExist == null)
                return false;


            var existingProduct =  await _context.Products.FirstOrDefaultAsync(p => p.ProductName == productDto.ProductName);
            if (existingProduct != null)
            {
                existingProduct.ProductName = productDto.ProductName;
                existingProduct.Price = decimal.Parse(productDto.Price);
                existingProduct.StockQuantity = int.Parse(productDto.StockQuatity);
                existingProduct.ProductImageUrl = productDto.ProductImageUrl;
                existingProduct.ProductYear = int.Parse(productDto.ProductYear);
            }
                var result = await  _context.SaveChangesAsync();
                if (result != null)
                    return true;
            
            return false;
        }
    }
}
