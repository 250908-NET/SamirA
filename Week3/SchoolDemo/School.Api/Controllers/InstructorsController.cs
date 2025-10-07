using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class InstructorsController : ControllerBase
    {
        // Fields
        private readonly ILogger<InstructorsController> _logger;
        private readonly IMapper _mapper;
        private readonly IInstructorService _service;

        // Constructor
        public InstructorsController(ILogger<InstructorsController> logger, IMapper mapper, IInstructorService service)
        {
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        // Methods
        // Get All No Payroll
        [Authorize(Roles = "Student")]
        [HttpGet(Name = "GetAllInstructorsNoPayroll")]
        // "/instructors"
        public async Task<IActionResult> GetAllNoPayrollAsync()
        {

            /* If you're here for the exception handling, you're in the right place.
            *  But I have to tell you, I'm pretty lazy. So what I've done is implement
            *  some basics, so that all of my methods are technically covered, but not
            *  in the most ellegant way. I use try/catch blocks to handle exceptions,
            *  but I'm sticking to the generic Exception type.
            *  I've got a BadRequest response going out for each method if there is an
            *  exception, but it would be much better if I could return more specifics
            *  about what went wrong. I'm working on it! */
            
            // all private values are already present...
            _logger.LogInformation("Getting all instructors without payroll");
            try{
                return Ok(_mapper.Map<List<InstructorNoPayrollDTO>>(await _service.GetAllAsync()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all instructors without payroll");
                return BadRequest(ex.Message);
            }
        }

        // Get All
        [Authorize(Roles = "Instructor")]
        [Authorize(Roles = "SysAdmin")]
        [HttpGet("details", Name = "GetAllInstructors")]
        // "/instructors/details"
        public async Task<IActionResult> GetAllAsync()
        {
            // all private values are already present...
            _logger.LogInformation("Getting all instructors");
            try{
                return Ok(_mapper.Map<List<InstructorDTO>>(await _service.GetAllAsync()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all instructors");
                return BadRequest(ex.Message);
            }
        }

        // Get By Id
        [Authorize(Roles = "Instructor")]
        [Authorize(Roles = "SysAdmin")]
        [HttpGet("{id}", Name = "GetInstructorById")]
        // "/instructors/{id}"
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Getting instructor {id}", id);
            try
            {
                var instructor = await _service.GetByIdAsync(id);
                return instructor is not null ? Ok(_mapper.Map<InstructorDTO>(instructor)) : NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting instructor {id}", id);
                return BadRequest(ex.Message);
            }
        }

        // Create
        [Authorize(Roles = "SysAdmin")]
        [HttpPost(Name = "CreateInstructor")]
        // "/instructors"
        public async Task<IActionResult> CreateAsync(Instructor instructor)
        {
            _logger.LogInformation("Creating instructor");
            try
            {
                await _service.CreateAsync(instructor);
                return Created($"/instructors/{instructor.Id}", _mapper.Map<InstructorDTO>(instructor));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating instructor");
                return BadRequest(ex.Message);
            }
        }

        // Update
        [Authorize(Roles = "SysAdmin")]
        [HttpPut("{id}", Name = "UpdateInstructor")]
        // "/instructors/{id}"
        public async Task<IActionResult> UpdateAsync(string id, Instructor instructor)
        {
            _logger.LogInformation("Updating instructor {id}", id);
            try
            {
                if (! await _service.Exists(id)) 
                {
                    _logger.LogInformation("Instructor {id} does not exist", id);
                    return BadRequest();
                }

                _logger.LogInformation("Instructor {id} found, updating.", id);
                await _service.UpdateAsync(id, instructor);
                return Ok(_mapper.Map<InstructorDTO>(await _service.GetByIdAsync(id)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating instructor {id}", id);
                return BadRequest(ex.Message);
            }
        }

        // Delete
        [Authorize(Roles = "SysAdmin")]
        [HttpDelete("{id}", Name = "DeleteInstructor")]
        // "/instructors/{id}"
        public async Task<IActionResult> DeleteAsync(string id)
        {
            _logger.LogInformation("Deleting instructor {id}", id);
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting instructor {id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}