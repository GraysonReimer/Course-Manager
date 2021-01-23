using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Manager.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext _context;


        [Authorize(Roles = "CanManageWebsite")]
        public ActionResult BeginCourseSelection()
        {
            _context.StudentsCanPickCourses = true;
            foreach (Student student in _context.Students)
                student.DonePickingClasses = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "CanManageWebsite")]
        public ActionResult StopCourseSelection()
        {
            _context.StudentsCanPickCourses = false;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            if (User.IsInRole("CanManageWebsite"))
            {
                AdminHomeViewModel model = new AdminHomeViewModel()
                {
                    Students = _context.Students.ToList(),
                    StudentsCanPickCourses = _context.StudentsCanPickCourses,
                    Outlines = _context.Outlines.Include("Courses").ToList(),
                    Classes = _context.Classes.ToList()
                };
                return View("AdminHome", model);
            }

            return View("StudentHome");
        }
        public ActionResult ClearCourses()
        {
            List<Student> students = _context.Students.ToList();
            foreach (Student student in students)
            {
                student.Classes = new List<Class>();
                student.CourseRequests = new List<CourseRequest>();
            }

            try
            {
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult IncrementGrades()
        {
            List<Student> students = _context.Students.ToList();
            foreach (Student student in students)
            {
                if (student.Grade != 12)
                    student.Grade++;
                else
                {
                    var user = _context.Users.SingleOrDefault(u => u.Id == student.UserId);

                    if (user != null)
                        _context.Users.Remove(user);

                    _context.Students.Remove(student);

                    _context.SaveChanges();
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AssignClasses()
        {
            List<Course> availableCourses = _context.Courses.ToList();
            List<Student> students = _context.Students.Include("CourseRequests").ToList();
            List<Class> classes = new List<Class>();
            foreach (Class Class in _context.Classes.ToList())
            {
                Class.StudentsEnrolled = 0;
            }

            foreach (Student student in students)
            {
                List<Course> mandatoryClasses = _context.Outlines.Include("Courses").FirstOrDefault(o => o.Grade == student.Grade).Courses.ToList();
                foreach (Course course in mandatoryClasses)
                {
                    List<Class> bestClasses = _context.Classes.ToList().Where(c => c.course == course && c.StudentsEnrolled < c.OccupancyLimit).OrderBy(c => c.StudentsEnrolled).ToList();
                    if (bestClasses.Count == 0)
                        return Json(new { success = false, message = $"Error: The mandatory course, {course.Name}, is not available for the student {student.FirstName} {student.LastName}." }, JsonRequestBehavior.AllowGet);

                    classes.AddRange(bestClasses);
                }
                foreach (CourseRequest request in student.CourseRequests)
                {
                    Class bestClass = request.GetClass(_context.Classes.ToList());

                    classes.Add(bestClass);
                }

                for (int i = 1; i < 9; i++)
                {
                    int block;
                    int semester;
                    if (i > 4)
                    {
                        block = i - 4;
                        semester = 2;
                    }
                    else
                    {
                        block = i;
                        semester = 1;
                    }
                    Class classForBlock = classes.FirstOrDefault(c => c.Block == block && c.Semester == semester);
                    if (classForBlock == null)
                        return Json(new { success = false, message = $"Error: No grade {student.Grade} class for {student.FirstName} {student.LastName} is available in block {block} semester {semester}." }, JsonRequestBehavior.AllowGet);

                    student.Classes.Add(classForBlock);
                    classForBlock.StudentsEnrolled++;
                }
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Data }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DecrementGrades()
        {
            List<Student> students = _context.Students.ToList();
            foreach (Student student in students)
            {
                student.Grade++;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
    }
}