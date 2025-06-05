using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BLL;
using BE;
using DevComponents.DotNetBar.Controls;

namespace CRMFinalProject
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
        Image pic;
        int id;
        UserBLL bll = new UserBLL();
        MsgBox m = new MsgBox();
        UserGroup ug = new UserGroup();
        UserGroupBLL UGbll = new UserGroupBLL();
        User Lu = new User();
        UserBLL Ubll = new UserBLL();
        UserAccessRole FillAccessRole(string Section,bool CanEnter,bool CanCreate,bool CanUpdate,bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CanDelete = CanDelete;
            return uar;
        }
        
        void ClearText()
        {
            textBoxX1.Text = "";
            textBoxX2.Text = "";
            textBoxX3.Text = "";
            textBoxX4.Text = "";
            comboBoxEx1.Text = "";
        }
        void FillDataGrid()
        {
            dataGridViewX1.DataSource = null;
            dataGridViewX1.DataSource = bll.ReadDataUser();
            dataGridViewX1.Columns["ردیف"].Visible = false;
        }
        string SavePic(string UserName)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\UserPics\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string PicName = UserName + ".JPG";
            try
            {
                string picPath = ofd.FileName;
                File.Copy(picPath,path + PicName);
            }
            catch (Exception e)
            {

                MessageBox.Show(" سیستم قادر به ذخیره عکس نمیباشد! \n" + e.Message);
            }
            return path + PicName;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";           
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pic = Image.FromFile(ofd.FileName);
                pictureBox1.Image = pic;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }    
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.name = textBoxX1.Text;
            u.UserName = textBoxX2.Text;
            u.RegDate = DateTime.Now;
            ug = UGbll.ReadName(comboBoxEx1.Text);
            if (textBoxX3.Text == textBoxX4.Text)
            {
                u.Password = textBoxX4.Text;
               
            }
            else
            {
                m.MyShowDialog("خطای رمز اشتباه","رمز عبور و تکرار آن با هم برابر نیستند","",false,true);
            }
            if (label1.Text == "ثبت اطلاعات")
            {
                if (Ubll.Access(Lu,"UserForm",2))
                {
                    u.Pic = SavePic(textBoxX2.Text);
                    m.MyShowDialog("ثبت کاربر",bll.Create(u,ug),"",false,false);
                }
                else
                {
                    m.MyShowDialog("محدودیت دسترسی","شما اجازه انجام این کار را ندارید","",false,true);
                }
            }
            else if (label1.Text == "ویرایش اطلاعات")
            {
                u.Pic = SavePic(textBoxX2.Text);
                m.MyShowDialog("ثبت کاربر",bll.Update(u,id),"", false, false);
            }            
            FillDataGrid();
            ClearText();
            FillCombobox();
        }
        public void FillCombobox()
        {
            comboBoxEx1.DataSource = UGbll.ReadUserGroup();
            comboBoxEx1.DisplayMember = "Title";
        }
        private void UserForm_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection Name = new AutoCompleteStringCollection();
            foreach(var item in UGbll.ReadUserGroup())
            {
                Name.Add(item);
            }
            textBoxX6.AutoCompleteCustomSource = Name;
            AutoCompleteStringCollection name = new AutoCompleteStringCollection();
            foreach (var item in UGbll.ReadUserGroup())
            {
                name.Add(item);
            }
            comboBoxEx1.AutoCompleteCustomSource = name;
            FillDataGrid();
            MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            Lu = w.LoggedInUser;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                id = Convert.ToInt32(dataGridViewX1.Rows[dataGridViewX1.CurrentRow.Index].Cells["ردیف"].Value);
            }
            catch (Exception)
            {

                MessageBox.Show("آیدی مورد نظر یافت نشد","خطا",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }        
        }
        private void ویرایشToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Ubll.Access(Lu,"UserForm",3))
            {
                User u = bll.Read(id);
                textBoxX1.Text = u.name;
                textBoxX2.Text = u.UserName;
                pictureBox1.Image = Image.FromFile(u.Pic);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                label1.Text = "ویرایش اطلاعات";
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه انجام این کار را ندارید", "", false, true);
            }
           
        }      
        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// بخش امنیت کاربران 
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) 
            { 
                checkBox5.Checked = true;
                checkBox6.Checked = true;
                checkBox7.Checked = true;
                checkBox8.Checked = true;
                checkBox9.Checked = true;
                checkBox10.Checked = true;
                checkBox11.Checked = true;
                checkBox12.Checked = true;
                checkBox13.Checked = true;
            }
            else
            {
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox9.Checked = false;
                checkBox10.Checked = false;
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox14.Checked = true;
                checkBox15.Checked = true;
                checkBox16.Checked = true;
                checkBox17.Checked = true;
                checkBox18.Checked = true;
                checkBox19.Checked = true;
                checkBox20.Checked = true;
                checkBox21.Checked = true;
                checkBox22.Checked = true;
            }
            else 
            {
                checkBox14.Checked = false;
                checkBox15.Checked = false;
                checkBox16.Checked = false;
                checkBox17.Checked = false;
                checkBox18.Checked = false;
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox22.Checked = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox23.Checked = true;
                checkBox24.Checked = true;
                checkBox25.Checked = true;
                checkBox26.Checked = true;
                checkBox27.Checked = true;
                checkBox28.Checked = true;
                checkBox29.Checked = true;
                checkBox30.Checked = true;
                checkBox31.Checked = true;

            }
            else 
            {
                checkBox23.Checked = false;
                checkBox24.Checked = false;
                checkBox25.Checked = false;
                checkBox26.Checked = false;
                checkBox27.Checked = false;
                checkBox28.Checked = false;
                checkBox29.Checked = false;
                checkBox30.Checked = false;
                checkBox31.Checked = false;                              
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox32.Checked = true;
                checkBox33.Checked = true;
                checkBox34.Checked = true;
                checkBox35.Checked = true;
                checkBox36.Checked = true;
                checkBox37.Checked = true;
                checkBox38.Checked = true;
                checkBox39.Checked = true;
                checkBox40.Checked = true;
            }
            else 
            {
                checkBox32.Checked = false;
                checkBox33.Checked = false;
                checkBox34.Checked = false;
                checkBox35.Checked = false;
                checkBox36.Checked = false;
                checkBox37.Checked = false;
                checkBox38.Checked = false;
                checkBox39.Checked = false;
                checkBox40.Checked = false;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            UserGroup ug = new UserGroup();
            ug.Title = textBoxX6.Text;
            ug.UserAccessRoles.Add(FillAccessRole(label4.Text = "CustomerForm", checkBox5.Checked, checkBox14.Checked, checkBox23.Checked, checkBox32.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label5.Text = "ProductForm", checkBox6.Checked, checkBox15.Checked, checkBox24.Checked, checkBox33.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label6.Text = "invoiceForm", checkBox7.Checked, checkBox16.Checked, checkBox25.Checked, checkBox34.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label7.Text = "ActivityForm", checkBox8.Checked, checkBox17.Checked, checkBox26.Checked, checkBox35.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label8.Text = "ReminderForm", checkBox9.Checked, checkBox18.Checked, checkBox27.Checked, checkBox36.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label9.Text = "UserForm", checkBox10.Checked, checkBox19.Checked, checkBox28.Checked, checkBox37.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label10.Text = "SmsPanelForm", checkBox11.Checked, checkBox20.Checked, checkBox29.Checked, checkBox38.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label11.Text = "ReportForm", checkBox12.Checked, checkBox21.Checked, checkBox30.Checked, checkBox39.Checked)); 
            ug.UserAccessRoles.Add(FillAccessRole(label12.Text = "",checkBox13.Checked, checkBox22.Checked, checkBox31.Checked, checkBox40.Checked));
            m.MyShowDialog("نتیجه ثبت اطلاعات", UGbll.Create(ug), "", false, false);
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
