using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class Class
    {
        [Required]
        public int Id { get; set; }
        public Course course { get; set; }
        [Required]
        public int CourseId { get; set; }
        public StaffMember Instructor { get; set; }
        [Required]
        public int InstructorId { get; set; }
        [Required]
        [Display(Name = "Room Number")]
        public int RoomNumber { get; set; }
        [Required]
        [Range(1, 4)]
        public int Block { get; set; }
        [Required]
        [Range(1, 2)]
        public byte Semester { get; set; }
        [Display(Name = "Occupancy Limit")]
        public int OccupancyLimit { get; set; }
        public int StudentsEnrolled { get; set; }

        public Class()
        {
            StudentsEnrolled = 0;
        }
    }
}