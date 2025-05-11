using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.DTO;

namespace PixelPortalen.API.Services
{
    public class CustomerRatingService
    {
        private readonly ICustomerRatingRepository _repository;

        public CustomerRatingService(ICustomerRatingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerRating>> GetAllAsync() =>
            await _repository.GetRatingsAsync();

        public async Task<IEnumerable<CustomerRating>> GetByCustomerIdAsync(int customerId) =>
            await _repository.GetRatingsByCustomerIdAsync(customerId);

        public async Task<CustomerRating> AddRatingAsync(int customerId, CustomerRatingDTO dto)
        {
            var rating = new CustomerRating
            {
                CustomerId = customerId,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddRatingAsync(rating);

            return rating;
        }

        public async Task<List<CustomerRating>> GetRatingsByProductAsync(int productId)
        {
            return await _repository.GetRatingsByProductIdAsync(productId);
        }


    }
}
