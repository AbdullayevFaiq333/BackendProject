using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public BlogDetail BlogDetail { get; set; }
    }
}
