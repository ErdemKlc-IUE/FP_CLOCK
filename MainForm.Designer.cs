using System.Windows.Forms;

namespace FPClient
{
    partial class MainForm 
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnBellSetting = new FP_CLOCK.CustomButton();
            this.btnEnrollMangement = new FP_CLOCK.CustomButton();
            this.btnSaveDevice = new FP_CLOCK.CustomButton();
            this.btnLogManagement = new FP_CLOCK.CustomButton();
            this.btnSysInfo = new FP_CLOCK.CustomButton();
            this.btnLockCtrl = new FP_CLOCK.CustomButton();
            this.btnSetPassTime = new FP_CLOCK.CustomButton();
            this.btnDeviceInfo = new FP_CLOCK.CustomButton();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(159, 179);
            this.textPassword.Margin = new System.Windows.Forms.Padding(2);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '@';
            this.textPassword.Size = new System.Drawing.Size(103, 22);
            this.textPassword.TabIndex = 9;
            // 
            // textPort
            // 
            this.textPort.Location = new System.Drawing.Point(159, 149);
            this.textPort.Margin = new System.Windows.Forms.Padding(2);
            this.textPort.Name = "textPort";
            this.textPort.Size = new System.Drawing.Size(103, 22);
            this.textPort.TabIndex = 8;
            // 
            // ipAddressControl1
            // 
            this.ipAddressControl1.AllowInternalTab = false;
            this.ipAddressControl1.AutoHeight = true;
            this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl1.Location = new System.Drawing.Point(0, 0);
            this.ipAddressControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ipAddressControl1.MinimumSize = new System.Drawing.Size(99, 22);
            this.ipAddressControl1.Name = "ipAddressControl1";
            this.ipAddressControl1.ReadOnly = false;
            this.ipAddressControl1.Size = new System.Drawing.Size(99, 22);
            this.ipAddressControl1.TabIndex = 0;
            this.ipAddressControl1.Text = "...";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 50);
            this.tabControl1.Location = new System.Drawing.Point(2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1091, 827);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabPage1.Location = new System.Drawing.Point(4, 54);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1083, 769);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "KULLANICI YÖNETİM";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabPage2.Location = new System.Drawing.Point(4, 54);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1083, 769);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CİHAZ KAYIT";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnBellSetting
            // 
            this.btnBellSetting.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnBellSetting.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnBellSetting.BorderRadius = 20;
            this.btnBellSetting.BorderSize = 0;
            this.btnBellSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBellSetting.FlatAppearance.BorderSize = 0;
            this.btnBellSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBellSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBellSetting.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBellSetting.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBellSetting.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnBellSetting.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnBellSetting.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnBellSetting.Location = new System.Drawing.Point(249, 24);
            this.btnBellSetting.Margin = new System.Windows.Forms.Padding(2);
            this.btnBellSetting.Name = "btnBellSetting";
            this.btnBellSetting.Size = new System.Drawing.Size(219, 182);
            this.btnBellSetting.TabIndex = 3;
            this.btnBellSetting.Text = "Alarm Ayarları";
            this.btnBellSetting.UseVisualStyleBackColor = true;
            this.btnBellSetting.Click += new System.EventHandler(this.btnBellSetting_Click);
            // 
            // btnEnrollMangement
            // 
            this.btnEnrollMangement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnrollMangement.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEnrollMangement.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnEnrollMangement.BorderRadius = 20;
            this.btnEnrollMangement.BorderSize = 0;
            this.btnEnrollMangement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnrollMangement.FlatAppearance.BorderSize = 0;
            this.btnEnrollMangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollMangement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnEnrollMangement.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEnrollMangement.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEnrollMangement.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnEnrollMangement.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnEnrollMangement.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnEnrollMangement.Location = new System.Drawing.Point(11, 24);
            this.btnEnrollMangement.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnrollMangement.Name = "btnEnrollMangement";
            this.btnEnrollMangement.Size = new System.Drawing.Size(259, 257);
            this.btnEnrollMangement.TabIndex = 3;
            this.btnEnrollMangement.Text = "Kullanıcı Yönetim";
            this.btnEnrollMangement.UseVisualStyleBackColor = false;
            this.btnEnrollMangement.Click += new System.EventHandler(this.btnEnrollManagement_Click);
            // 
            // btnSaveDevice
            // 
            this.btnSaveDevice.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSaveDevice.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSaveDevice.BorderRadius = 20;
            this.btnSaveDevice.BorderSize = 0;
            this.btnSaveDevice.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveDevice.FlatAppearance.BorderSize = 0;
            this.btnSaveDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSaveDevice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSaveDevice.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSaveDevice.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnSaveDevice.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnSaveDevice.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnSaveDevice.Location = new System.Drawing.Point(488, 24);
            this.btnSaveDevice.Name = "btnSaveDevice";
            this.btnSaveDevice.Size = new System.Drawing.Size(219, 182);
            this.btnSaveDevice.TabIndex = 3;
            this.btnSaveDevice.Text = "Cihaz Kayıt";
            this.btnSaveDevice.UseVisualStyleBackColor = false;
            this.btnSaveDevice.Click += new System.EventHandler(this.btnSaveDevice_Click);
            // 
            // btnLogManagement
            // 
            this.btnLogManagement.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnLogManagement.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLogManagement.BorderRadius = 20;
            this.btnLogManagement.BorderSize = 0;
            this.btnLogManagement.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogManagement.FlatAppearance.BorderSize = 0;
            this.btnLogManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogManagement.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLogManagement.GradientEndColor = System.Drawing.Color.Silver;
            this.btnLogManagement.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnLogManagement.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnLogManagement.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnLogManagement.Location = new System.Drawing.Point(276, 199);
            this.btnLogManagement.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogManagement.Name = "btnLogManagement";
            this.btnLogManagement.Size = new System.Drawing.Size(217, 50);
            this.btnLogManagement.TabIndex = 3;
            this.btnLogManagement.Text = "Log Data Management";
            this.btnLogManagement.UseVisualStyleBackColor = false;
            // 
            // btnSysInfo
            // 
            this.btnSysInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSysInfo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSysInfo.BorderRadius = 20;
            this.btnSysInfo.BorderSize = 0;
            this.btnSysInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSysInfo.FlatAppearance.BorderSize = 0;
            this.btnSysInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSysInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSysInfo.GradientEndColor = System.Drawing.Color.Silver;
            this.btnSysInfo.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnSysInfo.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnSysInfo.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnSysInfo.Location = new System.Drawing.Point(545, 199);
            this.btnSysInfo.Margin = new System.Windows.Forms.Padding(2);
            this.btnSysInfo.Name = "btnSysInfo";
            this.btnSysInfo.Size = new System.Drawing.Size(217, 50);
            this.btnSysInfo.TabIndex = 3;
            this.btnSysInfo.Text = "Sistem Bilgi";
            this.btnSysInfo.UseVisualStyleBackColor = true;
            this.btnSysInfo.Click += new System.EventHandler(this.btnSysInfo_Click);
            // 
            // btnLockCtrl
            // 
            this.btnLockCtrl.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnLockCtrl.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLockCtrl.BorderRadius = 20;
            this.btnLockCtrl.BorderSize = 0;
            this.btnLockCtrl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLockCtrl.FlatAppearance.BorderSize = 0;
            this.btnLockCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockCtrl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLockCtrl.GradientEndColor = System.Drawing.Color.Silver;
            this.btnLockCtrl.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnLockCtrl.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnLockCtrl.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnLockCtrl.Location = new System.Drawing.Point(7, 274);
            this.btnLockCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.btnLockCtrl.Name = "btnLockCtrl";
            this.btnLockCtrl.Size = new System.Drawing.Size(217, 50);
            this.btnLockCtrl.TabIndex = 3;
            this.btnLockCtrl.Text = "Kilit Kontrol";
            this.btnLockCtrl.UseVisualStyleBackColor = true;
            this.btnLockCtrl.Click += new System.EventHandler(this.btnLockCtrl_Click);
            // 
            // btnSetPassTime
            // 
            this.btnSetPassTime.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSetPassTime.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSetPassTime.BorderRadius = 20;
            this.btnSetPassTime.BorderSize = 0;
            this.btnSetPassTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSetPassTime.FlatAppearance.BorderSize = 0;
            this.btnSetPassTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPassTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSetPassTime.GradientEndColor = System.Drawing.Color.Silver;
            this.btnSetPassTime.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnSetPassTime.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnSetPassTime.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnSetPassTime.Location = new System.Drawing.Point(545, 274);
            this.btnSetPassTime.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetPassTime.Name = "btnSetPassTime";
            this.btnSetPassTime.Size = new System.Drawing.Size(217, 50);
            this.btnSetPassTime.TabIndex = 3;
            this.btnSetPassTime.Text = "Parametre Ayarları";
            this.btnSetPassTime.UseVisualStyleBackColor = true;
            this.btnSetPassTime.Click += new System.EventHandler(this.btnSetPassTime_Click);
            // 
            // btnDeviceInfo
            // 
            this.btnDeviceInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnDeviceInfo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDeviceInfo.BorderRadius = 20;
            this.btnDeviceInfo.BorderSize = 0;
            this.btnDeviceInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeviceInfo.FlatAppearance.BorderSize = 0;
            this.btnDeviceInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeviceInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDeviceInfo.GradientEndColor = System.Drawing.Color.Silver;
            this.btnDeviceInfo.GradientStartColor = System.Drawing.Color.WhiteSmoke;
            this.btnDeviceInfo.HoverEndColor = System.Drawing.Color.LightBlue;
            this.btnDeviceInfo.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.btnDeviceInfo.Location = new System.Drawing.Point(7, 349);
            this.btnDeviceInfo.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeviceInfo.Name = "btnDeviceInfo";
            this.btnDeviceInfo.Size = new System.Drawing.Size(217, 50);
            this.btnDeviceInfo.TabIndex = 3;
            this.btnDeviceInfo.Text = "Cihaz Bilgi";
            this.btnDeviceInfo.UseVisualStyleBackColor = true;
            this.btnDeviceInfo.Click += new System.EventHandler(this.btnDeviceInfo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1092, 839);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enka Teknoloji";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textPort;
        private IPAddressControlLib.IPAddressControl ipAddressControl1;
        private FP_CLOCK.CustomButton btnSaveDevice;
        private FP_CLOCK.CustomButton btnEnrollMangement;
        private FP_CLOCK.CustomButton btnLogManagement;
        private FP_CLOCK.CustomButton btnSysInfo;
        private FP_CLOCK.CustomButton btnLockCtrl;
        private FP_CLOCK.CustomButton btnBellSetting;
        private FP_CLOCK.CustomButton btnSetPassTime;
        private FP_CLOCK.CustomButton btnDeviceInfo;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}

