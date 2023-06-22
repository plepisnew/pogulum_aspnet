namespace Pogulum.Data.Repos;

public interface IBaseRepo<TEntity>
{
    Task CreateAsync(TEntity entity);

    Task<List<TEntity>> GetAsync();

    Task<TEntity?> GetAsync(object id);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(object id);
}