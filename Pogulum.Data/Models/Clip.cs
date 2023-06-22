using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class Clip
{
    [Key]
    public string Id { get; set; }

    public string Url { get; set; }

    public string EmbedUrl { get; set; }

    public Broadcaster Broadcaster { get; set; }

    public Game Game { get; set; }

    public string Title { get; set; }

    public int ViewCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public string ThumbnailUrl { get; set; }

    public double Duration { get; set; }
}