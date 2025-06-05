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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            this.Controls.Add(r);
            this.Controls["RegisterAdmin"].Location = new Point(400, 830);
            this.Controls.Add(l);
            this.Controls["LoginUC"].Location = new Point(400, 830);
            InitializeComponent();
        }
        Timer t1 = new Timer();
        Timer t2 = new Timer();
        Timer t3 = new Timer();
        UserBLL Ubll = new UserBLL();

        List<string> username = new List<string>();
        RegisterAdmin r = new RegisterAdmin();
        LoginUC l = new LoginUC();
        bool _IsRegistered;       
        public void LoadLoginForm()
        {
            t3.Enabled = true;
            t3.Interval = 15;
            t3.Tick += Timer3_Tick;
            t3.Start();

        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            label2.Visible = true;
            t1.Enabled = true;
            t1.Interval = 15;
            t1.Tick += Timer1_Tick;
            t1.Start();            
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBarX1.Value >= 100)
            {
                t1.Stop();
                progressBarX1.Visible = false;
                label2.Visible = false;
                label1.Visible = true;    
                t2.Enabled = true;
                t2.Interval = 1;
                t2.Tick += Timer2_Tick;
                t2.Start();
            }
            else if (progressBarX1.Value >= 50)
            {
                _IsRegistered = Ubll.IsRegistered();
                progressBarX1.Value++;
            }
            else 
            {
                progressBarX1.Value++;
            }
        }
        int y = 390;
        int y2 = 830;
        int y3 = 830;
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (label1.Location.Y >= 50)
            {               
                y = y - 15;
                y2 = y2 - 30;
                label1.Location = new Point(468, y);
                if (_IsRegistered)
                {
                    this.Controls["LoginUC"].Location = new Point(400, y2);
                }
                else
                {
                _IsRegistered = false;
                this.Controls["RegisterAdmin"].Location = new Point(400, y2);
                }
            }
            else
            {
                t2.Stop();
                r.panel1.Visible = true;
            
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }
        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (this.Controls["LoginUC"].Location.Y >= 100)
            {
                y = y - 15;
                y3 = y3 - 30;
                this.Controls["LoginUC"].Location = new Point(400, y3);
            }
            else
            {
                t3.Stop();
            }
        }
        
    }
}
