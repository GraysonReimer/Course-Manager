using AutoMapper;
using Course_Manager.Controllers.DTOs;
using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Course_Manager.Controllers.Api
{
    public class CoursesController : ApiController
    {
        ApplicationDbContext _context;

        // GET /api/courses/1
        public IHttpActionResult GetCourse(int id)
        {
            Course course = _context.Courses.FirstOrDefault(s => s.Id == id);

            if (course == null)
                return BadRequest("Id not valid");

            return Ok(Mapper.Map<Course, CourseDto>(course));
        }

        // GET /api/courses
        public IHttpActionResult GetCourses()
        {
            List<CourseDto> courseDtos = _context.Courses.Select(c => Mapper.Map<Course, CourseDto>(c)).ToList();



            return Ok(courseDtos);
        }

        // PUT /api/courses/1
        [HttpPut]
        public IHttpActionResult UpdateCourse(int id, CourseDto dto)
        {
            Course course = _context.Courses.FirstOrDefault(s => s.Id == id);

            if (course == null)
                return NotFound();

            Mapper.Map(course, dto);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/courses/1
        [HttpDelete]
        public IHttpActionResult DeleteCourse(int id)
        {
            Course course = _context.Courses.FirstOrDefault(s => s.Id == id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);

            _context.SaveChanges();

            return Ok();
        }

        // POST /api/courses
        [HttpPost]
        public IHttpActionResult CreateStudent(CourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Course not valid");
            }

            Course newCourse = Mapper.Map<CourseDto, Course>(course);

            _context.Courses.Add(newCourse);

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + newCourse.Id), course);
        }

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
    }
}
