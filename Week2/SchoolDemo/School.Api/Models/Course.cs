using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public class Course
{
    // Fields

    public int Id { get; set; }

    [Required, MaxLength(50)]
    public string Title { get; set; }

    [Required, MaxLength(50)]
    public string Description { get; set; }

    public Instructor Instructor { get; set; } // EF Core will recognize the other Models/tables for the references

    public List<Student> Students { get; set; } = new();
}