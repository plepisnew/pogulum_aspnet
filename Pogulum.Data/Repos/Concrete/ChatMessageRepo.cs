using Pogulum.Data.Models;

namespace Pogulum.Data.Repos.Concrete;

public class ChatMessageRepo : BaseRepo<ChatMessage>
{
    public ChatMessageRepo(PogulumDbContext db) : base(db)
    {
    }
}