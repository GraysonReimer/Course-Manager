using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Course_Manager.Controllers
{
    public class OutlinesController : Controller
    {
        private ApplicationDbContext _context;
        // GET: Outlines
        public ActionResult Index()
        {
            return View("Index", new OutlinesViewModel() { outline = new Outline(0) });
        }
        public ActionResult View(Outline outline)
        {
            outline = _context.Outlines.Include("Courses").SingleOrDefault(o => o.Grade == outline.Grade);


            if (outline == null)
                return HttpNotFound();

            OutlinesViewModel model = new OutlinesViewModel()
            {
                outline = outline,
                courses = _context.Courses.Where(c => c.GradeLevel == outline.Grade).ToList()
            };

            return View("Index", model);
        }
        [HttpPost]
        public ActionResult AddCourseToOutline(int id, int grade)
        {
            Course course = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (course == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            Outline outline = _context.Outlines.SingleOrDefault(o => o.Grade == grade);
                
            outline.Courses.Add(course);
            _context.Entry(outline).State = System.Data.Entity.EntityState.Modified;
            try
            {
                _context.SaveChanges();

            } catch(Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RemoveCourseFromOutline(int id, int grade)
        {
            Course course = _context.Courses.SingleOrDefault(c => c.Id == id);

            if (course == null)
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            Outline outline = _context.Outlines.SingleOrDefault(o => o.Grade == grade);

            outline.Courses.Remove(course);
            _context.Entry(outline).State = System.Data.Entity.EntityState.Modified;
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
        public OutlinesController()
        {
            _context = new ApplicationDbContext();
        }
    }
}