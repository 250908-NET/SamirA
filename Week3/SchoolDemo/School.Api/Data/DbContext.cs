using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Models;

namespace School.Data;

public class SchoolDbContext : IdentityDbContext<User>
{
    // Fields
    public DbSet<SysAdmin> SysAdmins { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    // Methods
    // You still need a constructor!

    public SchoolDbContext( DbContextOptions<SchoolDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
        .HasDiscriminator<string>("UserType")
        .HasValue<Student>("Student")
        .HasValue<Instructor>("Instructor")
        .HasValue<SysAdmin>("SysAdmin");
            
        // Course configuration
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(200);
                
            entity.Property(e => e.Description)
                .HasMaxLength(1000);
                
            entity.HasOne(e => e.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(e => e.InstructorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        });

           // Student-Course many-to-many relationship
        modelBuilder.Entity<Student>(entity =>
        {
            
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity<Dictionary<string, object>>(
                "StudentCourse",  // Junction table name
                j => j.HasOne<Course>().WithMany().HasForeignKey("CourseId"),
                j => j.HasOne<Student>().WithMany().HasForeignKey("StudentId"),
                j =>
                {
                    j.HasKey("StudentId", "CourseId");
                    j.ToTable("StudentCourses");
                });
        });

        // Instructor configuration
        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.HasMany(i => i.Courses)
                .WithOne(c => c.Instructor)
                .HasForeignKey(c => c.InstructorId)
                .IsRequired(false);
        });

         modelBuilder.Entity<SysAdmin>(entity =>
        {
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
                
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);
        });
    
    }
}