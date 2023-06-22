using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class GameRepo : BaseRepo<Game>
{
    public GameRepo(PogulumDbContext db) : base(db)
    {
    }
}