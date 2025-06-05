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
    public class InvoiceDAL
    {
        DB db = new DB();

        public string Create(invoice i,Customer c,User u,List<Product> p)
        {
            try
            {
                i.customer = db.Customers.Find(c.id);
                foreach (var item in p)
                {
                    i.Products.Add(db.products.Find(item.id));                    
                }
                i.user = db.Users.Find(u.id);               
                Random rnd = new Random();
                string NumberRandom = rnd.Next(10000000).ToString();
                var q = db.Invoices.Where(z => z.invoiceNumber == NumberRandom);
                while (q.Count() > 0)
                {
                    NumberRandom = rnd.Next(10000000).ToString();
                }
                i.invoiceNumber = NumberRandom;
                db.Invoices.Add(i);
                db.SaveChanges();
                return "ثبت فاکتور با موفقیت انجام شد";
            }
            catch (Exception e)
            {

                return "ثبت فاکتور با مشکلی مواجه شد" + e.Message;
            }
        }
        public DataTable ReadInvoice()
        {
            try
            {
                string cmd = "SELECT \r\n    i.id AS [آیدی فاکتور],\r\n    i.invoiceNumber AS [شماره فاکتور],\r\n    c.Name AS [نام مشتری],\r\n    p.Name AS [نام محصول],\r\n    p.Price AS [قیمت محصول],\r\n    u.UserName AS [نام کاربر],\r\n    i.IsCheckout AS [پرداخت شده],\r\n    i.RegDate AS [تاریخ ثبت],\r\n    pi.invoice_id AS [آیدی فاکتور در محصول],\r\n    pi.Product_id AS [آیدی محصول]\r\nFROM \r\n    dbo.invoices AS i\r\nLEFT JOIN \r\n    dbo.Customers AS c ON i.customer_id = c.id\r\nLEFT JOIN \r\n    dbo.Productinvoices AS pi ON i.id = pi.invoice_id\r\nLEFT JOIN \r\n    dbo.Products AS p ON pi.Product_id = p.id\r\nLEFT JOIN \r\n    dbo.Users AS u ON i.user_id = u.id;\r\n";
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
        public string ReadInvoiceNum()
        {
            var q = db.Invoices.OrderByDescending(i => i.id).FirstOrDefault();
            return q.invoiceNumber;
        }
        public invoice ReadByid(int id)
        {
            return db.Invoices.Where(i => i.id == id).FirstOrDefault();
        }
        public string Delete(int id)
        {
            try
            {
                var q = db.Invoices.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    db.Invoices.Remove(q);
                    db.SaveChanges();
                    return "فاکتور مورد نظر با موفقیت حذف شد";
                }
                else
                {
                    return "فاکتور مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "در حذف فاکتور مشکلی به وجود آمد\n"+ e.Message;
            }
        }
    }
}
