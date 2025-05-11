using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsInStock();
        Task<PagedResult<Product>> GetPaged(int page, int pageSize);
    }
}
