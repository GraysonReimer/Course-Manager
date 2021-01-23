using AutoMapper;
using Course_Manager.Controllers.DTOs;
using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<Student, StudentDto>();
            Mapper.CreateMap<StudentDto, Student>();
            Mapper.CreateMap<Course, CourseDto>();
            Mapper.CreateMap<CourseDto, Course>();
        }
    }
}