using System.ComponentModel.DataAnnotations;

namespace FrenchTutor.Api.Models;

public class Song
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = default!;

    public int? Year { get; set; }
    public string? Lyrics { get; set; }
    public string? Translation { get; set; }

    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = default!;

    public List<Term> Terms { get; set; } = new();
}
