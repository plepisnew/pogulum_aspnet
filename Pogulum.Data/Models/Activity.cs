using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class Activity
{
    [Key]
    public int Id { get; set; }

    public User Actor { get; set; }

    public Game? GameSubject { get; set; }

    public Clip? ClipSubject { get; set; }

    public Broadcaster BroadcasterSubject { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}