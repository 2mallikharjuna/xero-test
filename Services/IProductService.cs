using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Services
{
    /// <summary>
    /// IProductService interface definition
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get All the products
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Product>> GetAllProductsAsync();
        /// <summary>
        /// Get the product by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Product> GetProductByIdAsync(Guid id);
        /// <summary>
        /// Add the product entry
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> AddProductAsync(Product product);
        /// <summary>
        /// Update the product entry
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<Product> UpdateProductAsync(Guid productId, Product product);
        /// <summary>
        /// Delete the product entry
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Task<Product> DeleteProductAsync(Guid productId);
    }
}
