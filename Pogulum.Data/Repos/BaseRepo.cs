using Microsoft.EntityFrameworkCore;
using Pogulum.Data.Models;

namespace Pogulum.Data.Repos;

public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class
{
    private readonly PogulumDbContext _db;

    public BaseRepo(PogulumDbContext db)
    {
        _db = db;
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        _db.Set<TEntity>().Add(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task<List<TEntity>> GetAsync()
    {
        return await _db.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(object id)
    {
        return await _db.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _db.Set<TEntity>().Update(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(object id)
    {
        var entity = await GetAsync(id);
        _db.Set<TEntity>().Remove(entity);

        await _db.SaveChangesAsync();
    }
}