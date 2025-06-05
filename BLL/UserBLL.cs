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
    public class UserBLL
    {
        /// Encoded password 
        private string Encode(string Pass)
        {
            Byte[] encData_byte = new byte[Pass.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(Pass);
            string encodeData = Convert.ToBase64String(encData_byte);
            return encodeData;
        }
        /// Decoded password 
        private string Decode(string EncodedPass)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8decode = encoder.GetDecoder();
            Byte[] todecode_byte = Convert.FromBase64String(EncodedPass);
            int charCount = utf8decode.GetCharCount(todecode_byte, 0 ,todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8decode.GetChars(todecode_byte,0,todecode_byte.Length,decoded_char,0);
            string result = new string(decoded_char);
            return result;

        }


        DB db = new DB();
        UserDAL dal = new UserDAL();
        public string Create(User u,UserGroup ug)
        {
            u.Password = Encode(u.Password);
            return dal.Create(u , ug);
        }
        public bool IsRegistered()
        {

            return dal.IsRegistered();
        }
        public DataTable ReadDataUser()
        {
            return dal.ReadDataUser();
        }
        public UserGroup Readtitle(string n)
        {
            return dal.Readtitle(n);
        }
        public User Read(int id)
        {
            return (User)dal.Read(id);
        }
        public User ReadU(string s)
        {
            return dal.ReadU(s);
        }
        public List<string> ReadUserName()
        {
            return dal.ReadUserName();
        }
        public string Update(User u,int id)
        {
            u.Password = Encode(u.Password);
            return dal.Update(u,id);
        }
        public User Login(string User,string Password)
        {
            return dal.Login(User,Encode(Password));
        }
        public bool Access(User u, string s, int a)
        {
            return dal.Access(u,s,a);
        }
        public List<User> ReadInvoices()
        {
            return dal.ReadInvoices();
        }
    }
}
