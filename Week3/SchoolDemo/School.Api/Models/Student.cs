using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // to customize the column names

namespace School.Models;

public class Student : User
{
    // Fields
    
    public List<Course> Courses { get; set; } = new();
}