using Course_Manager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Controllers.DTOs
{
    public class CourseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int GradeLevel { get; set; }
        public bool FirstSemester { get; set; }
        public bool SecondSemester { get; set; }
    }
}