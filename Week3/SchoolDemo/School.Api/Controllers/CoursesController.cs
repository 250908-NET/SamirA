using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using School.DTO;
using School.Models;
using School.Services;
using Serilog;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // "/students
    public class CoursesController : ControllerBase
    {
        // Fields
        private readonly ILogger<CoursesController> _logger;
        private readonly IMapper _mapper;
        private readonly ICourseService _service;

        // Constructor
        public CoursesController(ILogger<CoursesController> logger, IMapper mapper, ICourseService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        // Methods

        // Get All
        [HttpGet(Name = "GetAllCourses")]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("Getting all courses");
            //return Ok(await _service.GetAllAsync());
            return Ok(_mapper.Map<List<CourseDTO>>(await _service.GetAllAsync()));
        }

        // Get By Id
        [HttpGet("{id}", Name = "GetCourseById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _logger.LogInformation("Getting course {id}", id);
            var course = await _service.GetByIdAsync(id);
            return course is not null ? Ok(_mapper.Map<CourseDTO>(course)) : NotFound();
        }

        // Create
        [HttpPost(Name = "CreateCourse")]
        public async Task<IActionResult> CreateAsync(Course course)
        {
            _logger.LogInformation("Creating course");
            var createdCourse = await _service.CreateAsync(course);
            return Created($"/courses/{createdCourse.Id}", _mapper.Map<CourseDTO>(createdCourse));
        }

        // Update
        [HttpPut("{id}", Name = "UpdateCourse")]
        public async Task<IActionResult> UpdateAsync(int id, Course course)
        {
            _logger.LogInformation("Updating course {id}", id);
            if (! await _service.Exists(id)) 
            {
                return BadRequest();
            }

            await _service.UpdateAsync(id, course);
            return Ok(_mapper.Map<CourseDTO>(await _service.GetByIdAsync(id)));
        }

        // Delete
        [HttpDelete("{id}", Name = "DeleteCourse")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _logger.LogInformation("Deleting course {id}", id);
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}