using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using School.DTO;
using School.Services;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _logger = logger;
        }

        [HttpPost("register/student")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDto dto)
        {
            var student = new Student
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(student, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Ensure "Student" role exists
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Student"));
            }

            await _userManager.AddToRoleAsync(student, "Student");

            _logger.LogInformation("Student registered: {Email}", dto.Email);

            return Ok(new { message = "Student registered successfully" });
        }

        [HttpPost("register/instructor")]
        public async Task<IActionResult> RegisterInstructor([FromBody] RegisterInstructorDto dto)
        {
            var instructor = new Instructor
            {
                UserName = dto.Email,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(instructor, dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Ensure "Instructor" role exists
            if (!await _roleManager.RoleExistsAsync("Instructor"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Instructor"));
            }

            await _userManager.AddToRoleAsync(instructor, "Instructor");

            _logger.LogInformation("Instructor registered: {Email}", dto.Email);

            return Ok(new { message = "Instructor registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateToken(user, roles);

            _logger.LogInformation("User logged in: {Email}", dto.Email);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Roles = roles.ToList()
            });
        }
    }
}