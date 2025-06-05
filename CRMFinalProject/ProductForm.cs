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

namespace CRMFinalProject
{
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }

        ProductBLL bll = new ProductBLL();
        Product p = new Product();
        UserBLL Ubll = new UserBLL();
        MsgBox m = new MsgBox();
        DashboardBLL Dbll = new DashboardBLL();
        int id;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";
        }
        bool Checked()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی ها","لطفا نام محصول را وارد کنید","",false,true);
                textBoxX1.Focus();
                isvalid = false;
            }
            else if (textBoxX2.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی ها", "لطفا قیمت محصول را وارد کنید", "", false, true);
                textBoxX2.Focus();
                isvalid = false;
            }
            else if(textBoxX3.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی ها", "لطفا نام تعداد محصول را وارد کنید", "", false, true); ;
                textBoxX3.Focus();
                isvalid = false;
            }
            return isvalid; 
        }
        User Lu = new User();
        private void buttonX1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (Checked())
            {
                Product p = new Product();
                p.Name = textBoxX1.Text;
                p.Price = Convert.ToDouble(textBoxX2.Text);
                p.Stock = Convert.ToInt32(textBoxX3.Text);
                if (buttonX1.Text == "ثبت محصول")
                {
                    if (Ubll.Access(Lu,"ProductForm",2))
                    {
                        m.MyShowDialog("ثبت کالا",bll.Create(p),"Create Succeful Product",false,false);
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
                    }
                }
                else if (buttonX1.Text == "ویرایش اطلاعات")
                {
                    m.MyShowDialog("نتیجه بروز رسانی",bll.Update(id,p),"",false,false);
                }
                textBoxX1.Text = "";
                textBoxX2.Text = "";
                textBoxX3.Text = "";
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = bll.ReadData();
            }           
        }
        private void textBoxX4_TextChanged(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.Read(textBoxX4.Text);
        }
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی محصول"].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("آیدی مورد نظر برای ویرایش یافت نشد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                contextMenuStrip1.Enabled = false;                             
            }          
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"ProductForm",3))
            {
                Product p = bll.ReadByid(id);
                textBoxX1.Text = p.Name;
                textBoxX2.Text = p.Price.ToString();
                textBoxX3.Text = p.Stock.ToString();
                buttonX1.Text = "ویرایش اصلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
            }         
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadData();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadData();
            label8.Text = Dbll.ProductCount();
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Enabled = true;
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"ProductForm",4))
            {
                DialogResult dr = m.MyShowDialog("حذف کالا", "آیا از حدف کالا اطمینان دارید", "", true, false);
                if (dr == DialogResult.Yes)
                {
                    m.MyShowDialog("نتیجه حذف کالا", bll.Delete(id), "", false, false);
                }
                else
                {
                    m.MyShowDialog("نتیجه حذف کالا","کالای مورد نظر حذف نشد","",false,true);
                }
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadData();
        }
    }

}
