using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Data;

public class SchoolDbContext : DbContext
{
    // Fields
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    // Methods
    // You still need a constructor!

    public SchoolDbContext( DbContextOptions<SchoolDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}