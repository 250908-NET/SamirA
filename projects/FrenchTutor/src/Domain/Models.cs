namespace Domain;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Song> Songs { get; set; } = new List<Song>();
}

public class Song
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public int? Year { get; set; }

    // Content
    public string LyricsFr { get; set; } = "";          // store full French lyrics (paste your own content)
    public string TranslationEn { get; set; } = "";     // full English translation

    // Artist
    public int ArtistId { get; set; }
    public Artist Artist { get; set; } = default!;

    // m-m: Song <-> Expression
    public ICollection<Expression> Expressions { get; set; } = new List<Expression>();
}

public class Expression
{
    public int Id { get; set; }
    public string Phrase { get; set; } = default!;  // e.g., "Ne me quitte pas"
    public string Meaning { get; set; } = default!; // e.g., idiomatic translation/explanation

    public ICollection<Song> Songs { get; set; } = new List<Song>();
}
