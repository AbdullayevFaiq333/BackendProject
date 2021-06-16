using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class CourseViewModel
    {
        public List<Category> Categories { get; set; }
        public Course Course { get; set; }
    }
}
