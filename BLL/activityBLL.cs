using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class activityBLL
    {
        DB db = new DB();
        activityDAL dal = new activityDAL();
        CustomerDAL Cdal = new CustomerDAL();
        ActivityCategoryDAL ACdal = new ActivityCategoryDAL();

        public string Create (Activity a,User u,Customer c,ActivityCategory Ac)
        {
            return dal.Create(a,u,c,Ac);
        }
        public DataTable ReadDataActivity()
        {
            return dal.ReadDataActivity();
        }
        public Activity Read(int id)
        {
            return dal.Read(id);
        }
        public string ReadByinfo(int id)
        {
            return dal.ReadByinfo(id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id); 
        }


    }
}
