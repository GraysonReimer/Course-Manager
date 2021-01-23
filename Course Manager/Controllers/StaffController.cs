using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Course_Manager.Controllers
{
    public class StaffController : Controller
    {
        private ApplicationDbContext _context;

        // GET: Staff
        public ActionResult Index()
        {
            return View(_context.Staff.ToList());
        }
        public ActionResult New(StaffMember staffMember = null)
        {
            if (staffMember == null)
                staffMember = new StaffMember { FirstName = "", LastName = "" };

            return View("StaffForm", staffMember);
        }
        public ActionResult Details(int id)
        {
            StaffMember staffMember = _context.Staff.SingleOrDefault(s => s.Id == id);
            List<Class> classes = _context.Classes.Include("course").Where(c => c.Instructor.Id == staffMember.Id).ToList();
            StaffDetailsViewModel model = new StaffDetailsViewModel()
            { 
                staffMember = staffMember,
                classes = classes
            };


            return View(model);
        }
        public ActionResult Save(StaffMember staffMember)
        {
            StaffMember staffMemberInDb = _context.Staff.SingleOrDefault(s => s.Id == staffMember.Id);
            if (!ModelState.IsValid)
            {
                return View("StaffForm", staffMember);
            }
            if (staffMemberInDb == null)
            {
                _context.Staff.Add(staffMember);
            }
            else
            {
                foreach (PropertyInfo property in typeof(StaffMember).GetProperties())
                {
                    if (property.CanWrite)
                    {
                        property.SetValue(staffMemberInDb, property.GetValue(staffMember, null), null);
                    }
                }
            }

            _context.SaveChanges();

            return View("Index", _context.Staff.ToList());
        }
        public StaffController()
        {
            _context = new ApplicationDbContext();
        }
    }
}