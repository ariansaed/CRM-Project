using BE;
using BLL;
using Stimulsoft.Report;
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
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }
        MsgBox m = new MsgBox();
        UserBLL Ubll = new UserBLL();
        private void label8_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool Cheked()
        {
            bool Value = true;
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked && !radioButton5.Checked)
            {
                m.MyShowDialog("خطا", "یک گزینه رو برای چاپ انتخاب کنید", "Select an option to print.", false, true);
                Value = false;
            }
            return Value;
        }
        private void label2_Click(object sender, EventArgs e)
        {
            if (Cheked())
            {
                try
                {
                    if (radioButton4.Checked)
                    {
                        StiReport sti = new StiReport();
                        sti.Load(@"D:\Project-cs\CRM-Project\DAL\bin\Debug\InvoiceReportMonth.mrt");
                        sti.Render();
                        sti.Show();
                    }
                    else if (radioButton1.Checked)
                    {
                        StiReport sti = new StiReport();
                        sti.Load(@"D:\Project-cs\CRM-Project\DAL\bin\Debug\RegisteredCustomer-Report.mrt");
                        sti.Render();
                        sti.Show();
                    }else if (radioButton2.Checked)
                    {
                        StiReport sti = new StiReport();
                        sti.Load(@"D:\Project-cs\CRM-Project\DAL\bin\Debug\RegisteredUserActivity.mrt");
                        sti.Render();
                        sti.Show();
                    }
                    else
                    {
                        m.MyShowDialog("خطای چاپ گزارش", "در چاپ گزارش مشکلی به وجود آمد", "", false, true);
                    }
                }
                catch (Exception)
                {
                    m.MyShowDialog("خطا", "گزارش چاپ نشد\nخطای مربوط به نرم افزار", "", false, true);
                }
            }
        }
        private void label7_Click(object sender, EventArgs e)
        {
            chart1.Series["Series1"].Points.Clear();
            if (radioButton8.Checked)
            {
                foreach(var item in Ubll.ReadInvoices())
                {
                    int x = 0;
                    foreach(var q  in item.Invoices)
                    {
                        if(q.RegDate.Date > dateTimeInput1.Value.Date && q.RegDate.Date < dateTimeInput2.Value.Date)
                        {
                            x++;
                        }
                    }
                    chart1.Series["Series1"].Points.AddXY(item.name,x);
                }
            }
            
            
              

        }
    }
} 