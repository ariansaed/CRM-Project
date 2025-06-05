using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class CustomerDAL
    {
        DB db = new DB();
        // متد ثبت کردن مشتری در سرور 
        // Create Method 
        public string Create(Customer c)
        {
            try
            {
                db.Customers.Add(c);
                db.SaveChanges();
                return "ثبت کاربر با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت کاربر با مشکلی مواجه شد :\n" + e.Message;
            }
        }
        // متد تکراری بودن شماره تماس مشتری 
        public bool Read(Customer c)
        {
            var q = db.Customers.Where(i => i.Phone == c.Phone);
            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //خواندن اطلاعات و دریافت از دیتابیس     
        public List<Customer> ReadDataCustomer()
        {
            return db.Customers.ToList();
        }
        public DataTable ReadData()
        {
            try
            {
                string cmd = "SELECT id AS [ردیف], Name AS [نام مشتری], Phone AS [شماره تماس], RegDate AS [تاریخ ثبت]\r\nFROM dbo.Customers";
                SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
                var sqladapter = new SqlDataAdapter(cmd, con);
                var commandbuilder = new SqlCommandBuilder(sqladapter);
                var ds = new DataSet();
                sqladapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
           
        }
        // متد سرچ و نمایش اطلاعات در دیتابیس
        public DataTable Search(string s, int index)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (index == 0)
                {
                    cmd.CommandText = "dbo.SearchCustomerAll";
                }
                else if (index == 1)
                {
                    cmd.CommandText = "dbo.SearchCustomerName";
                }
                else if (index == 2)
                {
                    cmd.CommandText = "dbo.SearchCustomerPhone";
                }
                SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
                cmd.Parameters.AddWithValue("@Search", s);
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                var sqladapter = new SqlDataAdapter();
                sqladapter.SelectCommand = cmd;
                var commandbuilder = new SqlCommandBuilder(sqladapter);
                var ds = new DataSet();
                sqladapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
                
        }
        // گرفتن دیتا از دیتابیس با آیدی برای ویرایش اطلاعات  
        public Customer ReadByid(int id)
        {
            return db.Customers.Where(i => i.id == id).FirstOrDefault();
        }

        // متد دریافت نام مشتری 
        public Customer ReadC(string s)
        {
            return db.Customers.Where(i => i.Phone == s).FirstOrDefault();
        }
        // متد دریافت شماره تماس مشتری به صورت لیست
        // Collections//
        public List<string> ReadCustomerPhone()
        {
            return db.Customers.Select(i => i.Phone).ToList();
        }
        public Customer ReadbyPhone(string phone)
        {
            return db.Customers.Where(i => i.Phone == phone).FirstOrDefault();
        }
        // متد آپدیت و ویرایش اطلاعات
        public string Update(int id, Customer c)
        {
            try
            {
                var q = db.Customers.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.Name = c.Name;
                    q.Phone = c.Phone;
                    db.SaveChanges();
                    return "ویرایش کاربر با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {
                return " ویرایش کاربر با مشکلی مواجه شد!:\n" + e.Message;
            }
        }
        // متد پاک کردن اطلاعات دیتابیس
        //  ( بازگردانی اطلاعات امکان پذیر است) 
        public string Delete(int id)
        {
            var q = db.Customers.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    //q.DeleteStatus = true;
                    db.Customers.Remove(q);
                    db.SaveChanges();
                    return "حذف کاربر با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {
                return "حذف کاربر با مشکلی مواجه شد :\n" + e.Message;
            }
        }
    }
}

