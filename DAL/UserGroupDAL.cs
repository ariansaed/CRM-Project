using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UserGroupDAL
    {
        DB db = new DB();

        public string Create(UserGroup ug)
        {
            try
            {
                db.userGroups.Add(ug);
                db.SaveChanges();
                return "ثبت گروه کاربری با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "در ثبت گروه های کاربری مشکلی به موجود آمد\n" + e.Message;
            }           
        }
        public UserGroup ReadName(string s) 
        {
            return db.userGroups.Where(i => i.Title == s).FirstOrDefault();
        }
        public List<string> ReadUserGroup()
        {
            return db.userGroups.Select(i => i.Title).ToList();

        }
    }
}
