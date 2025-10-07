using Microsoft.EntityFrameworkCore;
using FrenchTutor.Api.Models;

namespace FrenchTutor.Api.Data;

public class FrenchTutorDbContext : DbContext
{
    public FrenchTutorDbContext(DbContextOptions<FrenchTutorDbContext> options) : base(options) { }

    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Song>   Songs   => Set<Song>();
    public DbSet<Term>   Terms   => Set<Term>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Song>()
            .HasOne(s => s.Artist)
            .WithMany(a => a.Songs)
            .HasForeignKey(s => s.ArtistId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Song>()
            .HasMany(s => s.Terms)
            .WithMany(t => t.Songs)
            .UsingEntity(j => j.ToTable("SongTerm"));
    }
}
