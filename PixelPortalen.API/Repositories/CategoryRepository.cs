using PixelPortalen.API.Data;
using PixelPortalen.API.Repositories.Interfaces;
using PixelPortalen.Shared.Models;

namespace PixelPortalen.API.Repositories;
public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private APIDbContext? categoryContext => context as APIDbContext;

    public CategoryRepository(APIDbContext context) : base(context)
    {
    }

    public Category? GetByName(string name)
    {
        return categoryContext.Categories.FirstOrDefault(g => g.Name == name);
    }
}