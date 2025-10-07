using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.Services;
using Serilog;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // "/students
    public class EnrollmentController : ControllerBase
    {
        // Fields
        private readonly ILogger<EnrollmentController> _logger;
        private readonly IStudentService _service;

        // Constructor
        public EnrollmentController(ILogger<EnrollmentController> logger, IStudentService service)
        {
            _logger = logger;
            _service = service;
        }

        // Methods

        // Enroll Student In Course
        [Authorize(Roles = "Student")]
        [Authorize(Roles = "Instructor")]
        [HttpPost("{studentId}/{courseId}", Name = "EnrollStudentInCourse")]
        public async Task<IActionResult> EnrollAsync(string studentId, int courseId)
        {
            _logger.LogInformation("Enrolling student {studentId} in course {courseId}", studentId, courseId);
            await _service.EnrollAsync(studentId, courseId);
            return Ok();
        }
    }
}