using Microsoft.EntityFrameworkCore;
using Pogulum.Data.Models;

public class PogulumDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DbSet<Broadcaster> Broadcasters { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Clip> Clips { get; set; }

    public DbSet<SavedClip> SavedClips { get; set; }

    public DbSet<Activity> Activities { get; set; }

    public PogulumDbContext(DbContextOptions<PogulumDbContext> options) : base(options)
    {
    }
}