namespace FPClient
{
    partial class SysInfo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelInfo = new System.Windows.Forms.Label();
            this.btnGetDeviceTime = new FP_CLOCK.CustomButton();
            this.btnSetDeviceTime = new FP_CLOCK.CustomButton();
            this.btnPowerOnDev = new FP_CLOCK.CustomButton();
            this.btnPowerOffDev = new   FP_CLOCK.CustomButton();
            this.btnDisableDevice = new FP_CLOCK.CustomButton();
            this.btnOK = new FP_CLOCK.CustomButton();
            this.btnGetDevInfo = new FP_CLOCK.CustomButton();
            this.btnSetDevInfo = new FP_CLOCK.CustomButton();
            this.btnGetDevStatus = new FP_CLOCK.CustomButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbItemList = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textStatusInfo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInfo.Location = new System.Drawing.Point(12, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(640, 44);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetDeviceTime
            // 
            this.btnGetDeviceTime.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetDeviceTime.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetDeviceTime.BorderRadius = 20;
            this.btnGetDeviceTime.BorderSize = 0;
            this.btnGetDeviceTime.FlatAppearance.BorderSize = 0;
            this.btnGetDeviceTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDeviceTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetDeviceTime.Location = new System.Drawing.Point(61, 64);
            this.btnGetDeviceTime.Name = "btnGetDeviceTime";
            this.btnGetDeviceTime.Size = new System.Drawing.Size(156, 45);
            this.btnGetDeviceTime.TabIndex = 3;
            this.btnGetDeviceTime.Text = "GetDeviceTime";
            this.btnGetDeviceTime.UseVisualStyleBackColor = false;
            this.btnGetDeviceTime.Click += new System.EventHandler(this.btnGetDeviceTime_Click);
            // 
            // btnSetDeviceTime
            // 
            this.btnSetDeviceTime.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSetDeviceTime.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSetDeviceTime.BorderRadius = 20;
            this.btnSetDeviceTime.BorderSize = 0;
            this.btnSetDeviceTime.FlatAppearance.BorderSize = 0;
            this.btnSetDeviceTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetDeviceTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSetDeviceTime.Location = new System.Drawing.Point(61, 136);
            this.btnSetDeviceTime.Name = "btnSetDeviceTime";
            this.btnSetDeviceTime.Size = new System.Drawing.Size(156, 45);
            this.btnSetDeviceTime.TabIndex = 3;
            this.btnSetDeviceTime.Text = "SetDeviceTime";
            this.btnSetDeviceTime.UseVisualStyleBackColor = false;
            this.btnSetDeviceTime.Click += new System.EventHandler(this.btnSetDeviceTime_Click);
            // 
            // btnPowerOnDev
            // 
            this.btnPowerOnDev.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPowerOnDev.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnPowerOnDev.BorderRadius = 20;
            this.btnPowerOnDev.BorderSize = 0;
            this.btnPowerOnDev.FlatAppearance.BorderSize = 0;
            this.btnPowerOnDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPowerOnDev.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPowerOnDev.Location = new System.Drawing.Point(269, 64);
            this.btnPowerOnDev.Name = "btnPowerOnDev";
            this.btnPowerOnDev.Size = new System.Drawing.Size(139, 45);
            this.btnPowerOnDev.TabIndex = 3;
            this.btnPowerOnDev.Text = "PowerOnDevice";
            this.btnPowerOnDev.UseVisualStyleBackColor = false;
            this.btnPowerOnDev.Click += new System.EventHandler(this.btnPowerOnDev_Click);
            // 
            // btnPowerOffDev
            // 
            this.btnPowerOffDev.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnPowerOffDev.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnPowerOffDev.BorderRadius = 20;
            this.btnPowerOffDev.BorderSize = 0;
            this.btnPowerOffDev.FlatAppearance.BorderSize = 0;
            this.btnPowerOffDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPowerOffDev.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnPowerOffDev.Location = new System.Drawing.Point(269, 136);
            this.btnPowerOffDev.Name = "btnPowerOffDev";
            this.btnPowerOffDev.Size = new System.Drawing.Size(139, 45);
            this.btnPowerOffDev.TabIndex = 3;
            this.btnPowerOffDev.Text = "PowerOffDevice";
            this.btnPowerOffDev.UseVisualStyleBackColor = false;
            this.btnPowerOffDev.Click += new System.EventHandler(this.btnPowerOffDev_Click);
            // 
            // btnDisableDevice
            // 
            this.btnDisableDevice.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnDisableDevice.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDisableDevice.BorderRadius = 20;
            this.btnDisableDevice.BorderSize = 0;
            this.btnDisableDevice.FlatAppearance.BorderSize = 0;
            this.btnDisableDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisableDevice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDisableDevice.Location = new System.Drawing.Point(461, 77);
            this.btnDisableDevice.Name = "btnDisableDevice";
            this.btnDisableDevice.Size = new System.Drawing.Size(123, 45);
            this.btnDisableDevice.TabIndex = 3;
            this.btnDisableDevice.Text = "DisableDevice";
            this.btnDisableDevice.UseVisualStyleBackColor = false;
            this.btnDisableDevice.Click += new System.EventHandler(this.btnDisableDevice_Click);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnOK.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnOK.BorderRadius = 20;
            this.btnOK.BorderSize = 0;
            this.btnOK.FlatAppearance.BorderSize = 0;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnOK.Location = new System.Drawing.Point(461, 136);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(139, 45);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnGetDevInfo
            // 
            this.btnGetDevInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetDevInfo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetDevInfo.BorderRadius = 20;
            this.btnGetDevInfo.BorderSize = 0;
            this.btnGetDevInfo.FlatAppearance.BorderSize = 0;
            this.btnGetDevInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDevInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetDevInfo.Location = new System.Drawing.Point(61, 291);
            this.btnGetDevInfo.Name = "btnGetDevInfo";
            this.btnGetDevInfo.Size = new System.Drawing.Size(139, 45);
            this.btnGetDevInfo.TabIndex = 3;
            this.btnGetDevInfo.Text = "GetDeviceInfo";
            this.btnGetDevInfo.UseVisualStyleBackColor = false;
            this.btnGetDevInfo.Click += new System.EventHandler(this.btnGetDevInfo_Click);
            // 
            // btnSetDevInfo
            // 
            this.btnSetDevInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSetDevInfo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSetDevInfo.BorderRadius = 20;
            this.btnSetDevInfo.BorderSize = 0;
            this.btnSetDevInfo.FlatAppearance.BorderSize = 0;
            this.btnSetDevInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetDevInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSetDevInfo.Location = new System.Drawing.Point(269, 291);
            this.btnSetDevInfo.Name = "btnSetDevInfo";
            this.btnSetDevInfo.Size = new System.Drawing.Size(139, 45);
            this.btnSetDevInfo.TabIndex = 3;
            this.btnSetDevInfo.Text = "SetDeviceInfo";
            this.btnSetDevInfo.UseVisualStyleBackColor = false;
            this.btnSetDevInfo.Click += new System.EventHandler(this.btnSetDevInfo_Click);
            // 
            // btnGetDevStatus
            // 
            this.btnGetDevStatus.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetDevStatus.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetDevStatus.BorderRadius = 20;
            this.btnGetDevStatus.BorderSize = 0;
            this.btnGetDevStatus.FlatAppearance.BorderSize = 0;
            this.btnGetDevStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDevStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetDevStatus.Location = new System.Drawing.Point(461, 291);
            this.btnGetDevStatus.Name = "btnGetDevStatus";
            this.btnGetDevStatus.Size = new System.Drawing.Size(139, 45);
            this.btnGetDevStatus.TabIndex = 3;
            this.btnGetDevStatus.Text = "GetDeviceStatus";
            this.btnGetDevStatus.UseVisualStyleBackColor = false;
            this.btnGetDevStatus.Click += new System.EventHandler(this.btnGetDevStatus_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 45);
            this.label1.TabIndex = 4;
            this.label1.Text = "StatusParameter:  Info Parameter:";
            // 
            // cmbItemList
            // 
            this.cmbItemList.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cmbItemList.FormattingEnabled = true;
            this.cmbItemList.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbItemList.Location = new System.Drawing.Point(153, 237);
            this.cmbItemList.Name = "cmbItemList";
            this.cmbItemList.Size = new System.Drawing.Size(47, 24);
            this.cmbItemList.TabIndex = 5;
            this.cmbItemList.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(329, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Status Value:";
            // 
            // textStatusInfo
            // 
            this.textStatusInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.textStatusInfo.Location = new System.Drawing.Point(445, 236);
            this.textStatusInfo.Name = "textStatusInfo";
            this.textStatusInfo.Size = new System.Drawing.Size(100, 22);
            this.textStatusInfo.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(12, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(640, 3);
            this.label3.TabIndex = 8;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(437, 92);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(18, 17);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SysInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(664, 360);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textStatusInfo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbItemList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetDevStatus);
            this.Controls.Add(this.btnSetDevInfo);
            this.Controls.Add(this.btnGetDevInfo);
            this.Controls.Add(this.btnDisableDevice);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnPowerOffDev);
            this.Controls.Add(this.btnPowerOnDev);
            this.Controls.Add(this.btnSetDeviceTime);
            this.Controls.Add(this.btnGetDeviceTime);
            this.Controls.Add(this.labelInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "SysInfo";
            this.Text = "SysInfo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SysInfo_FormClosed);
            this.Load += new System.EventHandler(this.SysInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private FP_CLOCK.CustomButton btnGetDeviceTime;
        private FP_CLOCK.CustomButton btnSetDeviceTime;
        private FP_CLOCK.CustomButton btnPowerOnDev;
        private FP_CLOCK.CustomButton btnPowerOffDev;
        private FP_CLOCK.CustomButton btnDisableDevice;
        private FP_CLOCK.CustomButton btnOK;
        private FP_CLOCK.CustomButton btnGetDevInfo;
        private FP_CLOCK.CustomButton btnSetDevInfo;
        private FP_CLOCK.CustomButton btnGetDevStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbItemList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textStatusInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}