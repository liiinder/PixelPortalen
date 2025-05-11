using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Category? GetByName(string name);
}