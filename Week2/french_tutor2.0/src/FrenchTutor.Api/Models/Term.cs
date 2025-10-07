using System.ComponentModel.DataAnnotations;

namespace FrenchTutor.Api.Models;

public class Term
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string French { get; set; } = default!;

    [Required, MaxLength(200)]
    public string English { get; set; } = default!;

    [MaxLength(500)]
    public string? Notes { get; set; }

    public List<Song> Songs { get; set; } = new();
}
