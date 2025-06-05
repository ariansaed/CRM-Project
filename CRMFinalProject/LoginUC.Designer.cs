namespace CRMFinalProject
{
    partial class LoginUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.symbolBox1 = new DevComponents.DotNetBar.Controls.SymbolBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxX5 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.textBoxX4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // symbolBox1
            // 
            // 
            // 
            // 
            this.symbolBox1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.symbolBox1.Location = new System.Drawing.Point(178, 15);
            this.symbolBox1.Name = "symbolBox1";
            this.symbolBox1.Size = new System.Drawing.Size(223, 228);
            this.symbolBox1.Symbol = "";
            this.symbolBox1.SymbolColor = System.Drawing.SystemColors.Window;
            this.symbolBox1.TabIndex = 1;
            this.symbolBox1.Text = "symbolBox1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("IRANSansWeb", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(534, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "برای ورود به حساب کاربری اطلاعات خود را کامل کنید";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.textBoxX5);
            this.panel3.Controls.Add(this.textBoxX4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Location = new System.Drawing.Point(3, 287);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(588, 211);
            this.panel3.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Image = global::CRMFinalProject.Properties.Resources.icons8_check_mark_48;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(54, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 44);
            this.label6.TabIndex = 5;
            this.label6.Text = "ورود به حساب کاربری";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBoxX5
            // 
            // 
            // 
            // 
            this.textBoxX5.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX5.Border.Class = "TextBoxBorder";
            this.textBoxX5.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX5.Location = new System.Drawing.Point(60, 91);
            this.textBoxX5.Name = "textBoxX5";
            this.textBoxX5.PreventEnterBeep = true;
            this.textBoxX5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxX5.Size = new System.Drawing.Size(469, 49);
            this.textBoxX5.TabIndex = 3;
            this.textBoxX5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX5.WatermarkText = "رمز عبور";
            // 
            // textBoxX4
            // 
            // 
            // 
            // 
            this.textBoxX4.Border.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxX4.Border.Class = "TextBoxBorder";
            this.textBoxX4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.textBoxX4.Location = new System.Drawing.Point(60, 36);
            this.textBoxX4.Name = "textBoxX4";
            this.textBoxX4.PreventEnterBeep = true;
            this.textBoxX4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBoxX4.Size = new System.Drawing.Size(469, 49);
            this.textBoxX4.TabIndex = 2;
            this.textBoxX4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxX4.WatermarkText = "نام کاربری";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(184, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(246, 33);
            this.label5.TabIndex = 0;
            this.label5.Text = "وارد حساب کاربری خود شوید\r\n";
            // 
            // LoginUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(25)))), ((int)(((byte)(139)))));
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.symbolBox1);
            this.Font = new System.Drawing.Font("IRANSansWeb", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Window;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "LoginUC";
            this.Size = new System.Drawing.Size(600, 775);
            this.Load += new System.EventHandler(this.LoginUC_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.SymbolBox symbolBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX5;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX4;
        private System.Windows.Forms.Label label5;
    }
}
