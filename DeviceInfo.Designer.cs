using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;


namespace FPClient
{
    partial class DeviceInfo
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
            this.labelSN = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnGetBackupNumber = new FP_CLOCK.CustomButton();
            this.btnGetProductCode = new FP_CLOCK.CustomButton();
            this.btnGetSerialNumber = new FP_CLOCK.CustomButton();
            this.chatGPTButton1 = new ChatGPTButton();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInfo.Location = new System.Drawing.Point(12, 10);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(729, 47);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSN
            // 
            this.labelSN.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.labelSN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelSN.Location = new System.Drawing.Point(125, 81);
            this.labelSN.Name = "labelSN";
            this.labelSN.Size = new System.Drawing.Size(615, 47);
            this.labelSN.TabIndex = 0;
            this.labelSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(125, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(615, 47);
            this.label3.TabIndex = 0;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(125, 222);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(615, 47);
            this.label4.TabIndex = 0;
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Serial Number";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 180);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "Backup Number";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.Location = new System.Drawing.Point(17, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "Product Code";
            // 
            // btnGetBackupNumber
            // 
            this.btnGetBackupNumber.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetBackupNumber.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetBackupNumber.BorderRadius = 20;
            this.btnGetBackupNumber.BorderSize = 0;
            this.btnGetBackupNumber.FlatAppearance.BorderSize = 0;
            this.btnGetBackupNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetBackupNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetBackupNumber.Location = new System.Drawing.Point(325, 303);
            this.btnGetBackupNumber.Name = "btnGetBackupNumber";
            this.btnGetBackupNumber.Size = new System.Drawing.Size(150, 40);
            this.btnGetBackupNumber.TabIndex = 3;
            this.btnGetBackupNumber.Text = "Backup Number";
            this.btnGetBackupNumber.UseVisualStyleBackColor = false;
            this.btnGetBackupNumber.Click += new System.EventHandler(this.btnGetBackupNumber_Click);
            // 
            // btnGetProductCode
            // 
            this.btnGetProductCode.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetProductCode.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetProductCode.BorderRadius = 20;
            this.btnGetProductCode.BorderSize = 0;
            this.btnGetProductCode.FlatAppearance.BorderSize = 0;
            this.btnGetProductCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetProductCode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetProductCode.Location = new System.Drawing.Point(520, 303);
            this.btnGetProductCode.Name = "btnGetProductCode";
            this.btnGetProductCode.Size = new System.Drawing.Size(150, 40);
            this.btnGetProductCode.TabIndex = 3;
            this.btnGetProductCode.Text = "Product Code";
            this.btnGetProductCode.UseVisualStyleBackColor = false;
            this.btnGetProductCode.Click += new System.EventHandler(this.btnGetProductCode_Click);
            // 
            // btnGetSerialNumber
            // 
            this.btnGetSerialNumber.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnGetSerialNumber.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGetSerialNumber.BorderRadius = 20;
            this.btnGetSerialNumber.BorderSize = 0;
            this.btnGetSerialNumber.FlatAppearance.BorderSize = 0;
            this.btnGetSerialNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGetSerialNumber.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnGetSerialNumber.Location = new System.Drawing.Point(130, 303);
            this.btnGetSerialNumber.Name = "btnGetSerialNumber";
            this.btnGetSerialNumber.Size = new System.Drawing.Size(150, 40);
            this.btnGetSerialNumber.TabIndex = 3;
            this.btnGetSerialNumber.Text = "Get Serial Number";
            this.btnGetSerialNumber.UseVisualStyleBackColor = false;
            this.btnGetSerialNumber.Click += new System.EventHandler(this.btnGetSerialNum_Click);
            // 
            // chatGPTButton1
            // 
            this.chatGPTButton1.BackColor = System.Drawing.Color.Transparent;
            this.chatGPTButton1.BorderRadius = 10;
            this.chatGPTButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chatGPTButton1.FlatAppearance.BorderSize = 0;
            this.chatGPTButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chatGPTButton1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.chatGPTButton1.ForeColor = System.Drawing.Color.White;
            this.chatGPTButton1.Location = new System.Drawing.Point(275, 136);
            this.chatGPTButton1.Name = "chatGPTButton1";
            this.chatGPTButton1.Size = new System.Drawing.Size(198, 68);
            this.chatGPTButton1.TabIndex = 4;
            this.chatGPTButton1.Text = "chatGPTButton1";
            this.chatGPTButton1.UseVisualStyleBackColor = false;
            // 
            // DeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(758, 355);
            this.Controls.Add(this.chatGPTButton1);
            this.Controls.Add(this.btnGetSerialNumber);
            this.Controls.Add(this.btnGetProductCode);
            this.Controls.Add(this.btnGetBackupNumber);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSN);
            this.Controls.Add(this.labelInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "DeviceInfo";
            this.Text = "DeviceInfo";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DeviceInfo_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelSN;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private FP_CLOCK.CustomButton btnGetBackupNumber;
        private FP_CLOCK.CustomButton btnGetProductCode;
        private FP_CLOCK.CustomButton btnGetSerialNumber;
        private ChatGPTButton chatGPTButton1;
    }
}