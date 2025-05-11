using PixelPortalen.API.Repositories;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository catRepo)
        {
            categoryRepository = catRepo;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await categoryRepository.GetAll();
        }
        
        public async Task AddCategory(Category category)
        {
            await categoryRepository.Add(category);
        }

        // WARNING CASCADE DELETE
        //public async Task RemoveCategory(int id)
        //{
        //    var category = await categoryRepository.Get(id);
        //    await categoryRepository.Remove(category);
        //}

        public async Task<Category?> EditCategory(int id, string nameOfCategory)
        {
            var existingCategory = await categoryRepository.Get(id);

            if (existingCategory is null) return null;

            existingCategory.Name = nameOfCategory;
            await categoryRepository.Edit(existingCategory);

            return existingCategory;
        }
    }
}
