using PixelPortalen.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PixelPortalen.API.Repositories.Interfaces
{
    public interface ICustomerRatingRepository
    {
        Task AddRatingAsync(CustomerRating rating);
        Task<CustomerRating> GetRatingByIdAsync(int id);
        Task<IEnumerable<CustomerRating>> GetRatingsAsync();
        Task<IEnumerable<CustomerRating>> GetRatingsByCustomerIdAsync(int customerId);
        Task<List<CustomerRating>> GetRatingsByProductIdAsync(int productId);
    }
}