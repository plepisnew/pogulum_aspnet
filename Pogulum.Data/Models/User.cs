using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [MinLength(4)]
    [MaxLength(20)]
    public required string Username { get; set; }

    public required string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int ClipCount { get; set; } = 0;

    public string ProfilePictureSrc { get; set; } = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

    public List<Clip> FavoriteClips { get; set; } = new List<Clip>();

    public List<Game> FavoriteGames { get; set; } = new List<Game>();

    public List<Broadcaster> FavoriteBroadcasters { get; set; } = new List<Broadcaster>();

    public List<SavedClip> SavedClips { get; set; } = new List<SavedClip>();
}