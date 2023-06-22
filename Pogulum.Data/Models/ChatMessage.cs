using System.ComponentModel.DataAnnotations;

namespace Pogulum.Data.Models;

public class ChatMessage
{
    [Key]
    public int Id { get; set; }

    public string Content { get; set; }

    public User User { get; set; }

    public DateTime CreatedAt { get; set; }
}