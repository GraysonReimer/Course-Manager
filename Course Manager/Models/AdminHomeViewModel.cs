using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class AdminHomeViewModel
    {
        public List<Student> Students { get; set; }
        public List<Outline> Outlines { get; set; }
        public List<Class> Classes { get; set; }
        public bool StudentsCanPickCourses { get; set; }
    }
}