using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class SearchViewModel
    {
        public ICollection<Course> Courses { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        
    }
}
