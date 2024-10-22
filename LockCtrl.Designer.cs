namespace FPClient
{
    partial class LockCtrl
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
            this.btnGetDoorStatus = new FP_CLOCK.CustomButton();
            this.btnDoorOpen = new FP_CLOCK.CustomButton();
            this.btnUncondOpen = new FP_CLOCK.CustomButton();
            this.btnAutoRecover = new FP_CLOCK.CustomButton();
            this.btnReboot = new FP_CLOCK.CustomButton();
            this.btnUncondClose = new FP_CLOCK.CustomButton();
            this.btnWarnCancel = new FP_CLOCK.CustomButton();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInfo.Location = new System.Drawing.Point(12, 10);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(628, 37);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGetDoorStatus
            // 
            this.btnGetDoorStatus.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetDoorStatus.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetDoorStatus.BorderRadius = 20;
            this.btnGetDoorStatus.BorderSize = 0;
            this.btnGetDoorStatus.FlatAppearance.BorderSize = 0;
            this.btnGetDoorStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetDoorStatus.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetDoorStatus.Location = new System.Drawing.Point(72, 132);
            this.btnGetDoorStatus.Name = "btnGetDoorStatus";
            this.btnGetDoorStatus.Size = new System.Drawing.Size(125, 46);
            this.btnGetDoorStatus.TabIndex = 3;
            this.btnGetDoorStatus.Text = "Kapı Durumu";
            this.btnGetDoorStatus.UseVisualStyleBackColor = false;
            this.btnGetDoorStatus.Click += new System.EventHandler(this.btnGetDoorStatus_Click);
            // 
            // btnDoorOpen
            // 
            this.btnDoorOpen.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnDoorOpen.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnDoorOpen.BorderRadius = 20;
            this.btnDoorOpen.BorderSize = 0;
            this.btnDoorOpen.FlatAppearance.BorderSize = 0;
            this.btnDoorOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDoorOpen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDoorOpen.Location = new System.Drawing.Point(231, 132);
            this.btnDoorOpen.Name = "btnDoorOpen";
            this.btnDoorOpen.Size = new System.Drawing.Size(125, 25);
            this.btnDoorOpen.TabIndex = 3;
            this.btnDoorOpen.Text = "Kapı Aç";
            this.btnDoorOpen.UseVisualStyleBackColor = false;
            this.btnDoorOpen.Click += new System.EventHandler(this.btnDoorOpen_Click);
            // 
            // btnUncondOpen
            // 
            this.btnUncondOpen.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnUncondOpen.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnUncondOpen.BorderRadius = 20;
            this.btnUncondOpen.BorderSize = 0;
            this.btnUncondOpen.FlatAppearance.BorderSize = 0;
            this.btnUncondOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUncondOpen.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUncondOpen.Location = new System.Drawing.Point(396, 132);
            this.btnUncondOpen.Name = "btnUncondOpen";
            this.btnUncondOpen.Size = new System.Drawing.Size(127, 25);
            this.btnUncondOpen.TabIndex = 3;
            this.btnUncondOpen.Text = "Koşulsuz Açık";
            this.btnUncondOpen.UseVisualStyleBackColor = false;
            this.btnUncondOpen.Click += new System.EventHandler(this.btnUncondOpen_Click);
            // 
            // btnAutoRecover
            // 
            this.btnAutoRecover.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnAutoRecover.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAutoRecover.BorderRadius = 20;
            this.btnAutoRecover.BorderSize = 0;
            this.btnAutoRecover.FlatAppearance.BorderSize = 0;
            this.btnAutoRecover.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoRecover.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAutoRecover.Location = new System.Drawing.Point(231, 181);
            this.btnAutoRecover.Name = "btnAutoRecover";
            this.btnAutoRecover.Size = new System.Drawing.Size(125, 25);
            this.btnAutoRecover.TabIndex = 3;
            this.btnAutoRecover.Text = "Otomatik Kurtar";
            this.btnAutoRecover.UseVisualStyleBackColor = false;
            this.btnAutoRecover.Click += new System.EventHandler(this.btnAutoRecover_Click);
            // 
            // btnReboot
            // 
            this.btnReboot.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnReboot.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReboot.BorderRadius = 20;
            this.btnReboot.BorderSize = 0;
            this.btnReboot.FlatAppearance.BorderSize = 0;
            this.btnReboot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReboot.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReboot.Location = new System.Drawing.Point(231, 237);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(125, 25);
            this.btnReboot.TabIndex = 3;
            this.btnReboot.Text = "Yeniden Başlat";
            this.btnReboot.UseVisualStyleBackColor = false;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // btnUncondClose
            // 
            this.btnUncondClose.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnUncondClose.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnUncondClose.BorderRadius = 20;
            this.btnUncondClose.BorderSize = 0;
            this.btnUncondClose.FlatAppearance.BorderSize = 0;
            this.btnUncondClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUncondClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnUncondClose.Location = new System.Drawing.Point(396, 181);
            this.btnUncondClose.Name = "btnUncondClose";
            this.btnUncondClose.Size = new System.Drawing.Size(127, 25);
            this.btnUncondClose.TabIndex = 3;
            this.btnUncondClose.Text = "Koşulsuz Kapalı";
            this.btnUncondClose.UseVisualStyleBackColor = false;
            this.btnUncondClose.Click += new System.EventHandler(this.btnUncondClose_Click);
            // 
            // btnWarnCancel
            // 
            this.btnWarnCancel.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnWarnCancel.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnWarnCancel.BorderRadius = 20;
            this.btnWarnCancel.BorderSize = 0;
            this.btnWarnCancel.FlatAppearance.BorderSize = 0;
            this.btnWarnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarnCancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnWarnCancel.Location = new System.Drawing.Point(396, 237);
            this.btnWarnCancel.Name = "btnWarnCancel";
            this.btnWarnCancel.Size = new System.Drawing.Size(127, 25);
            this.btnWarnCancel.TabIndex = 3;
            this.btnWarnCancel.Text = "Uyarı İptal";
            this.btnWarnCancel.UseVisualStyleBackColor = false;
            this.btnWarnCancel.Click += new System.EventHandler(this.btnWarnCancel_Click);
            // 
            // LockCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(641, 315);
            this.Controls.Add(this.btnWarnCancel);
            this.Controls.Add(this.btnUncondClose);
            this.Controls.Add(this.btnUncondOpen);
            this.Controls.Add(this.btnReboot);
            this.Controls.Add(this.btnAutoRecover);
            this.Controls.Add(this.btnDoorOpen);
            this.Controls.Add(this.btnGetDoorStatus);
            this.Controls.Add(this.labelInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LockCtrl";
            this.Text = "Kilit Kontrol";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LockCtrl_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private FP_CLOCK.CustomButton btnGetDoorStatus;
        private FP_CLOCK.CustomButton btnDoorOpen;
        private FP_CLOCK.CustomButton btnUncondOpen;
        private FP_CLOCK.CustomButton btnAutoRecover;
        private FP_CLOCK.CustomButton btnReboot;
        private FP_CLOCK.CustomButton btnUncondClose;
        private FP_CLOCK.CustomButton btnWarnCancel;
    }
}