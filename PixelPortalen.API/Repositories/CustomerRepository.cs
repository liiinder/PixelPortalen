using Microsoft.EntityFrameworkCore;
using PixelPortalen.API.Data;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private APIDbContext? customerContext => context as APIDbContext;

        public CustomerRepository(APIDbContext context) : base(context)
        {
        }

        public Task<Customer?> GetByEmail(string email)
        {
            
            return customerContext.Customers
                .Include(c => c.Address)
                .Include(c => c.Orders)
                .ThenInclude(o => o.Details)
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public Task<Customer?> GetByPhoneNumber(string phoneNumber)
        {
            return customerContext.Customers
                .Include(c => c.Address)
                .Include(c => c.Orders)
                .ThenInclude(o => o.Details)
                .FirstOrDefaultAsync(c => c.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<Customer>?> GetAllCustomers()
        {
            var customers = await customerContext.Customers
               .Include(c => c.Address)
               .Include(c => c.Orders)
               .ThenInclude(o => o.Details)
               .ToListAsync();

            return customers;
        }
    }
}