using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class Course
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int GradeLevel { get; set; }
        [Display(Name = "Can Be An Elective")]
        public bool CanBeAnElective { get; set; }
    }
}