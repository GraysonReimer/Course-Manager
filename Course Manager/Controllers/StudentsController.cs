using AutoMapper;
using Course_Manager.Controllers.DTOs;
using Course_Manager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace Course_Manager.Controllers
{
    [Authorize(Roles = "CanManageWebsite")]
    public class StudentsController : Controller
    {
        // GET: Students
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;
        System.Random random;
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _context.Dispose();
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public StudentsController()
        {
            _context = new ApplicationDbContext();
            random = new System.Random();
        }

        public int GetNewId()
        {
            int ID = 0;
            List<int> IDs = _context.Students.Select(s => s.StudentNumber).ToList();
            ID = random.Next(0, 9999999);
            while (IDs.Contains(ID))
                ID = random.Next(0, 9999999);

            return ID;
        }
        [HttpPost]
        public async Task<ActionResult> Save(Student student)
        {
            Student studentInDb = _context.Students.SingleOrDefault(s => s.Id == student.Id);
            if (!ModelState.IsValid)
            {
                return View("StudentForm", student);
            }

            if (student.Id == 0)
            {
                student.StudentNumber = GetNewId();
                _context.Students.Add(student);
                var StudentAccount = new ApplicationUser { UserName = $"{student.FirstName} {student.LastName}" };
                var result = await UserManager.CreateAsync(StudentAccount, Convert.ToString(student.StudentNumber));
                student.UserId = StudentAccount.Id;
            }
            else
            {
                var user = _context.Users.SingleOrDefault(u => u.Id == studentInDb.UserId);
                user.UserName = student.FirstName + " " + student.LastName;
                _context.SaveChanges();
                foreach (PropertyInfo property in typeof(Student
                    ).GetProperties())
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(studentInDb, property.GetValue(student, null), null);
                    }
                }
            }



            
            _context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

        public ActionResult Details(int id)
        {
            List<Course> courses = _context.Courses.Include("Category").ToList();
            List<StaffMember> staff = _context.Staff.ToList();
            Student student = _context.Students.Include("CourseRequests").Include("Classes").FirstOrDefault(s => s.StudentNumber == id);
            if (student == null)
                return new HttpNotFoundResult();

            return View(student);
        }

        public ActionResult New(Student student = null)
        {
            Student newStudent = new Student { FirstName = "", LastName = "", Id = 0, Grade = 8 };

            if (student == null)
                return View("StudentForm", newStudent);
            else
                return View("StudentForm", student);
            


        }
        public ActionResult Index()
        {
            
            return View(_context.Students.ToList());
        }
    }
}