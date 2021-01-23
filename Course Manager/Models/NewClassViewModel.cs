using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class NewClassViewModel
    {
        public Class Class { get; set; }
        public List<Course> Courses { get; set; }
        public List<StaffMember> Staff { get; set; }
    }
}