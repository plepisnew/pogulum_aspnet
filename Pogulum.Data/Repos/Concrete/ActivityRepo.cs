using Microsoft.EntityFrameworkCore;
using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class ActivityRepo : BaseRepo<Activity>
{
    private readonly PogulumDbContext _db;

    public ActivityRepo(PogulumDbContext db) : base(db)
    {
        _db = db;
    }

    public override async Task<List<Activity>> GetAsync()
    {
        return _db.Activities
            .Include(a => a.Actor)
            .Include(a => a.GameSubject)
            .Include(a => a.ClipSubject)
            .Include(a => a.BroadcasterSubject)
            .ToList();
    }

    public override async Task<Activity?> GetAsync(object id)
    {
        return await _db.Activities
            .Include(a => a.Actor)
            .Include(a => a.GameSubject)
            .Include(a => a.ClipSubject)
            .Include(a => a.BroadcasterSubject)
            .FirstAsync(a => a.Id == (int)id);
    }
}