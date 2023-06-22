using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class Game
{
    [Key]
    public string Id { get; set; }

    public string Name { get; set; }

    public string BoxArtUrl { get; set; }

    public int Popularity { get; set; } = 0;
}