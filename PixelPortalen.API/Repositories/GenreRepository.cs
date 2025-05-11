using PixelPortalen.API.Data;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories;
public class GenreRepository : Repository<Genre>, IGenreRepository
{
    private APIDbContext? genreContext => context as APIDbContext;

    public GenreRepository(APIDbContext context) : base(context)
    {
    }

    public Genre? GetByName(string name)
    {
        return genreContext.Genres.FirstOrDefault(g => g.Name == name);
    }
}