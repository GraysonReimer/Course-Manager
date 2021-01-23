using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Course_Manager.Models
{
    public class CourseRequest
    {
        [Required]
        public int Id { get; set; }
        public Course FirstChoice { get; set; }
        public Course SecondChoice { get; set; }
        public Class GetClass(List<Class> classes)
        {
            classes = classes.Where(c => c.StudentsEnrolled < c.OccupancyLimit)
                .OrderBy(c => c.StudentsEnrolled).ToList();

            Class FirstClass = GetBestOption(classes, FirstChoice);
            Class SecondClass = GetBestOption(classes, SecondChoice);
            if (FirstClass.course == FirstChoice)
                return FirstClass;
            if (SecondClass.course == SecondChoice)
                return SecondClass;
            if (FirstClass.course.Category == FirstChoice.Category)
                return FirstClass;
            if (SecondClass.course.Category == SecondChoice.Category)
                return FirstClass;

            return FirstClass;
        }

        private Class GetBestOption(List<Class> classes, Course Choice)
        {
            Class Class = classes.FirstOrDefault(c => c.course == Choice);
            if (Class != null)
                return Class;

            Class = classes.FirstOrDefault(c => c.course.Category == Choice.Category);
            if (Class != null)
                return Class;

            return classes[0];
        }
    }
}