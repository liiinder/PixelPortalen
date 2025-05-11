using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer?> GetByEmail(string email);
        Task<Customer?> GetByPhoneNumber(string phoneNumber);
        Task<IEnumerable<Customer>?> GetAllCustomers();
    }
}
