using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Course_Manager.Controllers
{
    public class ClassesController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Classes
        public ActionResult Index()
        {
            ClassesViewModel model = new ClassesViewModel()
            {
                Classes = _context.Classes.Include("Course").ToList(),
                Staff = _context.Staff.Where(s => _context.Classes.Select(c => c.Instructor.Id).Contains(s.Id)).ToList()
            };

            return View(model);
        }
        public ActionResult Details(int id)
        {
            Class Class = _context.Classes.SingleOrDefault(c => c.Id == id);

            if (Class == null)
                return new HttpNotFoundResult();

            List<Student> students = _context.Students.ToList().Where(s => s.Classes.Contains(Class)).ToList();
            StaffMember instructor = _context.Staff.ToList().SingleOrDefault(s => s.Id == Class.Instructor.Id);
            Course course = _context.Courses.ToList().SingleOrDefault(c => c.Id == Class.course.Id);

            ClassDetailsViewModel model = new ClassDetailsViewModel
            {
                Block = Class.Block,
                Semester = Class.Semester,
                InstructorName = instructor.FullName,
                CourseName = course.Name,
                StudentsInClass = students
            };

            return View("Details", model);
        }
        public ActionResult New(Class Class = null)
        {
            NewClassViewModel model = new NewClassViewModel()
            {
                Class = Class,
                Courses = _context.Courses.ToList(),
                Staff = _context.Staff.ToList()
            };

            if (Class == null)
                model.Class = new Class();

            return View("ClassForm", model);
        }
        public ActionResult Save(Class Class)
        {
            Class.Instructor = _context.Staff.SingleOrDefault(s => s.Id == Class.InstructorId);
            Class.course = _context.Courses.SingleOrDefault(c => c.Id == Class.CourseId);
            Class classInDb = _context.Classes.SingleOrDefault(c => c.Id == Class.Id);
            if (!ModelState.IsValid)
            {
                NewClassViewModel model = new NewClassViewModel()
                {
                    Class = Class,
                    Courses = _context.Courses.ToList(),
                    Staff = _context.Staff.ToList()
                };

                return View("ClassForm", model);
            }

            if (classInDb == null)
                _context.Classes.Add(Class);
            else
            {
                foreach (PropertyInfo property in typeof(Class).GetProperties())
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(classInDb, property.GetValue(Class, null), null);
                    }
                }
            }

            _context.SaveChanges();

            ClassesViewModel classesViewModel = new ClassesViewModel()
            {
                Classes = _context.Classes.Include("Course").ToList(),
                Staff = _context.Staff.Where(s => _context.Classes.Select(c => c.Instructor.Id).Contains(s.Id)).ToList()
            };



            return View("Index", classesViewModel);
        }
        public ClassesController()
        {
            _context = new ApplicationDbContext();
        }
    }
}