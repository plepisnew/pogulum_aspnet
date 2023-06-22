using Microsoft.EntityFrameworkCore;
using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class ClipRepo : BaseRepo<Clip>
{
    private readonly PogulumDbContext _db;

    public ClipRepo(PogulumDbContext db) : base(db)
    {
        _db = db;
    }

    public override async Task<Clip?> GetAsync(object id)
    {
        return await _db.Clips.Include(c => c.Broadcaster).Include(c => c.Game).FirstOrDefaultAsync(c => c.Id == (string)id);
    }

    public override async Task<List<Clip>> GetAsync()
    {
        return await _db.Clips.Include(c => c.Broadcaster).Include(c => c.Game).ToListAsync();
    }
}