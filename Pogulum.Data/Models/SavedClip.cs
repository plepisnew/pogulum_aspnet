using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class SavedClip
{
    [Key]
    public int Id { get; set; }

    public List<Clip> Clips { get; set; }

    // 1:20,2:30,9:12
    public string ClipDurations { get; set; }

    public User Creator { get; set; }
}