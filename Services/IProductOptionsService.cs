using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Services
{
    public interface IProductOptionsService
    {
        Task<IEnumerable<ProductOption>> GetAllProductOptionsAsync(Guid poductId);
        Task<ProductOption> GetProductOptionAsync(Guid productId, Guid id);
        Task<ProductOption> AddProductOptionAsync(Guid productId, ProductOption productOption);
        Task<ProductOption> UpdateProductOptionAsync(Guid productId, Guid id, ProductOption productOption);
        Task<ProductOption> DeleteProductOptionAsync(Guid productId, Guid id);        
    }
}
