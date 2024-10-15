namespace FP_CLOCK
{
    partial class WelcomePage
    {
        // <summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomePage));
            this.listView1 = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.quitButton = new FP_CLOCK.CustomButton();
            this.settingsButton = new FP_CLOCK.CustomButton();
            this.transferButton = new FP_CLOCK.CustomButton();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(215, 167);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(665, 375);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(897, 37);
            this.toolStrip1.TabIndex = 7;
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
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pictureBox1.BackgroundImage = global::FP_CLOCK.Properties.Resources.enka_teknoloji_logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.InitialImage = global::FP_CLOCK.Properties.Resources.enka_teknoloji_logo1;
            this.pictureBox1.Location = new System.Drawing.Point(12, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(868, 90);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // quitButton
            // 
            this.quitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quitButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.quitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.quitButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.quitButton.BorderRadius = 40;
            this.quitButton.BorderSize = 0;
            this.quitButton.FlatAppearance.BorderSize = 0;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.quitButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.quitButton.Image = global::FP_CLOCK.Properties.Resources.logout;
            this.quitButton.Location = new System.Drawing.Point(801, 565);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(79, 67);
            this.quitButton.TabIndex = 2;
            this.quitButton.Text = "\r\n";
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.customButton3_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.settingsButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.settingsButton.BorderRadius = 40;
            this.settingsButton.BorderSize = 0;
            this.settingsButton.FlatAppearance.BorderSize = 0;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.settingsButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.Location = new System.Drawing.Point(31, 313);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(151, 85);
            this.settingsButton.TabIndex = 1;
            this.settingsButton.Text = "\r\nAyarlar";
            this.settingsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.settingsButton.UseVisualStyleBackColor = true;
            this.settingsButton.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // transferButton
            // 
            this.transferButton.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.transferButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.transferButton.BorderRadius = 40;
            this.transferButton.BorderSize = 0;
            this.transferButton.FlatAppearance.BorderSize = 0;
            this.transferButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transferButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.transferButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.transferButton.Image = global::FP_CLOCK.Properties.Resources.Copy_Refresh_48x48;
            this.transferButton.Location = new System.Drawing.Point(31, 184);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(151, 87);
            this.transferButton.TabIndex = 0;
            this.transferButton.Text = "\r\nBilgileri Aktar";
            this.transferButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.transferButton.UseVisualStyleBackColor = true;
            this.transferButton.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // WelcomePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(897, 644);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.transferButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(919, 695);
            this.MinimumSize = new System.Drawing.Size(919, 695);
            this.Name = "WelcomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enka Teknoloji";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public  AxFP_CLOCKLib.AxFP_CLOCK axFP_CLOCK;
        private CustomButton transferButton;
        private CustomButton settingsButton;
        private CustomButton quitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton helpButton;
    }
}