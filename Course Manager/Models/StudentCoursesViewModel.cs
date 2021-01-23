using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class StudentCoursesViewModel
    {
        public Student student { get; set; }
        public List<CourseRequest> Requests { get; set; }
        public bool CanSelectCourses { get; set; }
    }
}