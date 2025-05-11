using PixelPortalen.API.Repositories;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Services
{
    public class GenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepo)
        {
            genreRepository = genreRepo;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await genreRepository.GetAll();
        }

        public async Task AddGenre(string genre)
        {
            var newGenre = new Genre() { Name = genre };
            await genreRepository.Add(newGenre);
        }

        // WARNING CASCADE DELETE
        //public async Task RemoveGenre(int id)
        //{
        //    var genre = await genreRepository.Get(id);
        //    await genreRepository.Remove(genre);
        //}

        public async Task<Genre?> EditGenre(int id, string nameOfGenre)
        {
            var existingGenre = await genreRepository.Get(id);

            if (existingGenre is null) return null;

            existingGenre.Name = nameOfGenre;
            await genreRepository.Edit(existingGenre);

            return existingGenre;
        }
    }
}
