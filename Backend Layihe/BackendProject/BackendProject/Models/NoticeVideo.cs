using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class NoticeVideo
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Required]
        public string Image { get; set; }
    }
}
