using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class Outline
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int Grade { get; set; }
        public List<Course> Courses { get; set; }

        public Outline(int Grade)
        {
            Courses = new List<Course>();
            this.Grade = Grade;
        }
        public Outline()
        {
            Courses = new List<Course>();
        }
    }
}