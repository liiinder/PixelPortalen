using Microsoft.EntityFrameworkCore;
using PixelPortalen.API.Repositories.Interfaces;

namespace PixelPortalen.API.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly DbContext context;

    public Repository(DbContext context)
    {
        this.context = context;
    }

    public virtual async Task<TEntity?> Get(int id)
    {
        return context.Set<TEntity>().Find(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task Add(TEntity entity)
    {
        context.Set<TEntity>().Add(entity);
        context.SaveChanges();
    }

    public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
    {
        return context.Set<TEntity>().Where(predicate);
    }
    //TODO: Kolla om den används, annars ta bort skiten sen

    public async Task Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
        context.SaveChanges();
    }

    public async Task Edit(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        context.SaveChanges();
    }
}