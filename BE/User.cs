using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class User
    {
        public User()
        {
            Status = false;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Pic { get; set; }
        public bool Status { get; set; }
        public DateTime RegDate { get; set; }
        public List<Activity> activities { get; set; } = new List<Activity>();
        public List<invoice> Invoices { get; set; } = new List<invoice>();
        public List<Reminder> Reminders { get; set; } = new List<Reminder>();
        public UserGroup UserGroup { get; set; }
    }
}
