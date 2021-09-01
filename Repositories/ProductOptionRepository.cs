using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repositories
{
    /// <summary>
    /// ProductOption Repository class
    /// </summary>
    public class ProductOptionRepository : BaseRepository<ProductOption>, IProductOptionRepository
    {
        IQueryable<ProductOption> _productOptions;
        public ProductOptionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            appDbContext.Database.EnsureCreated();
            _productOptions = GetAll();
        }        

        public async Task<IEnumerable<ProductOption>> GetAllProductOptionByIdAsync(Guid productId)
        {
            var productOptions = _productOptions.Where(PO => PO.ProductId == productId);
            return await productOptions.ToListAsync();
        }

        public async Task<ProductOption> GetProductOptionByIdAsync(Guid productId, Guid id)
        {
            return await _productOptions.FirstOrDefaultAsync(PO => PO.ProductId == productId && PO.Id == id);            
        }
        public async Task<ProductOption> AddProductOptionAsync(Guid productId, ProductOption productOption)
        {
            var productOptions = _productOptions.Where(PO => PO.ProductId == productId && PO.Id == productOption.Id);
            if(productOptions.Count() == 0) throw new ArgumentException("Invalid product or ProductOption Id");
            return await AddAsync(productOption);           
        }
        public async Task<ProductOption> UpdateProductOptionAsync(Guid productId, Guid id, ProductOption productOption)
        {
            if (productId != productOption.ProductId) throw new ArgumentException($"Product Id {productId} Not Matches with {productOption.Id}");
            if (id != productOption.Id) throw new ArgumentException($"Product Option Id {productId} Not Matches with {productOption.Id}");

            var productOptions = GetProductOptionByIdAsync(productId, id);
            if (productOptions == null) throw new ArgumentException("Invalid product Id: {productId} or ProductOption :{Id}");
            return await UpdateAsync(productOption);            
        }
        public async Task<ProductOption> DeleteProductOptionAsync(Guid productId, Guid Id)
        {
            var findProductOption = await GetAll().FirstOrDefaultAsync(PO => PO.Id == Id && PO.ProductId == productId);
            if (findProductOption == null) throw new ArgumentException($"Product Options Not exists with productID : {productId} or ProductOption :{Id}");

            return await DeleteAsync(findProductOption);            
        }
    }    
}
