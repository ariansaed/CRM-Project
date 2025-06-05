using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class invoice
    {
        public int id { get; set; }
        public string invoiceNumber { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsCheckout { get; set; }
        public DateTime CheckoutDate { get; set; }
        public Customer customer { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public User user { get; set; }
       
    }
}
