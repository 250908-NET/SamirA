using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FrenchTutor.Api.Models;

namespace FrenchTutor.Api.Data;

public static class SeedData
{
    public static async Task SeedAsync(FrenchTutorDbContext db)
    {
        // Idempotent seed
        if (await db.Songs.AnyAsync()) return;

        // ---------- Artists ----------
        var artists = new List<Artist>
        {
            new() { Name = "Édith Piaf", Country = "France", Bio = "Iconic French chanteuse and cultural symbol." },
            new() { Name = "Jacques Brel", Country = "Belgium", Bio = "Poet of the chanson; intense, theatrical delivery." },
            new() { Name = "Georges Brassens", Country = "France", Bio = "Wry, literate songwriter with a guitar and wit." },
            new() { Name = "Charles Aznavour", Country = "France", Bio = "Chanson legend blending drama and tenderness." },
            new() { Name = "Joe Dassin", Country = "France", Bio = "American-French singer of sunny 60s/70s pop." },
            new() { Name = "Françoise Hardy", Country = "France", Bio = "Yé-yé icon with introspective, cool delivery." },
            new() { Name = "Yves Montand", Country = "France", Bio = "Charming actor-singer; classic Parisian flavor." },
            new() { Name = "Serge Gainsbourg", Country = "France", Bio = "Provocateur and wordplay master of chanson-pop." },
            new() { Name = "Barbara", Country = "France", Bio = "Deeply personal, poetic singer-songwriter." },
            new() { Name = "Michel Sardou", Country = "France", Bio = "Big-voiced singer of emotive 70s hits." },
            new() { Name = "Michel Polnareff", Country = "France", Bio = "Baroque pop craftsman with lush melodies." },
            new() { Name = "France Gall", Country = "France", Bio = "Sparkling yé-yé star; Eurovision winner." },
            new() { Name = "Dalida", Country = "France", Bio = "Egyptian-Italian-French icon of timeless pop." },
            new() { Name = "Charles Trenet", Country = "France", Bio = "The ‘singing fool’; jaunty, poetic imagery." }
        };

        db.Artists.AddRange(artists);
        await db.SaveChangesAsync();

        var artistId = await db.Artists.ToDictionaryAsync(a => a.Name, a => a.Id);

        // ---------- Songs (Lyrics left null to avoid copyright; Translation is an original summary) ----------
        var songs = new List<Song>
        {
            new() {
                Title = "La vie en rose", Year = 1947, ArtistId = artistId["Édith Piaf"],
                Lyrics = null,
                Translation = "A romantic declaration: love makes everything glow; life turns rosy in a lover’s arms."
            },
            new() {
                Title = "Non, je ne regrette rien", Year = 1960, ArtistId = artistId["Édith Piaf"],
                Lyrics = null,
                Translation = "A bold reset: no regrets about the past; only the present love matters."
            },
            new() {
                Title = "Hymne à l’amour", Year = 1950, ArtistId = artistId["Édith Piaf"],
                Lyrics = null,
                Translation = "A vow of limitless devotion, promising to face any trial for love."
            },
            new() {
                Title = "Ne me quitte pas", Year = 1959, ArtistId = artistId["Jacques Brel"],
                Lyrics = null,
                Translation = "A desperate plea to a departing lover, promising wonders to win them back."
            },
            new() {
                Title = "Amsterdam", Year = 1964, ArtistId = artistId["Jacques Brel"],
                Lyrics = null,
                Translation = "A gritty portrait of sailors in port—excess, yearning, and raw humanity."
            },
            new() {
                Title = "Les copains d’abord", Year = 1964, ArtistId = artistId["Georges Brassens"],
                Lyrics = null,
                Translation = "A celebration of loyal friendship; a boat named ‘Friends First’ symbolizes camaraderie."
            },
            new() {
                Title = "La mauvaise réputation", Year = 1952, ArtistId = artistId["Georges Brassens"],
                Lyrics = null,
                Translation = "Defiant satire of social judgment; a stubborn nonconformist keeps his course."
            },
            new() {
                Title = "La Bohème", Year = 1965, ArtistId = artistId["Charles Aznavour"],
                Lyrics = null,
                Translation = "Bittersweet nostalgia for starving-artist youth in Montmartre, rich in memories."
            },
            new() {
                Title = "Les Champs-Élysées", Year = 1969, ArtistId = artistId["Joe Dassin"],
                Lyrics = null,
                Translation = "A carefree stroll on Paris’ avenue; chance meetings and sunny optimism."
            },
            new() {
                Title = "Tous les garçons et les filles", Year = 1962, ArtistId = artistId["Françoise Hardy"],
                Lyrics = null,
                Translation = "Lonely youth observing couples; longing for love’s first true connection."
            },
            new() {
                Title = "Sous le ciel de Paris", Year = 1951, ArtistId = artistId["Yves Montand"],
                Lyrics = null,
                Translation = "A poetic ode to Paris’ sky and the small dramas of life beneath it."
            },
            new() {
                Title = "La Javanaise", Year = 1963, ArtistId = artistId["Serge Gainsbourg"],
                Lyrics = null,
                Translation = "Playful word-jazz and sensuality; a slow dance of love and language."
            },
            new() {
                Title = "L’Aigle noir", Year = 1970, ArtistId = artistId["Barbara"],
                Lyrics = null,
                Translation = "A dream-vision of a black eagle; memory, trauma, and rebirth intertwine."
            },
            new() {
                Title = "La Maladie d’amour", Year = 1973, ArtistId = artistId["Michel Sardou"],
                Lyrics = null,
                Translation = "Love portrayed as a ‘sickness’ carried through generations, catchy and grand."
            },
            new() {
                Title = "Love Me, Please Love Me", Year = 1966, ArtistId = artistId["Michel Polnareff"],
                Lyrics = null,
                Translation = "Vulnerable confession: pleading for reciprocated love with orchestral sweep."
            },
            new() {
                Title = "Poupée de cire, poupée de son", Year = 1965, ArtistId = artistId["France Gall"],
                Lyrics = null,
                Translation = "A pop idol questions being molded by others, seeking an authentic voice."
            },
            new() {
                Title = "Paroles, paroles", Year = 1973, ArtistId = artistId["Dalida"],
                Lyrics = null,
                Translation = "A duet about empty promises; one partner is tired of charming talk."
            },
            new() {
                Title = "La mer", Year = 1946, ArtistId = artistId["Charles Trenet"],
                Lyrics = null,
                Translation = "A shimmering hymn to the sea—ever-changing light, motion, and joy."
            }
        };

        db.Songs.AddRange(songs);

        // ---------- Optional: core vocabulary terms ----------
        var terms = new List<Term>
        {
            new() { French = "amour",     English = "love",      Notes = "noun" },
            new() { French = "cœur",      English = "heart",     Notes = "noun; figurative feelings" },
            new() { French = "regret",    English = "regret",    Notes = "noun/verb idea" },
            new() { French = "rose",      English = "pink/rose", Notes = "color / rose imagery" },
            new() { French = "ciel",      English = "sky",       Notes = "noun; poetic usage" },
            new() { French = "mer",       English = "sea",       Notes = "noun; natural imagery" },
            new() { French = "ami",       English = "friend",    Notes = "masc.; amie = fem." },
            new() { French = "paroles",   English = "words",     Notes = "lyrics/empty talk" },
            new() { French = "souvenir",  English = "memory",    Notes = "noun; remembrance" },
            new() { French = "tristesse", English = "sadness",   Notes = "noun; emotion" },
            new() { French = "joie",      English = "joy",       Notes = "noun; emotion" },
            new() { French = "liberté",   English = "freedom",   Notes = "noun; concept" }
        };
        db.Terms.AddRange(terms);

        await db.SaveChangesAsync();

        // ---------- Optional: link some terms to a few songs ----------
        var termMap = await db.Terms.ToDictionaryAsync(t => t.French, t => t);
        var songMap = await db.Songs.ToDictionaryAsync(s => s.Title, s => s);

        void Link(string songTitle, params string[] frenchTerms)
        {
            if (!songMap.TryGetValue(songTitle, out var s)) return;
            foreach (var ft in frenchTerms)
                if (termMap.TryGetValue(ft, out var t) && !s.Terms.Any(x => x.Id == t.Id))
                    s.Terms.Add(t);
        }

        Link("La vie en rose", "amour", "rose", "joie");
        Link("Non, je ne regrette rien", "regret", "liberté");
        Link("Hymne à l’amour", "amour", "cœur");
        Link("Ne me quitte pas", "tristesse", "amour");
        Link("Sous le ciel de Paris", "ciel", "joie");
        Link("La mer", "mer", "joie");
        Link("Les copains d’abord", "ami", "joie");
        Link("Paroles, paroles", "paroles");

        await db.SaveChangesAsync();
    }
}
