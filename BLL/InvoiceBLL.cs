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
    public class InvoiceBLL
    {
        DB db = new DB();
        InvoiceDAL dal = new InvoiceDAL();
        public string Create(invoice i,Customer c,User u,List<Product> p)
        {
            return dal.Create(i,c,u,p);
        }
       public DataTable ReadInvoice()
        {
            return dal.ReadInvoice(); 
        }
        public string ReadInvoiceNum()
        {
            return dal.ReadInvoiceNum();
        }
        public invoice ReadByid(int id) 
        {
            return dal.ReadByid(id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
    }
}
