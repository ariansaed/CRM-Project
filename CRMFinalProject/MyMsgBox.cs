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
    public partial class MyMsgBox : Form
    {
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
           int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse
           );   // Design Form
        public MyMsgBox()
        {
            InitializeComponent();            
            this.FormBorderStyle = FormBorderStyle.None;   // Design Form
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
        }
        private void MyMsgBox_Load(object sender, EventArgs e)
        {

        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (buttonX2.Text == "تایید")
            {
                this.DialogResult = DialogResult.Yes;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }
    }
}
