using Domain;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class FrenchTutorDbContext(DbContextOptions<FrenchTutorDbContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Expression> Expressions => Set<Expression>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>().HasIndex(a => a.Name).IsUnique();

        modelBuilder.Entity<Song>()
            .HasOne(s => s.Artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Song>()
            .HasMany(s => s.Expressions)
            .WithMany(e => e.Songs)
            .UsingEntity(j => j.ToTable("SongExpressions"));

        
        modelBuilder.Entity<Artist>().HasData(
            new Artist { Id = 1, Name = "Édith Piaf" },
            new Artist { Id = 2, Name = "Jacques Brel" },
            new Artist { Id = 3, Name = "Georges Brassens" }
        );

        modelBuilder.Entity<Song>().HasData(
            new Song { Id = 1, Title = "La Vie en rose", Year = 1947, LyricsFr = "Quand il me prend...", TranslationEn = "When he takes me...", ArtistId = 1 },
            new Song { Id = 2, Title = "Ne me quitte pas", Year = 1959, LyricsFr = "Ne me quitte pas...", TranslationEn = "Don't leave me...", ArtistId = 2 },
            new Song { Id = 3, Title = "Les Copains d'abord", Year = 1964, LyricsFr = "Non, ce n'était pas...", TranslationEn = "No, it wasn’t a raft...", ArtistId = 3 }
        );
    }
}
