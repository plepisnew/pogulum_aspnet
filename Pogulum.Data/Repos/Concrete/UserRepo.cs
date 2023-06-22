using Microsoft.EntityFrameworkCore;
using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class UserRepo : BaseRepo<User>
{
    private readonly PogulumDbContext _db;

    public UserRepo(PogulumDbContext db) : base(db)
    {
        _db = db;
    }

    public override async Task<List<User>> GetAsync()
    {
        return await _db.Users
            .Include(u => u.FavoriteBroadcasters)
            .Include(u => u.FavoriteClips)
            .Include(u => u.FavoriteGames)
            .ToListAsync();
    }

    public override async Task<User?> GetAsync(object id)
    {
        return await _db.Users
            .Include(u => u.FavoriteBroadcasters)
            .Include(u => u.FavoriteClips)
            .Include(u => u.FavoriteGames)
            .FirstOrDefaultAsync(u => u.Id == (int)id);
    }
}