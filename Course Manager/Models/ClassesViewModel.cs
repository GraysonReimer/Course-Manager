using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class ClassesViewModel
    {
        public List<Class> Classes { get; set; }
        public List<StaffMember> Staff { get; set; }
    }
}