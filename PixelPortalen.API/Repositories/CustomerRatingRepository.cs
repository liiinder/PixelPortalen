using PixelPortalen.API.Data;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PixelPortalen.API.Repositories
{
    public class CustomerRatingRepository : ICustomerRatingRepository
    {
        private readonly APIDbContext _context;

        public CustomerRatingRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task AddRatingAsync(CustomerRating rating)
        {
            _context.CustomerRatings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomerRating> GetRatingByIdAsync(int id)
        {
            return await _context.CustomerRatings.FindAsync(id);
        }

        public async Task<IEnumerable<CustomerRating>> GetRatingsAsync()
        {
            return await _context.CustomerRatings.ToListAsync();
        }

        public async Task<IEnumerable<CustomerRating>> GetRatingsByCustomerIdAsync(int customerId)
        {
            return await _context.CustomerRatings
                .Where(r => r.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<List<CustomerRating>> GetRatingsByProductIdAsync(int productId)
        {
            return await _context.CustomerRatings
                .Include(r => r.Customer)
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

    }
}
