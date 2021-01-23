using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class NewCourseViewModel
    {
        public List<Category> Catergories { get; set; }
        public Course Course { get; set; }
    }
}