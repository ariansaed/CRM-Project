using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class DashboardDAL
    {
        DB db = new DB();

        public string UserRemindersCount(User u)
        {
            User q = db.Users.Include("Reminders").Where(i => i.id == u.id).FirstOrDefault();
            return q.Reminders.Count().ToString();
        }
        public string CustomerCount()
        {
            return db.Customers.Count().ToString();
        }
        public string SellsCount()
        {
            int sum = 0;
            foreach (var item in db.Invoices)
            {
                if (item.RegDate.Date == DateTime.Today)
                {
                    sum = sum + 1;
                }
            }
            return sum.ToString();
        }
        public List<Reminder> GetReminders(User u) 
        {
            return db.Reminders.Include("Users").Where(i => i.Users.id == u.id).ToList();
        }
        public string ReminderCount()
        {
            return db.Reminders.Count().ToString();
        }
        public string ActivityCount()
        {
            return db.activities.Count().ToString();
        }
        public string InvoiceConut()
        {
            return db.Invoices.ToString();
        }
        public string ProductConut() 
        { 
            return db.products.ToString();
        }

    }
}
