using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class Broadcaster
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int ViewCount { get; set; }

    public string ProfileImageUrl { get; set; }

    public string OfflineImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public int Popularity { get; set; } = 0;
}