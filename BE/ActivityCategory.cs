﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ActivityCategory
    {
        public int id { get; set; }
        public string CategoryName { get; set; }
        public List<Activity> activities { get; set; } = new List<Activity>();
    }
}
