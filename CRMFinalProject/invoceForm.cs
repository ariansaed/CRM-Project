using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE;
using BLL;
using DevComponents.DotNetBar.Controls;
using Stimulsoft.Report;
using static HandyControl.Tools.Interop.InteropValues;

namespace CRMFinalProject
{
    public partial class invoceForm : Form
    {
        public invoceForm()
        {
            InitializeComponent();
        }
        ////////////////////////////
        Customer c = new Customer();
        CustomerBLL Cbll = new CustomerBLL();     
        ////////////////////////////
        ProductBLL Pbll = new ProductBLL();
        Product p = new Product();
        List<Product> products = new List<Product>();       
        ///////////////////////////
        invoice i = new invoice();
        InvoiceBLL Ibll = new InvoiceBLL();        
        //////////////////////////      
        User u = new User();
        UserBLL Ubll = new UserBLL();
        MsgBox MsgBox = new MsgBox();
        DashboardBLL Dbll = new DashboardBLL();
        void FillDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products;
            dataGridView1.Columns["id"].Visible = false;
            dataGridView1.Columns["Stock"].Visible = false;
            dataGridView1.Columns["Name"].HeaderText = "نام محصول";
            dataGridView1.Columns["Price"].HeaderText = "قیمت محصول";
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void invoceForm_Load(object sender, EventArgs e)
        {
            ShowDate.Text = DateTime.Now.ToString("yyyy,MM,dd");
            AutoCompleteStringCollection phone = new AutoCompleteStringCollection();
            foreach (var item in Cbll.ReadCustomerPhone())
            {
                phone.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = phone;
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in Pbll.ReadByProduct())
            {
                names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = names;
            AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
            foreach (var item in Ubll.ReadUserName())
            {
                Names.Add(item);
            }
            textBoxX5.AutoCompleteCustomSource = Names;
            FillData();
            label16.Text = Dbll.InvoiceCount();
        }
        void FillData()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = Ibll.ReadInvoice();
            dataGridView2.Columns["آیدی فاکتور"].Visible = false;
            dataGridView2.Columns["آیدی فاکتور در محصول"].Visible = false;
            dataGridView2.Columns["آیدی محصول"].Visible = false;

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxX1.Text == "")
                {
                    MessageBox.Show("لطفا شماره موبایل را وارد کنید", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxX1.Focus();
                }
                else 
                {
                    c = Cbll.ReadC(textBoxX1.Text);
                    textBoxX1.Enabled = false;
                    ShowName.Text = c.Name;
                    ShowPhone.Text = c.Phone;
                }              
            }
            catch (Exception)
            {
               ////////////
            }        
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxX2.Text == "")
                {
                    MessageBox.Show("لطفا محصول را وارد کنید", "اخطار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxX2.Focus();
                }
                else
                {
                    p = Pbll.ReadByNames(textBoxX2.Text);
                    products.Add(p);
                    textBoxX2.Text = "";
                    FillDataGrid();
                    string s = p.Name + " به ارزش " + p.Price.ToString("N0") + "تومان";
                    listBox1.Items.Add(s);
                    double sum = 0;
                    foreach(var item in products)
                    {
                        sum = sum + item.Price;
                    }
                    label11.Text = sum.ToString("N0");
                    label2.Text = (sum - Convert.ToDouble(textBoxX3.Text)).ToString("N0");                   
                }                              
            }
            catch (Exception)
            {
               /////////
            }            
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (textBoxX5.Text == "")
            {
                MessageBox.Show("نام کاربری را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxX5.Focus();
            }
            u = Ubll.ReadU(textBoxX5.Text);
            textBoxX5.Enabled = false;
        }
        User Lu = new User();
        bool Cheked()
        {
            bool isvalid = true;
            if (textBoxX1.Enabled != false || textBoxX1.Text == "")
            {
                MsgBox.MyShowDialog("خطای ورودی","شماره تماس را وارد کنید\n تیک را بزنید","",false,true);
                textBoxX1.Focus();
                isvalid = false;
            }
            else if(textBoxX5.Enabled != false || textBoxX5.Text == "")
            {
                MsgBox.MyShowDialog("خطای ورودی", "نام کاربری را وارد کنید\n تیک را بزنید", "", false, true);
                textBoxX5.Focus();
                isvalid = false;                          
            }else if (dataGridView1.DataSource == null)
            {
                MsgBox.MyShowDialog("خطای ورودی", "محصولی را وارد کنید\n تیک را بزنید", "", false, true);
                isvalid = false;
            }
            return isvalid;
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (Cheked())
            {
                try
                {
                    MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    Lu = w.LoggedInUser;
                    invoice i = new invoice();
                    i.RegDate = DateTime.Now;
                    if (checkBox1.Checked)
                    {
                        i.IsCheckout = true;
                        i.CheckoutDate = DateTime.Now;
                    }
                    else
                    {
                        i.IsCheckout = false;
                    }
                    if (Ubll.Access(Lu, "invoiceForm", 2))
                    {
                        DialogResult res = MsgBox.MyShowDialog("اطلاعیه", Ibll.Create(i, c,u, products) + "آیا قصد چاپ فاکتور را دارید؟", "Are you going to print the invoice?", true, false);
                        if (res == DialogResult.Yes)
                        {
                            StiReport sti = new StiReport();
                            sti.Load(@"D:\Project-cs\CRM-Project\DAL\bin\Debug\Invoice.mrt");
                            sti.Dictionary.Variables["InvoiceNumber"].Value = Ibll.ReadInvoiceNum();
                            sti.Dictionary.Variables["Date"].Value = ShowDate.Text;
                            sti.Dictionary.Variables["CustomerName"].Value = ShowName.Text;
                            sti.Dictionary.Variables["CustomerPhone"].Value = ShowPhone.Text;
                            sti.RegBusinessObject("Product", products);
                            sti.Render();
                            sti.Show();
                        }
                    }
                    else
                    {
                        MsgBox.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
                    }
                }
                catch (Exception)
                {
                    MsgBox.MyShowDialog("خطای چاپ فاکتور", "در چاپ فاکتور مشکلی به وجود آمد\nخطای مربوط به نرم افزار", "", false, true);
                }
            }        
                 FillData();
        }
        int IdRow;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                IdRow = Convert.ToInt32(dataGridView2.Rows[dataGridView2.CurrentRow.Index].Cells["آیدی فاکتور"].Value);
            }
            catch (Exception)
            {
                MessageBox.Show("آیدی مورد نظر برای ویرایش یافت نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contextMenuStrip1.Enabled = false;
            }

        }

        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"invoiceForm",4))
            {
                DialogResult res = MsgBox.MyShowDialog("اطلاعیه", "آیا از حذف فاکتور مورد نظر اطمینان دارید", "Are you sure to delete the desired invoice?", true, false);
                if (res == DialogResult.Yes)
                {
                    MsgBox.MyShowDialog("اطلاعیه", Ibll.Delete(IdRow), "The invoice was successfully deleted", false, false);

                }
            }
            else
            {
                MsgBox.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
           
            FillData();
        }

       
    }
}
