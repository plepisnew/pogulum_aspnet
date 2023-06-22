using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class SavedClipRepo : BaseRepo<SavedClip>
{
    public SavedClipRepo(PogulumDbContext db) : base(db)
    {
    }
}