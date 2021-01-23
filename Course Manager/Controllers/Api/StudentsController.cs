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
    public class StudentsController : ApiController
    {
        // GET: Students

        ApplicationDbContext _context;

        // GET /api/students/1
        public IHttpActionResult GetStudent(int id)
        {
            Student student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return BadRequest("Id not valid");

            return Ok(Mapper.Map<Student, StudentDto>(student));
        }

        // GET /api/students
        public IHttpActionResult GetStudents()
        {
            List<StudentDto> studentDtos = _context.Students.Select(s => Mapper.Map<Student, StudentDto>(s)).ToList();



            return Ok(studentDtos);
        }

        // PUT /api/students/1
        [HttpPut]
        public IHttpActionResult UpdateStudent(int id, StudentDto dto)
        {
            Student student = _context.Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            Mapper.Map(student, dto);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/students/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Student student = _context.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            var user = _context.Users.SingleOrDefault(u => u.Id == student.UserId);

            if (user != null)
                _context.Users.Remove(user);

            _context.Students.Remove(student);

            _context.SaveChanges();

            return Ok();
        }

        // POST /api/students
        [HttpPost]
        public IHttpActionResult CreateStudent(StudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Student not valid");
            }

            Student NewStudent = Mapper.Map<StudentDto, Student>(student);

            _context.Students.Add(NewStudent);

            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + NewStudent.Id), student);
        }

        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }

    }
}
