using Microsoft.EntityFrameworkCore;
using PixelPortalen.API.Data;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private APIDbContext? productContext => context as APIDbContext;

        public ProductRepository(APIDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProductsInStock()
        {
            return await productContext.Products
                .Where(x => (x.Stock > 0 || x.Stock == null))
                .Include(p => p.Genres)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<Product?> Get(int id)
        {
            var products = await productContext.Products
                .Where(x => (x.Id == id))
                .Include(p => p.Genres)
                .Include(p => p.Category)
                .ToListAsync();
            return products[0];
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await productContext.Products
                .Include(p => p.Genres)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<PagedResult<Product>> GetPaged(int page, int pageSize)
        {
            var query = productContext.Products
                .Include(p => p.Genres)
                .Include(p => p.Category);

            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Product>
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}
