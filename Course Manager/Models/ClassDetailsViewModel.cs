using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class ClassDetailsViewModel
    {
        public int Semester { get; set; }
        public int Block { get; set; }
        public string CourseName { get; set; }
        public string InstructorName { get; set; }
        public List<Student> StudentsInClass { get; set; }
    }
}