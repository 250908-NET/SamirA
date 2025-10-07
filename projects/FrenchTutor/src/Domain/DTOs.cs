namespace Domain;

public record SongCreateDto(string Title, int? Year, int ArtistId, string LyricsFr, string TranslationEn);
public record SongReadDto(int Id, string Title, int? Year, string ArtistName);
public record ExpressionDto(int Id, string Phrase, string Meaning);
