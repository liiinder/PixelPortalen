namespace PixelPortalen.API.Repositories.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> Get(int id);
    Task<IEnumerable<TEntity>> GetAll();
    IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);
    Task Add(TEntity entity);
    Task Remove(TEntity entity);
    Task Edit(TEntity entity);
}