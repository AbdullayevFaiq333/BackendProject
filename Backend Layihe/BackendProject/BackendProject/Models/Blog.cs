using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime Time { get; set; }
        public int MessageCount { get; set; }
        public string MainDescription { get; set; }
        public bool IsDeleted { get; set; }

        public BlogDetail BlogDetail { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
