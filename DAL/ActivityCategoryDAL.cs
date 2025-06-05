using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{  
    public class ActivityCategoryDAL
    {
        DB db = new DB();

        public string Create(ActivityCategory a)
        {
            try
            {
                db.ActivityCategories.Add(a);
                db.SaveChanges();
                return "ثبت دسته بندی فعالیت انجام شد";
            }
            catch (Exception e)
            {

                return"در ثبت دسته بندی فعالیت مشکلی به وجود آمد\n"+e.Message;
            }
        }
        public DataTable ReadData()
        {
            string cmd = "SELECT id AS [ردیف], CategoryName AS [نام دسته بندی]\r\nFROM dbo.ActivityCategories";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public ActivityCategory ReadByid(int id)
        {
            return db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
        }
        public ActivityCategory ReadCategoryList(string s)
        {
            return db.ActivityCategories.Where(i => i.CategoryName == s).FirstOrDefault();
        }
        public List<string> AcName () 
        { 
            return db.ActivityCategories.Select(i => i.CategoryName).ToList();
        }
        public string Update(ActivityCategory a,int id)
        {
            var q = db.ActivityCategories.Where(i => i.id==id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CategoryName = a.CategoryName;
                    db.SaveChanges();
                    return "ویرایش دسته بندی فعالیت با موفقیت انجام شد";
                }
                else
                {
                    return "دسته بندی مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "در ویرایش دسته بندی فعالیت مشکلی به وجود آمد\n"+e.Message;
            }
        }
        public string Delete(int id) 
        { 
            var q = db.ActivityCategories.Where(i => i.id ==id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    db.ActivityCategories.Remove(q);
                    db.SaveChanges();
                    return "حذف دسته بندی فعالیت انجام شد";
                }
                else
                {
                    return "دسته بندی مورد نظر یافت نشد";
                }

            }
            catch (Exception e)
            {

                return "در حذف دسته بندی مشکلی به وجود آمد"+e.Message;
            }
        }
    }
}
