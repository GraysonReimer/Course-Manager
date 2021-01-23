using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class SelectCoursesViewModel
    {
        public List<Course> AvailableCourses { get; set; }
        public List<int> FirstChoices { get; set; }
        public List<int> SecondChoices { get; set; }
        public int ElectiveCount { get; set; }
        public SelectCoursesViewModel()
        {
            FirstChoices = new List<int>();
            SecondChoices = new List<int>();
        }
    }
}