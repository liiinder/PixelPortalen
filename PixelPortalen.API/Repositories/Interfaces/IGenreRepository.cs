using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories.Interfaces;

public interface IGenreRepository : IRepository<Genre>
{
    Genre? GetByName(string name);
}