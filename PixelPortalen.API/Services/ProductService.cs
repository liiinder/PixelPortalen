using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IGenreRepository genreRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductService(
            IProductRepository prodRepo,
            IGenreRepository genreRepo,
            ICategoryRepository catRepo)
        {
            productRepository = prodRepo;
            genreRepository = genreRepo;
            categoryRepository = catRepo;
        }

        public async Task<IEnumerable<Product>> GetProductsInStock()
        {
            return await productRepository.GetProductsInStock();
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await productRepository.GetAll();
        }

        public async Task<Product?> GetById(int id)
        {
            return await productRepository.Get(id);
        }

        public async Task AddProduct(Product product)
        {
            var category = categoryRepository.GetByName(product.Category.Name)
                ?? new Category { Name = product.Category.Name };

            List<Genre> finalGenres = new();
            foreach (var genre in product.Genres)
            {
                var existingGenre = genreRepository.GetByName(genre.Name)
                      ?? new Genre { Name = genre.Name };

                finalGenres.Add(existingGenre);
            }

            product.Category = category;
            product.Genres = finalGenres;

            await productRepository.Add(product);
        }

        public async Task RemoveProduct(int id)
        {
            var product = await GetById(id);
            await productRepository.Remove(product);
        }

        public async Task<Product?> EditProduct(int id, Product product)
        {
            var existingProduct = await GetById(id);
            if (existingProduct is null) return null;

            existingProduct.Genres.Clear();

            List<Genre> finalGenres = new();
            foreach (var genre in product.Genres)
            {
                var existingGenre = genreRepository.GetByName(genre.Name)
                      ?? new Genre { Name = genre.Name };

                finalGenres.Add(existingGenre);
            }

            var category = categoryRepository.GetByName(product.Category.Name)
                ?? new Category { Name = product.Category.Name };

            existingProduct.Name = product.Name ?? existingProduct.Name;
            existingProduct.ShortDescription = product.ShortDescription ?? existingProduct.ShortDescription;
            existingProduct.LongDescription = product.LongDescription ?? existingProduct.LongDescription;
            existingProduct.ImagePath = product.ImagePath ?? existingProduct.ImagePath;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            existingProduct.Category = category;
            existingProduct.Genres = finalGenres;

            await productRepository.Edit(existingProduct);

            return existingProduct;
        }

        public async Task<PagedResult<Product>> GetPagedProducts(int page, int pageSize)
        {
            return await productRepository.GetPaged(page, pageSize);
        }
    }
}
