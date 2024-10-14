namespace FPClient
{
    partial class LogManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogManagement));
            this.labelInfo = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.labelTotal = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnOK = new FP_CLOCK.CustomButton();
            this.btnReadAllGLogData = new FP_CLOCK.CustomButton();
            this.button9 = new FP_CLOCK.CustomButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelInfo.Location = new System.Drawing.Point(112, 52);
            this.labelInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(872, 43);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(20, 139);
            this.listView1.Margin = new System.Windows.Forms.Padding(4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1089, 391);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(17, 115);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(41, 16);
            this.labelTotal.TabIndex = 2;
            this.labelTotal.Text = "Total:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(979, 115);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 20);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "ReadOnceMark";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
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
            this.btnOK.Location = new System.Drawing.Point(952, 537);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 40);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Tamam";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnReadAllGLogData
            // 
            this.btnReadAllGLogData.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnReadAllGLogData.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReadAllGLogData.BorderRadius = 20;
            this.btnReadAllGLogData.BorderSize = 0;
            this.btnReadAllGLogData.FlatAppearance.BorderSize = 0;
            this.btnReadAllGLogData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReadAllGLogData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReadAllGLogData.Location = new System.Drawing.Point(772, 537);
            this.btnReadAllGLogData.Name = "btnReadAllGLogData";
            this.btnReadAllGLogData.Size = new System.Drawing.Size(150, 40);
            this.btnReadAllGLogData.TabIndex = 3;
            this.btnReadAllGLogData.Text = "Bütün Datayı Çek";
            this.btnReadAllGLogData.UseVisualStyleBackColor = false;
            this.btnReadAllGLogData.Click += new System.EventHandler(this.btnReadAllGLogData_Click);
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.button9.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.button9.BorderRadius = 20;
            this.button9.BorderSize = 0;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button9.Location = new System.Drawing.Point(592, 537);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(150, 60);
            this.button9.TabIndex = 3;
            this.button9.Text = "Diskteki Bütün Datayı Çek";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.UDGLogRead_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1123, 37);
            this.toolStrip1.TabIndex = 6;
            // 
            // helpButton
            // 
            this.helpButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.helpButton.Image = global::FP_CLOCK.Properties.Resources.question_icon;
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(89, 34);
            this.helpButton.Text = "Yardım";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // LogManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1123, 623);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnReadAllGLogData);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.labelInfo);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LogManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kayıt Yönetimi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LogManagement_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.CheckBox checkBox1;
        private FP_CLOCK.CustomButton btnOK;
        private FP_CLOCK.CustomButton btnReadAllGLogData;
        private FP_CLOCK.CustomButton button9;
        private System.Windows.Forms.ToolStripButton helpButton;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}