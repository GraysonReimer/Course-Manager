using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }
        [Required]
        [Range(8, 12)]
        public int Grade { get; set; }
        [Required]
        public int StudentNumber { get; set; }
        public List<CourseRequest> CourseRequests { get; set; }
        public List<Class> Classes { get; set; }
        public bool DonePickingClasses { get; set; }
        public string UserId { get; set; }
        public Student()
        {
            Classes = new List<Class>();
            CourseRequests = new List<CourseRequest>();
            DonePickingClasses = false;
        }
    }
}