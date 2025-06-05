using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL; 

namespace BLL
{
    public class ReminderBLL
    {
        DB db = new DB();
        ReminderDAL dal = new ReminderDAL();

        public string Create (Reminder r,User u)
        {
            return dal.Create(r,u);
        }
        public DataTable ReadData()
        {
        return dal.ReadData(); 
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public Reminder ReadByid(int id)
        {
            return dal.ReadByid(id);
        }
       
        public string Update(Reminder r,int id)
        {
            return dal.Update(r,id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
