using API_BookStore.DTOs;
using API_BookStore.Interfaces;
using API_BookStore.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Data;

namespace API_BookStore.Services
{
    public class ProductServices : IProduct
    {
        private readonly MyDbContext _context;
        private readonly IDbConnection _dbConnection;
        public ProductServices(MyDbContext myDbContext, IDbConnection dbConnection)
        {

            _context = myDbContext;
            _dbConnection = dbConnection;
        }

        public async Task<bool> CreateProduct(ProductDTO productDto)
        {
            try
            {
                var sqlCheckProductExist = "SELECT COUNT(1) FROM Products WHERE ProductName = @ProductName";
                var productExist = await _dbConnection.ExecuteScalarAsync<bool>(sqlCheckProductExist, new { ProductName = productDto.ProductName });
                if (productExist)
                    return false;

                string newProductCode;

                string sql = @"SELECT TOP 1 ProductCode
                           FROM Products
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

                string imgageUrl = string.Empty;

                Console.WriteLine(productDto.ProductImageUrl);

                if (productDto.ProductImageUrl != null)
                {

                    var forlderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                    if (!Directory.Exists(forlderPath))
                    {
                        Directory.CreateDirectory(forlderPath);
                    }

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDto.ProductImageUrl.FileName)}";
                    var filePath = Path.Combine(forlderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.ProductImageUrl.CopyToAsync(stream);
                    }

                    imgageUrl = $"/images/{fileName}";

                    // Here you can implement logic to save the image to a server or cloud storage
                    // and get the URL. For simplicity, we'll just use the file name.
                }

                var newProduct = new Product
                {
                    ProductCode = newProductCode,
                    ProductName = productDto.ProductName,
                    Price = decimal.Parse(productDto.Price),
                    ProductType = productDto.ProductType,
                    ProductYear = int.Parse(productDto.ProductYear),
                    StockQuantity = int.Parse(productDto.StockQuantity),
                    ProductImageUrl = imgageUrl
                };
                _context.Products.Add(newProduct);
                 await _context.SaveChangesAsync();
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteProduct(string productCode)
        {
            try
            {
                var productExist = await _context.Products.FirstOrDefaultAsync(p => p.ProductCode == productCode);
                if (productExist == null)
                    return false;
                _context.Products.Remove(productExist);
                var result = await _context.SaveChangesAsync();
                if (result != null)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> GenerateProductCode()
        {

            var lastProductCode = await _context.Products
        .OrderByDescending(p => p.ProductCode)
        .Select(p => p.ProductCode)
        .FirstOrDefaultAsync();
            string newProductCode = "PR0001";
            if (lastProductCode != null)
            {
                int numericPart = int.Parse(lastProductCode.Substring(2));
                numericPart++;
                newProductCode = "PR" + numericPart.ToString("D4");
            }
            return newProductCode;
        }

        public async Task<List<Product?>> GetAllProductsAsync(string productName, string productType, int pageNumber, int pageSize)
        {
            string sql = "SELECT * FROM Products " +
                "WHERE ProductName LIKE N'%" + productName + "%' " +
                "AND ProductType LIKE N'%" + productType + "%'" + 
                "ORDER BY ProductCode OFFSET @PageSize * (@PageNumber -1) ROWS " +
                "FETCH NEXT @PageSize ROW ONLY";

            var products = await _dbConnection.QueryAsync<Product>(sql, new {productName, productType, pageSize, pageNumber });
            return products.ToList();
        }

        public async Task<int> GetTotalProductsAsync(string productName,  string productType)
        {
            string sqlCount = "SELECT COUNT(1) FROM Products " +
                "WHERE ProductName LIKE N'%" + productName + "%' " +
                "AND ProductType LIKE N'%" + productType + "%'";
            var count = await _dbConnection.ExecuteScalarAsync<int>(sqlCount);
            return count;
        }

        public async Task<Product?> GetProductByCodeAsync(string productCode)
        {
            string sql = "SELECT * FROM Products WHERE ProductCode = @ProductCode";
            var product = await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { ProductCode = productCode });
            return product;
        }

        public async Task<bool> UpdateProduct(ProductDTO productDto, string productCode)
        {
            try
            {
                var productExist = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductCode == productCode);
               
                if (productExist == null)
                    return false;

                productExist.ProductName = productDto.ProductName;
                productExist.ProductType = productDto.ProductType;
                productExist.Price = decimal.Parse(productDto.Price);
                productExist.StockQuantity = int.Parse(productDto.StockQuantity);
                productExist.ProductYear = int.Parse(productDto.ProductYear);

                if (productDto.ProductImageUrl != null)
                {
                    if (productExist.ProductImageUrl != null)
                    {
                        if (!string.IsNullOrEmpty(productExist.ProductImageUrl))
                        {
                            var oldImagePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            productExist.ProductImageUrl.TrimStart('/')
                            );

                            if (File.Exists(oldImagePath))
                            {
                                File.Delete(oldImagePath);
                            }
                        }
                    }
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(productDto.ProductImageUrl.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.ProductImageUrl.CopyToAsync(stream);
                    }

                    productExist.ProductImageUrl = $"/images/{fileName}";
                }

                

                var result = await _context.SaveChangesAsync();
                if (result != null)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
