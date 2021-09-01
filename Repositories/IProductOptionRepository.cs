using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RefactorThis.Repositories
{
    public interface IProductOptionRepository : IBaseRepository<ProductOption>
    {    
        Task<IEnumerable<ProductOption>> GetAllProductOptionByIdAsync(Guid productId);
        Task<ProductOption> GetProductOptionByIdAsync(Guid productId, Guid id);
        Task<ProductOption> AddProductOptionAsync(Guid productId, ProductOption productOption);
        Task<ProductOption> UpdateProductOptionAsync(Guid productId, Guid id, ProductOption productOption);
        Task<ProductOption> DeleteProductOptionAsync(Guid productId, Guid id);
    }
}