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
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }
        int id;
        int index;
        CustomerBLL bll = new CustomerBLL();   
        MsgBox MsgBox = new MsgBox();     
        UserBLL Ubll = new UserBLL();
        DashboardBLL Dbll = new DashboardBLL();
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadData();
            dataGridViewX1.Columns["ردیف"].Visible = false;
            label6.Text = Dbll.CustomerCount();
            textBoxX1.Text = "";
            textBoxX2.Text = "";
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CustomerForm_Load(object sender, EventArgs e)
        {         
           
            FillDataGrid();
            textBoxX1.Focus();
            buttonX3.Hide();
            
        }
        /// check TextBox in null 
        bool Checked()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "" || textBoxX1.Text == null)
            {
                MsgBox.MyShowDialog("خطا", "نام مشتری را وارد کنید", "Enter the customer's name", false, true);
                textBoxX1.Focus();
                isvalid = false;
            }
            else if (textBoxX2.Text == "" || textBoxX2.Text == null)
            {
                MsgBox.MyShowDialog("خطا", "شماره موبایل را وارد کنید", "Enter the mobile number", false, true);
                textBoxX2.Focus();
                isvalid = false;
            }
            else if (textBoxX2.Text.Length > 11 || textBoxX2.Text.Length < 11)
            {
                MsgBox.MyShowDialog("مورد اشتباه","تعداد کاراکترهای شماره موبایل کافی نیست", "The number of characters in the mobile number is not enough", false,true);
                textBoxX2.Focus();
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
                Customer c = new Customer();
                c.Name = textBoxX1.Text;
                c.Phone = textBoxX2.Text;
                c.RegDate = DateTime.Now;               
                if (buttonX1.Text == "ثبت مشتری")
                {
                    if (Ubll.Access(Lu,"CustomerForm",2))
                    {
                        MsgBox.MyShowDialog("اطلاعیه", bll.Create(c), "Create succefuly", false, false);
                    }
                    else
                    {
                        MsgBox.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
                    }                   
                }
                else if (buttonX1.Text == "ویرایش")
                {
                   
                        MsgBox.MyShowDialog("پیغام", bll.Update(id, c), "Update succefuly", false, false);
                        buttonX1.Text = "ثبت مشتری";                              
                }
                FillDataGrid();
            }          
        }
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["ردیف"].Value);
            }
            catch (Exception)
            {
                MessageBox.Show("آیدی مورد نظر برای ویرایش یافت نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contextMenuStrip1.Enabled = false;
            }          
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"CustomerForm",3))
            {
                DialogResult dr = MsgBox.MyShowDialog("سوال", "آیا می خواهید که مشتری را ویرایش کنید؟", "Do you want to edit the client?", true, false);
                if (dr == DialogResult.Yes)
                {
                    Customer c = bll.ReadByid(id);
                    textBoxX1.Text = c.Name;
                    textBoxX2.Text = c.Phone;
                    buttonX1.Text = "ویرایش";
                    buttonX3.Show();
                }
                else
                {
                    MessageBox.Show("ویرایش مشتری انجام نشد", "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MsgBox.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
           
          FillDataGrid();         
        }
        private void textBoxX3_TextChanged(object sender, EventArgs e)
        {
            if ((checkBox1.Checked && checkBox2.Checked) || (!checkBox2.Checked && !checkBox1.Checked))
            {
                index = 0;
            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {
                index = 1;
            }
            else if (checkBox2.Checked && !checkBox1.Checked)
            {
                index = 2;
            }         
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = bll.Search(textBoxX3.Text,index);                    
        }     
        private void textBoxX2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"CustomerForm",4))
            {
                DialogResult dr = MsgBox.MyShowDialog("سوال", "آیا از حذف مشتری اطمینان دارید؟", "Are you sure you want to delete the client?", true, false);
                if (dr == DialogResult.Yes)
                {
                    MessageBox.Show(bll.Delete(id), "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MsgBox.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
            }
           
            FillDataGrid();
        }
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (buttonX1.Text == "ویرایش")
            {
                textBoxX1.Text = "";
                textBoxX2.Text = "";
                buttonX1.Text = "ثبت مشتری";               
                buttonX3.Hide();
            }         
        }
    }
}
