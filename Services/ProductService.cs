using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RefactorThis.Models;
using RefactorThis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Services
{
    /// <summary>
    /// Product Service
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;        
        private ILogger<ProductService> Logger { get; }

        /// <summary>
        /// ProductService constructor
        /// </summary>
        /// <param name="productRepository"></param>        
        /// <param name="logger"></param>
        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(IProductService));            
            Logger = logger ?? throw new ArgumentNullException(nameof(ILogger<ProductService>));
        }
        /// <summary>
        /// Get the product entity from give product Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        /// <summary>
        /// Add product entity to product table
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> AddProductAsync(Product product)
        {
            return await _productRepository.AddProductAsync(product);            
        }
        /// <summary>
        /// Get all the product entiries from product table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProducts();
        }
        /// <summary>
        /// update the product entity of given product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task<Product> UpdateProductAsync(Guid productId, Product product)
        {
            return await _productRepository.UpdateProductAsync(productId, product);            
        }
        /// <summary>
        /// Delete the product entity of given product Id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<Product> DeleteProductAsync(Guid productId)
        {
            return await _productRepository.DeleteProductAsync(productId);            
        }

    }
}
