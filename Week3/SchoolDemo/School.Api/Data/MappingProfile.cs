using AutoMapper;
using School.Models;
using School.DTO;

namespace School.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>();
            CreateMap<Instructor, InstructorDTO>();
            CreateMap<Instructor, InstructorNoPayrollDTO>();
            CreateMap<Course, CourseDTO>();
        }
    }
}
