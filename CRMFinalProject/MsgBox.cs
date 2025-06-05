using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRMFinalProject
{
    public class MsgBox
    {
        public DialogResult MyShowDialog(string title,string fainfo,string enginfo,bool buttons,bool type)
        {
            MyMsgBox m = new MyMsgBox();
            m.label1.Text = title;
            m.label2.Text = fainfo;
            m.label3.Text = enginfo;
            if (buttons)
            {
                m.buttonX1.Text = "خیر";
            }
            else
            { 
                m.buttonX2.Visible = false;
            }
            if (type)            
            {
                m.BackColor = Color.FromArgb(208,0,0);
                m.pictureBox1.Image = Properties.Resources.icons8_error_50;
            }
            m.ShowDialog();
            return m.DialogResult;
        }
    }
}
