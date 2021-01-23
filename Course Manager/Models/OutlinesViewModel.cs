using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class OutlinesViewModel
    {
        public Outline outline { get; set; }
        public List<Course> courses { get; set; }
    }
}