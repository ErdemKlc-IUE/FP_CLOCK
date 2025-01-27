using AxFP_CLOCKLib;

namespace FP_CLOCK
{
    partial class WelcomePage
    {
        // <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public AxFP_CLOCKLib.AxFP_CLOCK axFP_CLOCK;



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
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.axFP_CLOCK = new AxFP_CLOCKLib.AxFP_CLOCK();
            this.quitButton = new FP_CLOCK.CustomButton2();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.recordsListview = new System.Windows.Forms.ListView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.checkBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.axFP_CLOCK)).BeginInit();
            this.listView1 = new System.Windows.Forms.ListView();
            this.transferButton = new FP_CLOCK.CustomButton2();
            this.settingsButton = new FP_CLOCK.CustomButton2();
            //this.btnSaveText = new FP_CLOCK.CustomButton2();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // axFP_CLOCK
            // 
            this.axFP_CLOCK.Visible = false;
            this.axFP_CLOCK.Enabled = true;
            this.axFP_CLOCK.Location = new System.Drawing.Point(0, 0);
            this.axFP_CLOCK.Name = "axFP_CLOCK";
            this.axFP_CLOCK.TabIndex = 0;
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Lavender;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::FP_CLOCK.Properties.Resources.enka_teknoloji_logo1;
            this.pictureBox1.InitialImage = global::FP_CLOCK.Properties.Resources.enka_teknoloji_logo1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(573, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // quitButton
            // 
            this.quitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.quitButton.BackColor = System.Drawing.Color.Transparent;
            this.quitButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.quitButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.quitButton.BorderRadius = 10;
            this.quitButton.BorderSize = 0;
            this.quitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.quitButton.FlatAppearance.BorderSize = 0;
            this.quitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.quitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.quitButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.quitButton.Image = global::FP_CLOCK.Properties.Resources.logout;
            this.quitButton.Location = new System.Drawing.Point(492, 563);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(81, 57);
            this.quitButton.TabIndex = 2;
            this.quitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.quitButton.UseVisualStyleBackColor = false;
            this.quitButton.Click += new System.EventHandler(this.customButton3_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 224);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(379, 400);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.recordsListview);
            this.tabPage2.Controls.Add(this.checkBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(371, 371);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Aktarılan Kayıtlar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 352);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Aktarılan Toplam Kayıt :";
            // 
            // recordsListview
            // 
            this.recordsListview.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.recordsListview.HideSelection = false;
            this.recordsListview.Location = new System.Drawing.Point(12, 13);
            this.recordsListview.Name = "recordsListview";
            this.recordsListview.Size = new System.Drawing.Size(353, 322);
            this.recordsListview.TabIndex = 7;
            this.recordsListview.UseCompatibleStateImageBehavior = false;
            this.recordsListview.View = System.Windows.Forms.View.Details;
            // 
            // checkBox
            // 
            this.checkBox.Checked = true;
            this.checkBox.AutoSize = true;
            this.checkBox.BackColor = System.Drawing.Color.Transparent;
            this.checkBox.Checked = true;
            this.checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox.Location = new System.Drawing.Point(397, 464);
            this.checkBox.Name = "checkBox";
            this.checkBox.Size = new System.Drawing.Size(162, 36);
            this.checkBox.TabIndex = 16;
            this.checkBox.Text = "Bilgi çekildikten sonra \r\ncihazı temizle";
            this.checkBox.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(13, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 25);
            this.label2.TabIndex = 17;
            this.label2.Text = "Cihazlar";
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 109);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(561, 109);
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // transferButton
            // 
            this.transferButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.transferButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.transferButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.transferButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.transferButton.BorderRadius = 10;
            this.transferButton.BorderSize = 0;
            this.transferButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.transferButton.FlatAppearance.BorderSize = 0;
            this.transferButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.transferButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.transferButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.transferButton.Image = global::FP_CLOCK.Properties.Resources.Copy_Refresh_48x48;
            this.transferButton.Location = new System.Drawing.Point(438, 262);
            this.transferButton.Name = "transferButton";
            this.transferButton.Size = new System.Drawing.Size(109, 95);
            this.transferButton.TabIndex = 18;
            this.transferButton.Text = "BİLGİLERİ AKTAR";
            this.transferButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.transferButton.UseVisualStyleBackColor = false;
            this.transferButton.Click += new System.EventHandler(this.customButton1_Click);
            // 
            // settingsButton
            // 
            this.settingsButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.settingsButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.settingsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.settingsButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.settingsButton.BorderRadius = 10;
            this.settingsButton.BorderSize = 0;
            this.settingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.settingsButton.FlatAppearance.BorderSize = 0;
            this.settingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.settingsButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.settingsButton.Image = global::FP_CLOCK.Properties.Resources.technic6;
            this.settingsButton.Location = new System.Drawing.Point(438, 363);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(109, 95);
            this.settingsButton.TabIndex = 19;
            this.settingsButton.Text = "AYARLAR";
            this.settingsButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.settingsButton.UseVisualStyleBackColor = false;
            this.settingsButton.Click += new System.EventHandler(this.customButton2_Click);
            // 
            // WelcomePage
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(578, 644);
            //this.Controls.Add(this.btnSaveText);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.transferButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.checkBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.axFP_CLOCK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 695);
            this.MinimumSize = new System.Drawing.Size(600, 695);
            this.Name = "WelcomePage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enka Teknoloji";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WelcomePage_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.axFP_CLOCK)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CustomButton transferButton1;
        private CustomButton settingsButton1;
        private CustomButton2 quitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        //private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton helpButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListView recordsListview;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.Label label2;
        private CustomButton2 settingsButton;
        private CustomButton2 transferButton;
        //private CustomButton2 btnSaveText;
    }
}