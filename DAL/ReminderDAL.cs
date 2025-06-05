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
    public class ReminderDAL
    {
        DB db = new DB();

        public string Create(Reminder r,User u)
        {
            try
            {
                r.Users = db.Users.Find(u.id);
                db.Reminders.Add(r);
                db.SaveChanges();
                return "ثبت یادآور با موفقیت انجام شد";
            }
            catch (Exception e)
            {

                return "در ثبت یادآور مشکلی به وجود آمد\n" + e.Message;
            }

        }
        public DataTable ReadData()
        {
            string cmd = "SELECT dbo.Reminders.id AS[آیدی یادآور], dbo.Reminders.Title AS [موضوع یادآور], dbo.Reminders.ReminderInfo AS [جزئیات یادآور], " +
                "dbo.Reminders.RemindDate AS [تاریخ یادآور]," +
                " dbo.Reminders.Isdone AS [انجام شده], dbo.Users.name AS [کاربر مربوط]\r\nFROM " +
                "dbo.Reminders INNER JOIN\r\n dbo.Users ON dbo.Reminders.users_id = dbo.Users.id";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
       
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchReminder");
            SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
            cmd.Parameters.AddWithValue("@search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

        public Reminder ReadByid(int id)
        {
            return db.Reminders.Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(Reminder r, int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Title = r.Title;
                    q.RemindDate = r.RemindDate;
                    q.ReminderInfo = r.ReminderInfo;
                    db.SaveChanges();
                    return "ویرایش یادآور با موفقیت انجام شد";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "در ویرایش یادآور مشکلی به وجود آمد!\n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {                   
                    db.Reminders.Remove(q);
                    db.SaveChanges();
                    return "حذف یادآور با موفقیت انجام شد";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "در حذف یادآور مشکلی به وجود آمد" + e.Message;
            }
        }
        public string Done(bool id)
        {
            var q = db.Reminders.Where(i => i.Isdone == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Isdone = true;
                    db.SaveChanges();
                    return "یادآور با موفقیت انجام شد";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "در تغییر یادآور مشکلی به وجود آمد!" + e.Message;
            }
        }
    }
}

