using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class StaffMember
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }
        public string FullName { 
            
            
            get
            {
                return FirstName + " " + LastName;
            }
        
        
        }
    }
}