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
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
        }
        UserBLL Ubll = new UserBLL();
        User U = new User();
        MsgBox m = new MsgBox();
        DashboardBLL Dbll = new DashboardBLL();
        public User LoggedInUser = new User();
        Reminder R = new Reminder();
        private void LoginUC_Load(object sender, EventArgs e)
        {
            textBoxX4.Focus();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            U = Ubll.Login(textBoxX4.Text, textBoxX5.Text);
            if (U != null)
            {
                m.MyShowDialog("خوش آمدید", "برای ورود به نرم افزار کلیک کنید", "", false, false);
                MainWindow w = (MainWindow)System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                w.LoggedInUser = U;
                w.UserNametxt.Text = U.UserName;
                w.PersonNametxt.Text = U.name;
                w.ReminderCountTxt.Text = Dbll.UserRemindersCount(U);
                w.CustomerCountTxt.Text = Dbll.CustomerCount();
                w.SellsCountTxt.Text = Dbll.SellsCount();           
                ((LoginForm)System.Windows.Forms.Application.OpenForms["LoginForm"]).Close();
            }
            else 
            {
                m.MyShowDialog("خطای ورود به برنامه", "نام کاربری و رمز عبور اشتباه است", "", false,true);
            }
        }

       
    }
}
