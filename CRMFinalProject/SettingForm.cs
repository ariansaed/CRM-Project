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
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }
        int id;
        ActivityCategoryBLL bll = new ActivityCategoryBLL();
        private List<string> enteredNames = new List<string>();
        UserBLL Ubll = new UserBLL();
        User Lu = new User();
        MsgBox m = new MsgBox();
        bool Checked()
        {
            bool isvalid = true;
            if (textBoxX1.Text == "")
            {
                MessageBox.Show("نام دسته بندی را وارد کنید","خطا",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                textBoxX1.Focus();
                isvalid = false;
            }           
            return isvalid;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (Checked())
            {
                ActivityCategory a = new ActivityCategory();               
                a.CategoryName = textBoxX1.Text;
                if (buttonX1.Text == "ثبت دسته بندی")
                {
                    if (Ubll.Access(Lu,"SettingForm",2))
                    {
                        m.MyShowDialog("ثبت دسته بندی",bll.Create(a),"",false,false);
                    }
                }
                else if (buttonX1.Text == "ویرایش دسته بندی")
                {
                    m.MyShowDialog("ثبت دسته بندی", bll.Update(a,id), "", false, false);
                    buttonX1.Text = "ثبت دسته بندی";
                }              
                textBoxX1.Text = "";
                dataGridViewX1.DataSource = null;
                dataGridViewX1.DataSource = bll.ReadData();
                dataGridViewX1.Columns["ردیف"].Visible = false;
            }          
        }
        private void SettingForm_Load(object sender, EventArgs e)
        {
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            Lu = w.LoggedInUser;
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadData();
            dataGridViewX1.Columns["ردیف"].Visible = false;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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
            if (Ubll.Access(Lu,"SettingForm",3))
            {
                ActivityCategory Ac = bll.ReadByid(id);
                textBoxX1.Text = Ac.CategoryName;
                buttonX1.Text = "ویرایش دسته بندی";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
            }
           
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"SettingForm",4))
            {
                DialogResult dr = MessageBox.Show("آیا از حذف دسته بندی اطمینان دارید؟", "اطلاعیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    MessageBox.Show(bll.Delete(id), "پیغام", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }       
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadData();
            dataGridViewX1.Columns["ردیف"].Visible = false;
        }
    }
}
