using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace BLL
{
    
    public class UserGroupBLL
    {
        UserGroupDAL dal = new UserGroupDAL();
        public string Create(UserGroup ug)
        {
            return dal.Create(ug);
        }
        public UserGroup ReadName(string s)
        { 
            return dal.ReadName(s);
        }
        public List<string> ReadUserGroup()
        {
            return dal.ReadUserGroup();       
        }   
    }
}
