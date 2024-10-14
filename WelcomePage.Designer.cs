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
            this.customButton2 = new FP_CLOCK.CustomButton();
            this.customButton1 = new FP_CLOCK.CustomButton();
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
            this.helpButton.Image = global::FP_CLOCK.Properties.Resources.Information_48x48;
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(89, 34);
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
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.customButton2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.customButton2.BorderRadius = 40;
            this.customButton2.BorderSize = 0;
            this.customButton2.FlatAppearance.BorderSize = 0;
            this.customButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.customButton2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.customButton2.Image = ((System.Drawing.Image)(resources.GetObject("customButton2.Image")));
            this.customButton2.Location = new System.Drawing.Point(31, 313);
            this.customButton2.Name = "customButton2";
            this.customButton2.Size = new System.Drawing.Size(151, 85);
            this.customButton2.TabIndex = 1;
            this.customButton2.Text = "\r\nAyarlar";
            this.customButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.customButton2.UseVisualStyleBackColor = true;
            this.customButton2.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.customButton1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.customButton1.BorderRadius = 40;
            this.customButton1.BorderSize = 0;
            this.customButton1.FlatAppearance.BorderSize = 0;
            this.customButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.customButton1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.customButton1.Image = global::FP_CLOCK.Properties.Resources.Copy_Refresh_48x48;
            this.customButton1.Location = new System.Drawing.Point(31, 184);
            this.customButton1.Name = "customButton1";
            this.customButton1.Size = new System.Drawing.Size(151, 87);
            this.customButton1.TabIndex = 0;
            this.customButton1.Text = "\r\nBilgileri Aktar";
            this.customButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.customButton1.UseVisualStyleBackColor = true;
            this.customButton1.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // WelcomePage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(897, 644);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WelcomePage";
            this.Text = "Enka Teknoloji";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public  AxFP_CLOCKLib.AxFP_CLOCK axFP_CLOCK;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton quitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton helpButton;
    }
}