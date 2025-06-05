using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BLL;
using DAL;

namespace BLL
{
    public class ActivityCategoryBLL
    {
        DB db = new DB();
        ActivityCategoryDAL dal = new ActivityCategoryDAL();

        public string Create(ActivityCategory a)
        {
            return dal.Create(a);
        }
        public DataTable ReadData()
        {
            return dal.ReadData();
        }
        public ActivityCategory ReadByid(int id)
        {
            return dal.ReadByid(id);
        }
        public ActivityCategory ReadCategoryList(string s)
        {
            return dal.ReadCategoryList(s);
        }
        public List<string> AcName ()
        {
            return dal.AcName();
        }
        public string Update(ActivityCategory a,int id)
        {
            return dal.Update(a,id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }

    }
}
