using AxFP_CLOCKLib;

namespace FP_CLOCK
{
    partial class SaveDevice
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //private AxFP_CLOCKLib.AxFP_CLOCK axFP_CLOCK;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveDevice));
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.deviceNameTextBox = new System.Windows.Forms.TextBox();
            this.pwTextBox = new System.Windows.Forms.MaskedTextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.dNameLabel = new System.Windows.Forms.Label();
            this.saveButton = new FP_CLOCK.CustomButton();
            this.SuspendLayout();
            // 
            // ipTextBox
            // 
            this.ipTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ipTextBox.Location = new System.Drawing.Point(354, 72);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(150, 27);
            this.ipTextBox.TabIndex = 1;
            // 
            // portTextBox
            // 
            this.portTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.portTextBox.Location = new System.Drawing.Point(354, 118);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(150, 27);
            this.portTextBox.TabIndex = 2;
            // 
            // deviceNameTextBox
            // 
            this.deviceNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.deviceNameTextBox.Location = new System.Drawing.Point(354, 214);
            this.deviceNameTextBox.Name = "deviceNameTextBox";
            this.deviceNameTextBox.Size = new System.Drawing.Size(150, 27);
            this.deviceNameTextBox.TabIndex = 4;
            // 
            // pwTextBox
            // 
            this.pwTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pwTextBox.Location = new System.Drawing.Point(354, 168);
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.Size = new System.Drawing.Size(150, 27);
            this.pwTextBox.TabIndex = 5;
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ipLabel.Location = new System.Drawing.Point(218, 75);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(28, 22);
            this.ipLabel.TabIndex = 6;
            this.ipLabel.Text = "IP";
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.portLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.portLabel.Location = new System.Drawing.Point(218, 121);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(46, 22);
            this.portLabel.TabIndex = 7;
            this.portLabel.Text = "Port";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.passwordLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.passwordLabel.Location = new System.Drawing.Point(218, 171);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(93, 22);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "Password";
            // 
            // dNameLabel
            // 
            this.dNameLabel.AutoSize = true;
            this.dNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dNameLabel.Location = new System.Drawing.Point(218, 217);
            this.dNameLabel.Name = "dNameLabel";
            this.dNameLabel.Size = new System.Drawing.Size(100, 22);
            this.dNameLabel.TabIndex = 7;
            this.dNameLabel.Text = "Cihaz İsmi";
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.saveButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.saveButton.BorderRadius = 40;
            this.saveButton.BorderSize = 0;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.saveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveButton.Location = new System.Drawing.Point(417, 256);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(87, 38);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Kaydet";
            this.saveButton.UseVisualStyleBackColor = false;
            // 
            // SaveDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(701, 372);
            this.Controls.Add(this.dNameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.pwTextBox);
            this.Controls.Add(this.deviceNameTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.saveButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaveDevice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveDeviceForm_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton saveButton;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox deviceNameTextBox;
        private System.Windows.Forms.MaskedTextBox pwTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label dNameLabel;
    }
}