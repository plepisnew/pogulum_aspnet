using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class BroadcasterRepo : BaseRepo<Broadcaster>
{
    public BroadcasterRepo(PogulumDbContext db) : base(db)
    {
    }
}