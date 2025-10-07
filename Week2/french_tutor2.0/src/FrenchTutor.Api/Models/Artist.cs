using System.ComponentModel.DataAnnotations;

namespace FrenchTutor.Api.Models;

public class Artist
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = default!;

    [MaxLength(80)]
    public string? Country { get; set; }

    [MaxLength(2000)]
    public string? Bio { get; set; }

    public List<Song> Songs { get; set; } = new();
}
