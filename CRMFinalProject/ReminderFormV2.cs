using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMFinalProject
{
    public partial class ReminderFormV2 : Form
    {
        public ReminderFormV2()
        {
            InitializeComponent();
        }
        ReminderBLL Rbll = new ReminderBLL();
        UserBLL Ubll = new UserBLL();
        User u = new User();
        User Lu = new User();
        MsgBox m = new MsgBox();
        DashboardBLL Dbll = new DashboardBLL();
        int IdRow;
        void FillData()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = Rbll.ReadData();
            dataGridViewX1.Columns["آیدی یادآور"].Visible = false;
            label3.Text = Dbll.ReminderCount();
        }
        private void ReminderFormV2_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection Name = new AutoCompleteStringCollection();
            foreach (var item in Ubll.ReadUserName())
            {
                Name.Add(item);
            }
            textBoxX1.AutoCompleteCustomSource = Name;
            FillData();
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            Lu = w.LoggedInUser;

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            textBoxX1.Enabled = false;
            u = Ubll.ReadU(textBoxX1.Text);
        }
        bool Check()
        {
            bool isvalid = true;
            if (textBoxX1.Enabled != false || textBoxX1.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن", "نام کاربری انتخاب نشده است", "", false, true);
                textBoxX1.Focus();
                isvalid = false;
            }
            else if (textBoxX2.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن", "موضوع یادآور را وارد کنید", "", false, true);
                textBoxX2.Focus();
                isvalid = false;
            }
            else if (richTextBox1.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن", "متن یادآور را وارد کنید", "", false, true);
                richTextBox1.Focus();
                isvalid = false;
            }
            else if (dateTimeInput1.Text == "")
            {
                m.MyShowDialog("خطای خالی بودن", "تاریخ یادآور را وارد کنید", "", false, true);
                dateTimeInput1.Focus();
                isvalid = false;
            }
            return isvalid;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {

            if (Check())
            {
                Reminder r = new Reminder();
                r.Users = u;
                r.Title = textBoxX2.Text;
                r.ReminderInfo = richTextBox1.Text;
                r.RegDate = DateTime.Now;
                r.RemindDate = dateTimeInput1.Value;
                if (buttonX1.Text == "ثبت یادآور")
                {
                    if (Ubll.Access(Lu, "ReminderForm", 2))
                    {
                        m.MyShowDialog("نتیجه ثبت یادآور", Rbll.Create(r, u), "", false, false);
                    }
                    else
                    {
                        m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
                    }
                }
                else if (buttonX1.Text == "ویرایش یادآور")
                {
                    m.MyShowDialog("ویرایش یادآور", Rbll.Update(r, IdRow), "", false, false);
                }
            }
            FillData();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                IdRow = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["آیدی یادآور"].Value);
            }
            catch (Exception)
            {
                MessageBox.Show("آیدی مورد نظر برای ویرایش یافت نشد", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                contextMenuStrip1.Enabled = false;
            }
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu, "ReminderForm", 3))
            {
                Reminder r = Rbll.ReadByid(IdRow);
                textBoxX2.Text = r.Title;
                richTextBox1.Text = r.ReminderInfo;
                dateTimeInput1.Value = r.RegDate;
                buttonX1.Text = "ویرایش یادآور";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
        }
        private void حذفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu, "ReminderForm",4))
            {
                try
                {
                    DialogResult dr = m.MyShowDialog("حذف یادآور", "آیا از حذف یادآور اطمینان دارید؟", "", true, false);
                    if (dr == DialogResult.Yes)
                    {
                        m.MyShowDialog("نتیجه حذف یادآور", Rbll.Delete(IdRow), "", true, false);
                    }
                }
                catch (Exception)
                {

                    m.MyShowDialog("","حذف یادآور انجام نشد\n لطفا بررسی کنید","",false,true);
                }            
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
        }
    }
}