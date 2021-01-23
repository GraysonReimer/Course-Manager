using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class CoursesViewModel
    {
        public List<Course> Courses { get; set; }
        public List<Class> Classes { get; set; }
    }
}