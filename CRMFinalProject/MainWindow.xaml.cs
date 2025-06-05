using BE;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CRMFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginForm f = new LoginForm();
            InitializeComponent();
            OpenWinForm(f);                                 
       }
        public User LoggedInUser = new User();
        UserBLL Ubll = new UserBLL();
        MsgBox m = new MsgBox();
        DashboardBLL Dbll = new DashboardBLL();
        void OpenWinForm(Form f)
        {
            BlurEffect bme = new BlurEffect();
            this.Effect = bme;
            bme.Radius = 15;
            f.ShowDialog();
            Effect = null;
        }
        void RefreshPage()
        {
            UserNametxt.Text = LoggedInUser.UserName;
            PersonNametxt.Text = LoggedInUser.name;
            ReminderCountTxt.Text = Dbll.UserRemindersCount(LoggedInUser);
            CustomerCountTxt.Text = Dbll.CustomerCount();
            SellsCountTxt.Text = Dbll.SellsCount();
            int a = 0;
            foreach (var item in Dbll.GetReminders(LoggedInUser))
            {
                if (a <= 7)
                {
                    ReminderUC r = new ReminderUC();
                    r.ReminderTitleTxt.Text = item.Title;
                    r.ReminderInfoTxt.Text = item.ReminderInfo.ToString();
                    Grid.SetRow(r, 5 + a);
                    Grid.SetColumnSpan(r, 6);
                    MainGrid.Children.Add(r);
                    a++;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Ubll.Access(LoggedInUser, "CustomerForm", 1))
               {
                    CustomerForm f = new CustomerForm();                   
                    OpenWinForm(f);
                    RefreshPage();
                }
                else
                {
                    m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
                }
            }
            catch (Exception )
            {

                m.MyShowDialog("خطا","در باز کردن فرم مشکلی پیش امد","",false,true);
               
            }
        }
            
        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"invoiceForm", 1))
            {
                invoceForm f = new invoceForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
            
        }

        private void TextBlock_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"ActivityForm",1))
            {
                ActivityForm f = new ActivityForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
        }

        private void TextBlock_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {

            if (Ubll.Access(LoggedInUser,"ReminderForm",1))
            {
                ReminderFormV2 f = new ReminderFormV2();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
        }

        private void TextBlock_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"SmsPanelForm",1))
            {
                SmsPanelForm f = new SmsPanelForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
        }

        private void TextBlock_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"UserForm",1))
            {
                UserForm f = new UserForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
        }

        private void TextBlock_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"ProductForm",1))
            {
                ProductForm f = new ProductForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Ubll.Access(LoggedInUser,"SettingForm",1))
            {
                SettingForm f = new SettingForm();
                OpenWinForm(f);
                RefreshPage();
            }
            else
            {
                m.MyShowDialog("محدودیت دسترسی", "شما اجازه ورود به این بخش را ندارید", "", false, true);
            }
        }

        private void TextBlock_MouseLeftButtonDown_8(object sender, MouseButtonEventArgs e)
        {
            ReportForm f = new ReportForm();
            OpenWinForm(f);
        }
    }
}
