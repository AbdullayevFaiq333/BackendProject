using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class User:IdentityUser
    {
        [Required]
        public string Fullname { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Course> Courses { get; set; }

    }
}
