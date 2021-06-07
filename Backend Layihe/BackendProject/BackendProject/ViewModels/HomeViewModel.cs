using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<BackgroundArea> BackgroundAreas { get; set; }
        public ICollection<Service> Services { get; set; }
        public CoursesAreaBackground CoursesAreaBackground { get; set; }
    }
}
