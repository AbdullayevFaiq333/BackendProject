using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public TeacherDetail TeacherDetail { get; set; }

        public ICollection<SocialMedia> SocialMedias { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
