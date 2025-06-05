using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class DashboardBLL
    {
        DashboardDAL dal = new DashboardDAL();
        public string UserRemindersCount(User u)
        {
            return dal.UserRemindersCount(u);
        }
        public string CustomerCount()
        {
            return dal.CustomerCount();
        }
        public string SellsCount()
        {
            return dal.SellsCount();
        }
        public List<Reminder> GetReminders(User u)
        {
            return dal.GetReminders(u);
        }
        public string ReminderCount()
        {
            return dal.ReminderCount();
        }
        public string ActivityConut()
        {
            return dal.ActivityCount();
        }
        public string InvoiceCount()
        {
            return dal.InvoiceConut();
        }
        public string ProductCount()
        {
            return dal.ProductConut();
        }
    }
}
