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
            this.ipTextBox = new System.Windows.Forms.MaskedTextBox();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.deviceNameTextBox = new System.Windows.Forms.TextBox();
            this.pwTextBox = new System.Windows.Forms.MaskedTextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.portLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.dNameLabel = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.deleteButton = new FP_CLOCK.CustomButton();
            this.editButton = new FP_CLOCK.CustomButton();
            this.addButton = new FP_CLOCK.CustomButton();
            this.saveButton = new FP_CLOCK.CustomButton();
            this.SuspendLayout();
            // 
            // ipTextBox
            // 
            this.ipTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ipTextBox.Location = new System.Drawing.Point(833, 236);
            this.ipTextBox.Mask = "000.000.0.000";
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.PromptChar = ' ';
            this.ipTextBox.Size = new System.Drawing.Size(150, 27);
            this.ipTextBox.TabIndex = 1;
            // 
            // portTextBox
            // 
            this.portTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.portTextBox.Location = new System.Drawing.Point(833, 282);
            this.portTextBox.MaxLength = 4;
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(150, 27);
            this.portTextBox.TabIndex = 2;
            // 
            // deviceNameTextBox
            // 
            this.deviceNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.deviceNameTextBox.Location = new System.Drawing.Point(833, 378);
            this.deviceNameTextBox.Name = "deviceNameTextBox";
            this.deviceNameTextBox.Size = new System.Drawing.Size(150, 27);
            this.deviceNameTextBox.TabIndex = 5;
            // 
            // pwTextBox
            // 
            this.pwTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pwTextBox.Location = new System.Drawing.Point(833, 332);
            this.pwTextBox.Mask = "0000";
            this.pwTextBox.Name = "pwTextBox";
            this.pwTextBox.PromptChar = ' ';
            this.pwTextBox.Size = new System.Drawing.Size(150, 27);
            this.pwTextBox.TabIndex = 5;
            this.pwTextBox.UseSystemPasswordChar = true;
            this.pwTextBox.ValidatingType = typeof(int);
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ipLabel.Location = new System.Drawing.Point(697, 239);
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
            this.portLabel.Location = new System.Drawing.Point(697, 285);
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
            this.passwordLabel.Location = new System.Drawing.Point(697, 335);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(51, 22);
            this.passwordLabel.TabIndex = 7;
            this.passwordLabel.Text = "Şifre";
            // 
            // dNameLabel
            // 
            this.dNameLabel.AutoSize = true;
            this.dNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dNameLabel.Location = new System.Drawing.Point(697, 381);
            this.dNameLabel.Name = "dNameLabel";
            this.dNameLabel.Size = new System.Drawing.Size(100, 22);
            this.dNameLabel.TabIndex = 7;
            this.dNameLabel.Text = "Cihaz İsmi";
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(84, 158);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(544, 367);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // deleteButton
            // 
            this.deleteButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.deleteButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.deleteButton.BorderRadius = 10;
            this.deleteButton.BorderSize = 0;
            this.deleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.deleteButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.deleteButton.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.deleteButton.GradientStartColor = System.Drawing.SystemColors.ActiveCaption;
            this.deleteButton.HoverEndColor = System.Drawing.Color.LightBlue;
            this.deleteButton.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.deleteButton.Location = new System.Drawing.Point(716, 429);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(90, 35);
            this.deleteButton.TabIndex = 11;
            this.deleteButton.Text = "Sil";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // editButton
            // 
            this.editButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.editButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.editButton.BorderRadius = 10;
            this.editButton.BorderSize = 0;
            this.editButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.editButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.editButton.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.editButton.GradientStartColor = System.Drawing.SystemColors.ActiveCaption;
            this.editButton.HoverEndColor = System.Drawing.Color.LightBlue;
            this.editButton.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.editButton.Location = new System.Drawing.Point(807, 429);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(90, 35);
            this.editButton.TabIndex = 10;
            this.editButton.Text = "Düzenle";
            this.editButton.UseVisualStyleBackColor = false;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // addButton
            // 
            this.addButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.addButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.addButton.BorderRadius = 10;
            this.addButton.BorderSize = 0;
            this.addButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.addButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.addButton.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.addButton.GradientStartColor = System.Drawing.SystemColors.ActiveCaption;
            this.addButton.HoverEndColor = System.Drawing.Color.LightBlue;
            this.addButton.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.addButton.Location = new System.Drawing.Point(898, 429);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(90, 35);
            this.addButton.TabIndex = 9;
            this.addButton.Text = "Ekle";
            this.addButton.UseVisualStyleBackColor = false;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.saveButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.saveButton.BorderRadius = 10;
            this.saveButton.BorderSize = 0;
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.saveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.saveButton.GradientEndColor = System.Drawing.SystemColors.ActiveCaption;
            this.saveButton.GradientStartColor = System.Drawing.SystemColors.ActiveCaption;
            this.saveButton.HoverEndColor = System.Drawing.Color.LightBlue;
            this.saveButton.HoverStartColor = System.Drawing.Color.AliceBlue;
            this.saveButton.Location = new System.Drawing.Point(890, 545);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(105, 45);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Tamam";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // SaveDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1061, 718);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.editButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.dNameLabel);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.pwTextBox);
            this.Controls.Add(this.deviceNameTextBox);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.ipTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SaveDevice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cihaz Kayıt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SaveDeviceForm_Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomButton saveButton;
        private System.Windows.Forms.MaskedTextBox ipTextBox;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.TextBox deviceNameTextBox;
        private System.Windows.Forms.MaskedTextBox pwTextBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label dNameLabel;
        public System.Windows.Forms.ListView listView1;
        private CustomButton addButton;
        private CustomButton editButton;
        private CustomButton deleteButton;
    }
}