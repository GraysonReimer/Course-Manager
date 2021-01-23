using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class StaffDetailsViewModel
    {
        public StaffMember staffMember { get; set; }
        public List<Class> classes { get; set; }
    }
}