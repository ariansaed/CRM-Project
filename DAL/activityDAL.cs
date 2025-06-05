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
    public class activityDAL
    {
        
        DB db = new DB();
        public string Create(Activity a,User u,Customer c,ActivityCategory Ac)
        {
            try
            {
                a.user = db.Users.Find(u.id);
                a.customer = db.Customers.Find(c.id);
                a.Category = db.ActivityCategories.Find(Ac.id);
               db.activities.Add(a);
                db.SaveChanges();
                return "ثبت فعالیت با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ثبت فعالیت مشکلی به وجود آمد\n" + e.Message;
            }

        }
        //خواندن اطلاعات از دیتابیس 
       
        public DataTable ReadDataActivity()
        {
            string cmd = "SELECT dbo.Activities.id AS [ردیف], dbo.Activities.Title AS [موضوع فعالیت]," +
                " dbo.ActivityCategories.CategoryName AS [دسته بندی], " +
                "dbo.Customers.name AS [نام مشتری], dbo.Users.name AS [نام کاربر]," +
                " dbo.Activities.RegDate AS [تاریخ ثبت]\r\nFROM     dbo.Activities INNER JOIN\r\n" +
                " dbo.Customers ON dbo.Activities.customer_id = dbo.Customers.id INNER JOIN\r\n" +
                " dbo.Users ON dbo.Activities.user_id = dbo.Users.id INNER JOIN\r\n " +
                " dbo.ActivityCategories ON dbo.Activities.Category_id = dbo.ActivityCategories.id";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public Activity Read(int id)
        {
            return db.activities.Find(id);
        }
        public string ReadByinfo(int id)
        {
            return db.activities.FirstOrDefault(i => i.id == id).Info;
        }
        public string Delete(int id)
        {
           var q = db.activities.Where(i => i.id == id).FirstOrDefault();
            try
            {
                db.activities.Remove(q);
                db.SaveChanges();
                return "حذف فعالیت با موفقیت انجام شد";
            }
            catch (Exception e)
            {

                return "در حذف فعالیت مشکلی به وجود آمد لطفا بررسی کنید" + e.Message;
            }
        }
    }
}
