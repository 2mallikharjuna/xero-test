using Microsoft.EntityFrameworkCore;
using RefactorThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Repositories
{
    /// <summary>
    /// Product Repository
    /// </summary>
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {     
        IQueryable<Product> _products;
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            appDbContext.Database.EnsureCreated();
            _products = GetAll().Include(PO => PO.ProductOptions);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            var findProduct = await _products.FirstOrDefaultAsync(pItem => pItem.Id == productId);
            if (findProduct == null) throw new ArgumentException("Invalid productID");
            return findProduct;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            var findProduct = await _products.FirstOrDefaultAsync(pItem => pItem.Id == product.Id);
            if (findProduct != null) throw new ArgumentException($"Product already exists with productID {product.Id}");
            return await AddAsync(product);            
        }
        public async Task<Product> UpdateProductAsync(Guid productId, Product product)
        {
            if (productId != product.Id) throw new ArgumentException($"ProductId {productId} Not Matches with {product.Id}");

            var findProduct = await _products.FirstOrDefaultAsync(pItem => pItem.Id == product.Id);
            if (findProduct == null) throw new ArgumentException($"Product Not exists with productID {product.Id}");
            return await UpdateAsync(product);
        }
        //Delete the product and product options
        public async Task<Product> DeleteProductAsync(Guid productId)
        {
            var findProduct = await _products.FirstOrDefaultAsync(pItem => pItem.Id == productId);
            if (findProduct == null) throw new ArgumentException($"Product Not exists with productID {productId}");
            return await DeleteAsync(_products.FirstOrDefault(pItem => pItem.Id == productId));            
        }
    }
}
