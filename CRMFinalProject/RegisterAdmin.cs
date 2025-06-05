using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoxLearn.License;
using BLL;
using BE;
using System.IO;
using System.Security.Cryptography;

namespace CRMFinalProject
{
    public partial class RegisterAdmin : UserControl
    {
        public RegisterAdmin()
        {
            InitializeComponent();
        }
        MsgBox m = new MsgBox();
        Timer t1 = new Timer();
        UserBLL Ubll = new UserBLL();
        UserGroupBLL UGbll = new UserGroupBLL();
        OpenFileDialog ofd = new OpenFileDialog();
        Image Pic;
        void SwichPaneles()
        {
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer_Tick;
            t1.Start();
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
                File.Copy(picPath, path + PicName);
            }
            catch (Exception e)
            {

                MessageBox.Show(" سیستم قادر به ذخیره عکس نمیباشد! \n" + e.Message);
            }
            return path + PicName;
        }
        UserAccessRole FillAccessRole(string Section, bool CanEnter, bool CanCreate, bool CanUpdate, bool CanDelete)
        {
            UserAccessRole uar = new UserAccessRole();
            uar.Section = Section;
            uar.CanEnter = CanEnter;
            uar.CanCreate = CanCreate;
            uar.CanUpdate = CanUpdate;
            uar.CanDelete = CanDelete;
            return uar;
        }
        void CreateAdminGroup()
        {
            UserGroup ug = new UserGroup();
            ug.Title = "مدیریت";
            ug.UserAccessRoles.Add(FillAccessRole("CustomerForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("ProductForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("invoiceForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("ActivityForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("ReminderForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("UserForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("SmsPanelForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("ReportForm",true,true,true,true));
            ug.UserAccessRoles.Add(FillAccessRole("SettingForm",true,true,true,true));
            UGbll.Create(ug);
        }
        int y = 318;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (panel1.Location.Y < 755)
            {
                y = y + 15;
                panel1.Location = new Point(3,y);               
            }
            else
            {
                t1.Stop();
                panel2.Visible = true;
            }
        }
        private void RegisterAdmin_Load(object sender, EventArgs e)
        {
            textBoxX1.Text = ComputerInfo.GetComputerId();      
            textBoxX2.Focus();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            KeyManager km = new KeyManager(textBoxX1.Text);
            string productKey = textBoxX2.Text;
            if (km.ValidKey(ref productKey))
            {
                KeyValuesClass kv = new KeyValuesClass();
                if (km.DisassembleKey(productKey, ref kv))
                {
                    LicenseInfo lic = new LicenseInfo();
                    lic.ProductKey = productKey;
                    lic.FullName = "Personal accounting";
                    if (kv.Type == LicenseType.TRIAL)
                    {
                        lic.Day = kv.Expiration.Day;
                        lic.Month = kv.Expiration.Month;
                        lic.Year = kv.Expiration.Year;
                    }

                    km.SaveSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), lic);
                    m.MyShowDialog("اطلاعیه","نرم افزار شما با موفقیت انجام شد","",false,false);
                    SwichPaneles();
                }
            }
            else if(textBoxX2.Text == "" && textBoxX2.Text == null)
            {
                m.MyShowDialog("اخطار","برای فعال سازی لایسنس را وارد کنید","",false,true);
            }
            else
            {
                m.MyShowDialog("اخطار", " کد لایسنس اشتباه است ", "", false, true);
            }
                
        }
        bool Checked()
        {
           bool isvalid = true;
            if (textBoxX3 == null || textBoxX3.Text == "")
            {
                MessageBox.Show("نام خود را وارد کنید","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBoxX3.Focus();
                isvalid = false;
            }
            else if (textBoxX4 == null || textBoxX4.Text == "")
            {
                MessageBox.Show("نام کاربری را وارد کنید","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBoxX4.Focus();
                isvalid = false;
            }
            else if (textBoxX5.Text == null || textBoxX5.Text == "")
            {
                MessageBox.Show("رمز عبور خود را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxX5.Focus();
                isvalid = false;
            }
            else if (textBoxX6.Text == null || textBoxX6.Text == "")
            {
                MessageBox.Show("تکرار رمز عبور خود را وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxX6.Focus();
                isvalid = false;
            }
            return isvalid;
        }
       
        private void label6_Click(object sender, EventArgs e)
        {
            if (Checked())
            {
                User u = new User();
                CreateAdminGroup();
                u.name = textBoxX3.Text;
                u.UserName = textBoxX4.Text;
                if (textBoxX5.Text == textBoxX6.Text)
                {
                    u.Password = textBoxX5.Text;
                }
                else
                {
                    m.MyShowDialog("اخطار", "کلمه عبور و تکرار آن با هم برابر نیستند", "", false, true);
                }
                u.RegDate = DateTime.Now;
                u.Pic = SavePic(textBoxX4.Text);
                m.MyShowDialog("نتیجه ثبت", Ubll.Create(u, UGbll.ReadName("مدیریت")), "", false, false);
                this.Visible = false;

                ((LoginForm)Application.OpenForms["LoginForm"]).LoadLoginForm();
            }       
        }    
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "JPG(*.JPG)|*.JPG";
            ofd.Title = "تصویر کاربر را انتخاب کنید";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Pic = Image.FromFile(ofd.FileName);
                pictureBox1.Image = Pic;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text != null && textBoxX1.Text != "")
            {
                Clipboard.SetText(textBoxX1.Text);
                MessageBox.Show("کد سیستم با موفقیت کپی شد", "موفقیت", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("کلید لایسنس خالی است", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
