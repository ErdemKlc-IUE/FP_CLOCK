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
            //this.axFP_CLOCK = new AxFP_CLOCKLib.AxFP_CLOCK();
            //this.cmbInterface = new System.Windows.Forms.ComboBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textPort = new System.Windows.Forms.TextBox();
            this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
            this.btnSaveDevice = new FP_CLOCK.CustomButton();
            this.btnEnrollMangement = new FP_CLOCK.CustomButton();
            this.btnLogManagement = new FP_CLOCK.CustomButton();
            this.btnSysInfo = new FP_CLOCK.CustomButton();
            this.btnLockCtrl = new FP_CLOCK.CustomButton();
            this.btnBellSetting = new FP_CLOCK.CustomButton();
            this.btnSetPassTime = new FP_CLOCK.CustomButton();
            this.btnDeviceInfo = new FP_CLOCK.CustomButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.homeButton= new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            //((System.ComponentModel.ISupportInitialize)(this.axFP_CLOCK)).BeginInit();
            //this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // axFP_CLOCK
            // 
            /*this.axFP_CLOCK.Enabled = true;
            this.axFP_CLOCK.Location = new System.Drawing.Point(0, 0);
            this.axFP_CLOCK.Name = "axFP_CLOCK";
            this.axFP_CLOCK.TabIndex = 0;*/
            //// 
            //// cmbInterface
            //// 
            //this.cmbInterface.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            //this.cmbInterface.FormattingEnabled = true;
            //this.cmbInterface.Items.AddRange(new object[] {
            //"COM",
            //"NET",
            //"P2S",
            //"USB"});
            //this.cmbInterface.Location = new System.Drawing.Point(160, 30);
            //this.cmbInterface.Margin = new System.Windows.Forms.Padding(2);
            //this.cmbInterface.Name = "cmbInterface";
            //this.cmbInterface.Size = new System.Drawing.Size(102, 24);
            //this.cmbInterface.TabIndex = 1;
            //this.cmbInterface.SelectedIndexChanged += new System.EventHandler(this.cmbInterface_SelectedIndexChanged);
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
            // btnSaveDevice
            // 
            this.btnSaveDevice.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSaveDevice.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSaveDevice.BorderRadius = 20;
            this.btnSaveDevice.BorderSize = 0;
            this.btnSaveDevice.FlatAppearance.BorderSize = 0;
            this.btnSaveDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveDevice.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSaveDevice.Location = new System.Drawing.Point(276, 349);
            this.btnSaveDevice.Name = "btnSaveDevice";
            this.btnSaveDevice.Size = new System.Drawing.Size(217, 50);
            this.btnSaveDevice.TabIndex = 3;
            this.btnSaveDevice.Text = "Cihaz Kayıt";
            this.btnSaveDevice.UseVisualStyleBackColor = false;
            this.btnSaveDevice.Click += new System.EventHandler(this.btnSaveDevice_Click);
            // 
            // btnEnrollMangement
            // 
            this.btnEnrollMangement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnrollMangement.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnEnrollMangement.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnEnrollMangement.BorderRadius = 20;
            this.btnEnrollMangement.BorderSize = 0;
            this.btnEnrollMangement.FlatAppearance.BorderSize = 0;
            this.btnEnrollMangement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnrollMangement.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEnrollMangement.Location = new System.Drawing.Point(7, 199);
            this.btnEnrollMangement.Margin = new System.Windows.Forms.Padding(2);
            this.btnEnrollMangement.Name = "btnEnrollMangement";
            this.btnEnrollMangement.Size = new System.Drawing.Size(217, 50);
            this.btnEnrollMangement.TabIndex = 3;
            this.btnEnrollMangement.Text = "Kullanıcı Yönetim";
            this.btnEnrollMangement.UseVisualStyleBackColor = true;
            this.btnEnrollMangement.Click += new System.EventHandler(this.btnEnrollManagement_Click);
            // 
            // btnLogManagement
            // 
            this.btnLogManagement.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnLogManagement.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnLogManagement.BorderRadius = 20;
            this.btnLogManagement.BorderSize = 0;
            this.btnLogManagement.FlatAppearance.BorderSize = 0;
            this.btnLogManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogManagement.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLogManagement.Location = new System.Drawing.Point(276, 199);
            this.btnLogManagement.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogManagement.Name = "btnLogManagement";
            this.btnLogManagement.Size = new System.Drawing.Size(217, 50);
            this.btnLogManagement.TabIndex = 3;
            this.btnLogManagement.Text = "Log Data Management";
            this.btnLogManagement.UseVisualStyleBackColor = false;
            this.btnLogManagement.Click += new System.EventHandler(this.btnLogManagement_Click);
            // 
            // btnSysInfo
            // 
            this.btnSysInfo.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSysInfo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSysInfo.BorderRadius = 20;
            this.btnSysInfo.BorderSize = 0;
            this.btnSysInfo.FlatAppearance.BorderSize = 0;
            this.btnSysInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSysInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
            this.btnLockCtrl.FlatAppearance.BorderSize = 0;
            this.btnLockCtrl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLockCtrl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnLockCtrl.Location = new System.Drawing.Point(7, 274);
            this.btnLockCtrl.Margin = new System.Windows.Forms.Padding(2);
            this.btnLockCtrl.Name = "btnLockCtrl";
            this.btnLockCtrl.Size = new System.Drawing.Size(217, 50);
            this.btnLockCtrl.TabIndex = 3;
            this.btnLockCtrl.Text = "Kilit Kontrol";
            this.btnLockCtrl.UseVisualStyleBackColor = true;
            this.btnLockCtrl.Click += new System.EventHandler(this.btnLockCtrl_Click);
            // 
            // btnBellSetting
            // 
            this.btnBellSetting.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnBellSetting.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnBellSetting.BorderRadius = 20;
            this.btnBellSetting.BorderSize = 0;
            this.btnBellSetting.FlatAppearance.BorderSize = 0;
            this.btnBellSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBellSetting.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBellSetting.Location = new System.Drawing.Point(276, 274);
            this.btnBellSetting.Margin = new System.Windows.Forms.Padding(2);
            this.btnBellSetting.Name = "btnBellSetting";
            this.btnBellSetting.Size = new System.Drawing.Size(217, 50);
            this.btnBellSetting.TabIndex = 3;
            this.btnBellSetting.Text = "Alarm Ayarları";
            this.btnBellSetting.UseVisualStyleBackColor = true;
            this.btnBellSetting.Click += new System.EventHandler(this.btnBellSetting_Click);
            // 
            // btnSetPassTime
            // 
            this.btnSetPassTime.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSetPassTime.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSetPassTime.BorderRadius = 20;
            this.btnSetPassTime.BorderSize = 0;
            this.btnSetPassTime.FlatAppearance.BorderSize = 0;
            this.btnSetPassTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetPassTime.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
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
            this.btnDeviceInfo.FlatAppearance.BorderSize = 0;
            this.btnDeviceInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeviceInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDeviceInfo.Location = new System.Drawing.Point(7, 349);
            this.btnDeviceInfo.Margin = new System.Windows.Forms.Padding(2);
            this.btnDeviceInfo.Name = "btnDeviceInfo";
            this.btnDeviceInfo.Size = new System.Drawing.Size(217, 50);
            this.btnDeviceInfo.TabIndex = 3;
            this.btnDeviceInfo.Text = "Cihaz Bilgi";
            this.btnDeviceInfo.UseVisualStyleBackColor = true;
            this.btnDeviceInfo.Click += new System.EventHandler(this.btnDeviceInfo_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.homeButton,
            this.helpButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(778, 37);
            this.toolStrip1.TabIndex = 9;
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.helpButton.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.helpButton.Image = global::FP_CLOCK.Properties.Resources.Information_48x48;
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(96, 34);
            this.helpButton.Text = "Yardım";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            //
            // homeButton
            //
            this.homeButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.homeButton.Image = global::FP_CLOCK.Properties.Resources.Information_48x48;
            this.homeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.homeButton.Name = "helpButton";
            this.homeButton.Size = new System.Drawing.Size(96, 34);
            this.homeButton.Text = "Ana Sayfa";
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.BackgroundImage = global::FP_CLOCK.Properties.Resources.enka_teknoloji_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.InitialImage = global::FP_CLOCK.Properties.Resources.enka_teknoloji_logo1;
            this.pictureBox1.Location = new System.Drawing.Point(7, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(771, 90);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(778, 449);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnDeviceInfo);
            this.Controls.Add(this.btnSetPassTime);
            this.Controls.Add(this.btnBellSetting);
            this.Controls.Add(this.btnEnrollMangement);
            this.Controls.Add(this.btnLockCtrl);
            this.Controls.Add(this.btnSaveDevice);
            this.Controls.Add(this.btnSysInfo);
            this.Controls.Add(this.btnLogManagement);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enka Teknoloji";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            //((System.ComponentModel.ISupportInitialize)(this.axFP_CLOCK)).EndInit();
            //this.toolStrip1.ResumeLayout(false);
            //this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private FP_CLOCK.CustomButton btnOpenDev;

        //private System.Windows.Forms.ComboBox cmbInterface;
        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textPort;
        private IPAddressControlLib.IPAddressControl ipAddressControl1;
        private ToolStripButton homeButton;
        private FP_CLOCK.CustomButton btnSaveDevice;
        private FP_CLOCK.CustomButton btnEnrollMangement;
        private FP_CLOCK.CustomButton btnLogManagement;
        private FP_CLOCK.CustomButton btnSysInfo;
        private FP_CLOCK.CustomButton btnLockCtrl;
        private FP_CLOCK.CustomButton btnBellSetting;
        private FP_CLOCK.CustomButton btnSetPassTime;
        private FP_CLOCK.CustomButton btnDeviceInfo;
        private ToolStrip toolStrip1;
        private ToolStripButton helpButton;
        private PictureBox pictureBox1;
    }
}

