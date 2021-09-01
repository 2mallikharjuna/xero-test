using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repositories
{
    /// <summary>
    /// IProductRepository interface
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllProducts();
       
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(Guid productId, Product product);
        Task<Product> DeleteProductAsync(Guid productId);
    }
}
