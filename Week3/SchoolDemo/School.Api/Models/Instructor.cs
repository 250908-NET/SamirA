using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.Models;

public class Instructor : User
{
    // Fields

    public List<Course> Courses { get; set; } = new();

    public Double Salary { get; set; }
}