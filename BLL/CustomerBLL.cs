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
    public class CustomerBLL
    {
        CustomerDAL dal = new CustomerDAL();
        public string Create(Customer c)
        {
            if (dal.Read(c))
            {
                return dal.Create(c);
            }else
            {
                return "کاربری با همین شماره تماس در سیستم ثبت شده است";
            }
        }
        public DataTable ReadData()
        {
            return dal.ReadData();
        }
        public List<Customer> ReadDataCustomr()
        {
            return dal.ReadDataCustomer();
        }
        public DataTable Search(string s,int index)
        {
            return dal.Search(s,index);          
        }
        public Customer ReadC(string s)
        {
            return dal.ReadC(s);
        }
        public List<string> ReadCustomerPhone()
        {
            return dal.ReadCustomerPhone();    
        }
        public Customer ReadbyPhone(string phone)
        {
            return dal.ReadbyPhone(phone);
        }
        public Customer ReadByid(int id)
        {
            return dal.ReadByid(id);
        }
        public string Update(int id, Customer c)
        {
           return dal.Update(id,c);         
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }

       
    }
}
