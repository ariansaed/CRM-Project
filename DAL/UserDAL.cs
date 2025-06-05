using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UserDAL
    {
        DB db = new DB();
        public string Create(User u,UserGroup ug)
        {
            try
            {
                if (Read(u))
                {
                    u.UserGroup = db.userGroups.Find(ug.id);
                    u.UserGroup = db.userGroups.Where(i => i.Title == ug.Title).SingleOrDefault();
                    db.Users.Add(u);
                    db.SaveChanges();
                    return "ثبت نام کاربر با موفقیت انجام شد";
                }
                else
                {
                    return "نام کاربری وارد شده تکراری می باشد";
                }

            }
            catch (Exception e)
            {

                return " ثبت اطلاعات با مشکلی مواجه شد! \n" + e.Message;
            }

        }
        public bool IsRegistered()
        {
            try
            {
                return db.Users.Count() > 0;
            }
            catch (Exception)
            {

                return false;
            }
            
        }
        public bool Read(User u)
        {
            var q = db.Users.Where(i => i.UserName == u.UserName);
            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable ReadDataUser()
        {
            string cmd = "SELECT id AS [ردیف], name AS [نام و نام خانوادگی], UserName AS [نام کاربری], RegDate AS [تاریخ ثبت]\r\nFROM     dbo.Users";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public User Read(int id)
        {
            return db.Users.Where(i => i.id == id).FirstOrDefault();
        }
        public User ReadU(string s)
        { 
            return db.Users.Where(i => i.UserName == s).FirstOrDefault();
        }
        public UserGroup Readtitle(string n)
        {
            return db.userGroups.Where(i => i.Title == n).FirstOrDefault();
        }
        public List<string> ReadUserName()
        {
            return db.Users.Select(i => i.UserName).ToList();
        }
        public string Update(User u,int id)
        {
            try
            {
                var q = db.Users.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    User user = Read(id);
                    q.name = u.name;
                    q.UserName = u.UserName;
                    q.Password = u.Password;
                    q.Pic = u.Pic;
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

                return "در ویرایش اطلاعات مشکلی به وجود آمد!\n"+e.Message;
            }
        }
        public User Login(string User,string Password)
        {
            return db.Users.Include("userGroup").Where(i => i.UserName == User && i.Password == Password).SingleOrDefault();
        }
        public bool Access(User u,string s,int a)
        {
            try
            {
                UserGroup ug = db.userGroups.Include("UserAccessRoles").Where(i => i.id == u.UserGroup.id).FirstOrDefault();
                UserAccessRole uar = ug.UserAccessRoles.Where(z => z.Section == s).FirstOrDefault();
                if (a == 1 && true)
                {
                    return uar.CanEnter;
                }
                else if (a == 2 && true)
                {   
                    return uar.CanCreate;
                }
                else if (a == 3 && true)
                {
                    return uar.CanUpdate;
                }
                else
                {             
                    return uar.CanDelete;
                }                     
            }
            catch (Exception)
            {
                return true;
            }           
        }

        public List<User> ReadInvoices()
        {
            return db.Users.Include("Invoices").Where(i => i.Status == false).ToList();
        }
    }
}
