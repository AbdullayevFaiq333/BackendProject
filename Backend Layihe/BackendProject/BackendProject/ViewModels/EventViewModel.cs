﻿using BackendProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendProject.ViewModels
{
    public class EventViewModel
    {
        public List<Category> Categories { get; set; }
        public Event Event { get; set; }
    }
}
