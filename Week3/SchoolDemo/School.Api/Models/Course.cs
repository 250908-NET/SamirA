using Microsoft.EntityFrameworkCore;

namespace School.Models;

public class Course
{
    // Fields

    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? InstructorId { get; set; }

    public Instructor? Instructor { get; set; } // EF Core will recognize the other Models/tables for the references

    public List<Student> Students { get; set; } = new();
}