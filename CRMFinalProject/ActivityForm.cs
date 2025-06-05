using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BE;

namespace CRMFinalProject
{
    public partial class ActivityForm : Form
    {
        public ActivityForm()
        {
            InitializeComponent();
        }
        ActivityCategory Ac = new ActivityCategory();
        ActivityCategoryBLL Acbll = new ActivityCategoryBLL();
        ///
        Customer c = new Customer();
        CustomerBLL cbll = new CustomerBLL();
        ///                 
        User u = new User();
        UserBLL ubll = new UserBLL();
        ///
        activityBLL Abll = new activityBLL();
        ReminderBLL Rbll = new ReminderBLL();
        MsgBox m = new MsgBox();
        DashboardBLL Dbll = new DashboardBLL();
        int id;
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection Phone = new AutoCompleteStringCollection();
            foreach (var item in cbll.ReadCustomerPhone()) 
            { 
                Phone.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = Phone;
            textBoxX1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxX1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            foreach (var item in ubll.ReadUserName())
            {
                names.Add(item);
            }
            textBoxX2.AutoCompleteCustomSource = names;
            textBoxX2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxX2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection AcName = new AutoCompleteStringCollection();
            foreach (var item in Acbll.AcName())
            {
                AcName.Add(item);
            }
            textBoxX6.AutoCompleteCustomSource = AcName;           
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Abll.ReadDataActivity();
            label7.Text = Dbll.ActivityConut();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            c = cbll.ReadC(textBoxX1.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            textBoxX2.Enabled = false;
            u = ubll.ReadU(textBoxX2.Text);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            textBoxX6.Enabled = false; 
            Ac = Acbll.ReadCategoryList(textBoxX6.Text);
        }
        bool Cheked()
        {
            bool isvalid = true;
            if (textBoxX1.Enabled != false || textBoxX1.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی","شماره تماس مشتری را وارد کنید", "",false,true);               
                textBoxX1.Focus();
                isvalid = false;
            }
            else if (textBoxX2.Enabled != false || textBoxX2.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی", "نام کاربری را وارد کنید", "", false, true);
                textBoxX2.Focus();
                isvalid = false;
            }
            else if (textBoxX6.Enabled != false || textBoxX6.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی", "دسته بندی فعالیت را وارد کنید", "", false, true);
                textBoxX6.Focus();
                isvalid = false;
            }
            else if (textBoxX4.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی", "موضوع فعالیت را وارد کنید", "", false, true);
                textBoxX4.Focus();
                isvalid = false;
            }
            else if (richTextBox1.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن ورودی", "توضیحات فعالیت را وارد کنید", "", false, true);
                richTextBox1.Focus();
                isvalid = false;
            }
            return isvalid;
        }
        User Lu = new User();
        private void buttonX1_Click(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            Lu = w.LoggedInUser;
            if (Cheked())
            {
                Activity a = new Activity();
                a.Title = textBoxX4.Text;
                a.Info = richTextBox1.Text;
                a.RegDate = DateTime.Now;
                if (ubll.Access(Lu, "ActivityForm", 2))
                {
                    m.MyShowDialog("ثبت کالا", Abll.Create(a, u, c, Ac), "", false, false);
                }
                else
                {
                    m.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
                }              
                if (checkBox3.Checked)
                {
                    Reminder r = new Reminder();
                    r.Title = textBoxX4.Text;
                    r.ReminderInfo = richTextBox1.Text;
                    r.RegDate = DateTime.Now;
                    r.RemindDate = dateTimeInput1.Value;                
                    m.MyShowDialog("ثبت یادآور", Rbll.Create(r, u), "", false, false);
                    dataGridViewX1.DataSource = null;
                    dataGridViewX1.DataSource = Abll.ReadDataActivity();
                }
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
               m.MyShowDialog("خطای حذف", "آیدی مورد یافت نشد", "",false,true);
                contextMenuStrip1.Enabled = false;
            }
        }
        private void نمایشجزئیاتToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            m.MyShowDialog("نمایش جزئیات", Abll.ReadByinfo(id),"",false,false);
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ubll.Access(Lu, "ActivityForm", 4))
            {
                try
                {
                    DialogResult dr =m.MyShowDialog("سوال","آیا از حذف فعالیت اطمینان دارید؟", "", true,false);
                    if (dr == DialogResult.Yes)
                    {
                        m.MyShowDialog("پیغام", Abll.Delete(id), "",false,false);
                    }
                    dataGridViewX1.DataSource = null;
                    dataGridViewX1.DataSource = Abll.ReadDataActivity();
                }
                catch (Exception)
                {
                    m.MyShowDialog("","حذف فعالیت انجام نشد\nلطفا بررسی کنید","",false,true);
                }
            }
            else            
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
           
        }
    }
}
