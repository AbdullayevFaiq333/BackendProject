using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string Username { get; set; }

        public string Role { get; set; }

        public List<string> Roles { get; set; }
    }
}
