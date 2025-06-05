using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class ProductBLL
    {
        ProductDAL dal = new ProductDAL();

        public string Create(Product p)
        {
            return dal.Create(p);
        }      
        public DataTable ReadData()
        {
            return dal.ReadData();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public Product ReadByNames(string s)
        {
            return dal.ReadByNames(s);
        }
        public List<string> ReadByProduct() 
        {
            return dal.ReadByProduct();
        }
        public Product ReadByid(int id)
        {
            return dal.ReadByid(id);
        }
        //public List<Product> ReadData(Product p)
        //{
        //    return dal.ReadData(p);
        //}
        public string Update(int id,Product p)
        {
           return dal.Upate(id,p);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);  
        }
    }
}
