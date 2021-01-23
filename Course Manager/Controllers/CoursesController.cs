using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Course_Manager.Controllers
{
    public class CoursesController : Controller
    {
        ApplicationDbContext _context;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult New(Course course = null)
        {
            if (course == null)
            {
                course = new Course { Name = "", GradeLevel = 0, Id = 0, CategoryId = 0, Category = new Category { Id = 0, Name = "" } };
            }
            NewCourseViewModel viewModel = new NewCourseViewModel
            {
                Course = course,
                Catergories = _context.Categories.ToList()
            };

            return View("CourseForm", viewModel);
        }
        public ActionResult Select()
        {
            string Id = User.Identity.GetUserId();
            Student student = _context.Students.SingleOrDefault(s => s.UserId == Id);

            if (student.DonePickingClasses || !_context.StudentsCanPickCourses)
                return RedirectToAction("Index");

            Outline outline = _context.Outlines.SingleOrDefault(o => o.Grade == student.Grade);

            List<Course> AvailableCourses = _context.Courses.Where(c => c.GradeLevel == student.Grade &&
                _context.Classes.Where(c2 => c2.course == c && c2.StudentsEnrolled < c2.OccupancyLimit).Count() > 0)
                .Where(c => c.CanBeAnElective && !outline.Courses.Contains(c) ).ToList();

            int coursesCount = _context.Outlines.Include("Courses").SingleOrDefault(o => o.Grade == student.Grade).Courses.Count;

            int ElectiveCount = 8 - coursesCount;
            SelectCoursesViewModel model = new SelectCoursesViewModel()
            {
                AvailableCourses = AvailableCourses,
                ElectiveCount = ElectiveCount,
                SecondChoices = new List<int>(),
                FirstChoices = new List<int>()
            };


            return View("SelectCourses", model);
        }
        public ActionResult SaveCoursesFor(SelectCoursesViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return new HttpNotFoundResult();

            List<Course> availableCourses = _context.Courses.ToList();
            string Id = User.Identity.GetUserId();
            Student student = _context.Students.Include("CourseRequests").SingleOrDefault(s => s.UserId == Id);
            List<CourseRequest> Requests = new List<CourseRequest>();
            List<CourseRequest> requests2 = student.CourseRequests; // Test Line
            for (int i = 0; i < viewModel.FirstChoices.Count; i++)
            {
                Course firstChoice = availableCourses.SingleOrDefault(c => c.Id == viewModel.FirstChoices[i]);
                Course secondChoice = availableCourses.SingleOrDefault(c => c.Id == viewModel.SecondChoices[i]);

                if (firstChoice == null || secondChoice == null)
                    return new HttpNotFoundResult();

                Requests.Add(new CourseRequest { Id = 0, FirstChoice = firstChoice, SecondChoice = secondChoice });
            }
            student.CourseRequests = Requests.ToList();
            student.DonePickingClasses = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Save(Course course)
        {
            course.Category = _context.Categories.FirstOrDefault(c => c.Id == course.CategoryId);
            Course courseInDb = _context.Courses.SingleOrDefault(c => c.Id == course.Id);
            if (!ModelState.IsValid)
            {
                NewCourseViewModel viewModel = new NewCourseViewModel
                {
                    Course = new Course { Name = "", GradeLevel = 0, Id = 0, CategoryId = 0, Category = new Category { Id=0, Name="" } },
                    Catergories = _context.Categories.Include("Category").ToList()
                };

                return View("CourseForm", viewModel);
            }
            if (courseInDb == null)
            {
                _context.Courses.Add(course);
            }
            else
            {
                foreach (PropertyInfo property in typeof(Course).GetProperties())
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(courseInDb, property.GetValue(course, null), null);
                    }
                }
            }
            

            _context.SaveChanges();

            CoursesViewModel CoursesViewModel = new CoursesViewModel()
            {
                Courses = _context.Courses.Include("Category").OrderBy(c => c.GradeLevel).ToList(),
                Classes = _context.Classes.ToList()
            };
            return View("Index", CoursesViewModel);

        }

        // GET: Courses
        public ActionResult Index()
        {
            if (User.IsInRole("CanManageWebsite"))
            {
                CoursesViewModel CoursesViewModel = new CoursesViewModel()
                {
                    Courses = _context.Courses.Include("Category").OrderBy(c => c.GradeLevel).ToList(),
                    Classes = _context.Classes.ToList()
                };
                return View(CoursesViewModel);
            }

            List<Course> availableCourses = _context.Courses.ToList();
            string Id = User.Identity.GetUserId();
            Student student = _context.Students.Include("CourseRequests").Include("Classes").SingleOrDefault(s => s.UserId == Id);

            StudentCoursesViewModel model = new StudentCoursesViewModel()
            {
                student = student,
                CanSelectCourses = _context.StudentsCanPickCourses,
                Requests = student.CourseRequests
            };

            return View("StudentCourses", model);
        }
    }
}