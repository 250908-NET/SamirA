using AutoMapper;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.DTO;
using School.Models;
using School.Services;
using School.Repositories;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

string CS = File.ReadAllText("../connection_string.env");

// Add services to the container...
// Controller Classes
builder.Services.AddControllers(); // let's add the controller classes as well...

// some dependencies for cookies! 
builder.Services.AddHttpContextAccessor();

// DTO to Model Mapping
builder.Services.AddAutoMapper(typeof(Program));

// debugging and openapi documentation
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: **Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...**"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// database context for DI
builder.Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(CS));

// identity service
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.AllowedForNewUsers = true;
    options.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<SchoolDbContext>()
.AddDefaultTokenProviders();

// Jwt Auth
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JwtSettings.SecretKey is missing in appsettings.json!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddAuthorization( options =>
{
    options.AddPolicy("Students", policy => policy.RequireClaim("Student"));
    options.AddPolicy("Instructors", policy => policy.RequireClaim("Instructor"));
    options.AddPolicy("SysAdmin", policy => policy.RequireClaim("SysAdmin"));
});

// repository classes for DI
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

// service classes for DI
builder.Services.AddScoped<ILanguagePreferenceService, LanguagePreferenceService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ICourseService, CourseService>();

// configure JSON serialization, but we're changing to DTOs now.
// builder.Services.ConfigureHttpJsonOptions(options=>
// {
//     options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//     options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
// });

// configure logger
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger(); // read from appsettings.json
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// -------------------------- ROOT -------------------------- //
app.MapGet("/", () => {
    return "Hello world";
});

// With the controller classes, all of these methods will be handled by our controllers and can be "removed" for now.

// -------------------------- STUDENT -------------------------- //
// app.MapGet("/students", async (ILogger<Program> logger, IStudentService service) => 
// {
//     logger.LogInformation("Getting all students");
//     return Results.Ok(await service.GetAllAsync());
// });

// app.MapGet("/students/{id}", async (ILogger<Program> logger, IStudentService service, int id) =>
// {
//     logger.LogInformation("Getting student {id}", id);
//     var student = await service.GetByIdAsync(id);
//     return student is not null ? Results.Ok(student) : Results.NotFound();
// });

// app.MapPost("/students", async (ILogger<Program> logger, IStudentService service, Student student) =>
// {
//     logger.LogInformation("Creating student");
//     Student createdStudent = await service.CreateAsync(student);
//     return Results.Created($"/students/{student.Id}", createdStudent);
// });

// app.MapPut("/students/{id}", async (ILogger<Program> logger, IStudentService service, int id, Student student) =>
// {
//     logger.LogInformation("Updating student {id}", id);
//     if (! await service.Exists(id)) 
//     {
//         return Results.BadRequest();
//     }

//     await service.UpdateAsync(id, student);
//     return Results.Ok(await service.GetByIdAsync(id));
// });

// app.MapDelete("/students/{id}", async (ILogger<Program> logger, IStudentService service, int id) =>
// {
//     logger.LogInformation("Deleting student {id}", id);
//     await service.DeleteAsync(id);
//     return Results.NoContent();
// });


// -------------------------- INSTRUCTOR -------------------------- //
// app.MapGet("/instructors", async (ILogger<Program> logger, IInstructorService service) => 
// {
//     logger.LogInformation("Getting all instructors");
//     return Results.Ok(await service.GetAllAsync());
// });

// app.MapGet("/instructors/{id}", async (ILogger<Program> logger, IInstructorService service, int id) =>
// {
//     logger.LogInformation("Getting instructor {id}", id);
//     var instructor = await service.GetByIdAsync(id);
//     return instructor is not null ? Results.Ok(instructor) : Results.NotFound();
// });

// app.MapPost("/instructors", async (ILogger<Program> logger, IInstructorService service, Instructor instructor) =>
// {
//     logger.LogInformation("Creating instructor");
//     await service.CreateAsync(instructor);
//     return Results.Created($"/instructors/{instructor.Id}", instructor);
// });

// app.MapPut("/instructors/{id}", async (ILogger<Program> logger, IInstructorService service, int id, Instructor instructor) =>
// {
//     logger.LogInformation("Updating instructor {id}", id);
//     if (! await service.Exists(id)) 
//     {
//         return Results.BadRequest();
//     }

//     await service.UpdateAsync(id, instructor);
//     return Results.Ok(await service.GetByIdAsync(id));
// });

// app.MapDelete("/instructors/{id}", async (ILogger<Program> logger, IInstructorService service, int id) =>
// {
//     logger.LogInformation("Deleting instructor {id}", id);
//     await service.DeleteAsync(id);
//     return Results.NoContent();
// });


// -------------------------- COURSE -------------------------- //
// app.MapGet("/courses", async (ILogger<Program> logger, ICourseService service) => 
// {
//     logger.LogInformation("Getting all courses");
//     return Results.Ok(await service.GetAllAsync());
// });

// app.MapGet("/courses/{id}", async (ILogger<Program> logger, ICourseService service, int id) =>
// {
//     logger.LogInformation("Getting course {id}", id);
//     var course = await service.GetByIdAsync(id);
//     return course is not null ? Results.Ok(course) : Results.NotFound();
// });

// app.MapPost("/courses", async (ILogger<Program> logger, ICourseService service, Course course) =>
// {
//     logger.LogInformation("Creating course");
//     var createdCourse = await service.CreateAsync(course);
//     return Results.Created($"/courses/{createdCourse.Id}", createdCourse);
// });

// app.MapPut("/courses/{id}", async (ILogger<Program> logger, ICourseService service, int id, Course course) =>
// {
//     logger.LogInformation("Updating course {id}", id);
//     if (! await service.Exists(id)) 
//     {
//         return Results.BadRequest();
//     }

//     await service.UpdateAsync(id, course);
//     return Results.Ok(await service.GetByIdAsync(id));
// });

// app.MapDelete("/courses/{id}", async (ILogger<Program> logger, ICourseService service, int id) =>
// {
//     logger.LogInformation("Deleting course {id}", id);
//     await service.DeleteAsync(id);
//     return Results.NoContent();
// });


// -------------------------- ENROLLMENT -------------------------- //
// app.MapPost("/enrollments/{studentId}/{courseId}", async (ILogger<Program> logger, IStudentService studentService, int studentId, int courseId) =>
// {
//     logger.LogInformation("Enrolling student {studentId} in course {courseId}", studentId, courseId);
//     await studentService.EnrollAsync(studentId, courseId);
//     return Results.Ok();
// });

// To complete our controller implementation, we need to let the framework know that we want to use the controllers.
app.MapControllers(); 


// I want to add a default SysAdmin to the system, so that I don't have to go through the UI to create one.
// I don't want to leave the ability to cretae a sysadmin open to the public, and only an admin should be able to create one.
// Which presents a "chicken or the egg" situation for us. This is just one solution, but there are others.
// (The other solution is to create a migration that creates the sysadmin, and then run it.)

await SeedAdmin(app);

app.Run();


static async Task SeedAdmin(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            // Check if admin already exists
            var existingAdmin = await userManager.FindByEmailAsync("admin@school.edu");
            if (existingAdmin != null)
            {
                logger.LogInformation("SysAdmin user already exists");
                return;
            }

            // Create SysAdmin role if it doesn't exist
            logger.LogInformation("No SysAdmin user found, Creating SysAdmin role");
            if (!await roleManager.RoleExistsAsync("SysAdmin"))
            {
                await roleManager.CreateAsync(new IdentityRole("SysAdmin"));
                logger.LogInformation("Created SysAdmin role");
            }

            // Create admin user
            var sysAdmin = new SysAdmin
            {
                UserName = "admin@school.edu",
                Email = "admin@school.edu",
                FirstName = "Sys",
                LastName = "Admin",
                EmailConfirmed = true // Optional: auto-confirm email
            };

            var result = await userManager.CreateAsync(sysAdmin, "Admin123!");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(sysAdmin, "SysAdmin");
                logger.LogInformation("SysAdmin user created successfully");
            }
            else
            {
                logger.LogError("Failed to create SysAdmin: {Errors}", 
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database");
        }
    }
}