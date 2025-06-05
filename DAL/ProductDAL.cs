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
    public class ProductDAL
    {
        DB db = new DB();

        public string Create(Product p)
        {
            try
            {
                db.products.Add(p);
                db.SaveChanges();
                return "ثبت محصول با موفقیت انجام شد";
            }
            catch (Exception e)
            {

                return " در ثبت محصول مشکلی به وجود آمد :\n" + e.Message;
            }
        }
        public bool Read(Product p)
        {
            var q = db.products.Where(i => i.Name == p.Name);
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       //public List<Product> ReadData(Product p)
       // {
       //     return db.products.ToList();
       // }
        public DataTable ReadData()
        {
            string cmd = "SELECT id AS [آیدی محصول], name AS [نام کالا], Price AS [قیمت محصول], Stock AS [موجودیت]\r\nFROM dbo.Products";
            SqlConnection con = new SqlConnection("data source=.;initial catalog=CRM-db; integrated security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchProduct");
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
        public Product ReadByNames(string s) 
        { 
            return db.products.Where(i => i.Name == s).FirstOrDefault();
        }
        public List<string> ReadByProduct()
        {
            return db.products.Select(i => i.Name).ToList();
        }
        public Product ReadByid(int id)
        {
            return db.products.Where(i => i.id == id).FirstOrDefault();
        }
        public string Upate(int id,Product p)
        {
            var q = db.products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    
                    q.Name = p.Name;
                    q.Price = p.Price;
                    q.Stock = p.Stock;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "محصول مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "  در ویرایش اطلاعات مشکلی به وجود آمد:\n "+e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    db.products.Remove(q);
                    db.SaveChanges();
                    return "حذف محصول با موفقیت انجام شد";
                }
                else
                {
                    return "محصول مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return " در حذف اطلاعات مشکلی به وجود آمد \n " + e.Message;
            }
           

        }
    }
}
