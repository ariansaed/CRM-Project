using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Reminder
    {
        public Reminder()
        { 
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Title { get; set; }
        public string ReminderInfo { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime RemindDate { get; set; }
        public bool DeleteStatus { get; set; }
        public Nullable<bool> Isdone { get; set; }
        public User Users { get; set; } 

    }
}
