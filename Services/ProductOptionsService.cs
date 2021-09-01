using System;
using System.Threading.Tasks;
using RefactorThis.Models;
using RefactorThis.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace RefactorThis.Services
{
    /// <summary>
    /// ProductOptions Service class
    /// </summary>
    public class ProductOptionsService : IProductOptionsService
    {
        private readonly IProductOptionRepository _productOptionRepository;
        private ILogger<ProductOptionsService> Logger { get; }
        /// <summary>
        /// ProductOptionsService class constructor
        /// </summary>
        /// <param name="productOptionRepository"></param>        
        /// <param name="logger"></param>
        public ProductOptionsService(IProductOptionRepository productOptionRepository, ILogger<ProductOptionsService> logger)
        {
            _productOptionRepository = productOptionRepository;
            Logger = logger;

        }
        /// <summary>
        /// Add product option entity
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="productOption"></param>
        /// <returns></returns>
        public async Task<ProductOption> AddProductOptionAsync(Guid productId, ProductOption productOption)
        {
            return await _productOptionRepository.AddProductOptionAsync(productId, productOption);            
        }
        /// <summary>
        /// Get All the product options of given product Id
        /// </summary>
        /// <param name="poductId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductOption>> GetAllProductOptionsAsync(Guid poductId)
        {
            return await _productOptionRepository.GetAllProductOptionByIdAsync(poductId);

        }
        /// <summary>
        /// Get the product option of Given product Id and ProductOption Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductOption> GetProductOptionAsync(Guid productId, Guid id)
        {
            return await _productOptionRepository.GetProductOptionByIdAsync(productId, id);
        }
        /// <summary>
        /// Update the product option entity of Given product Id and ProductOption Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <param name="productOption"></param>
        /// <returns></returns>
        public async Task<ProductOption> UpdateProductOptionAsync(Guid productId, Guid id, ProductOption productOption)
        {
            return await _productOptionRepository.UpdateProductOptionAsync(productId, id, productOption);
        }
        /// <summary>
        /// Delete the product option entity of Given product Id and ProductOption Id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductOption> DeleteProductOptionAsync(Guid productId, Guid id)
        {
            return await _productOptionRepository.DeleteProductOptionAsync(productId, id);
        }
        
    }
}
