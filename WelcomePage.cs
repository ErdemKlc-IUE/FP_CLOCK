using FPClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FP_CLOCK
{
    public partial class WelcomePage : Form
    {
        //自定义消息
        public const int USER = 0x500;
        public const int MYMESSAGE = USER + 1;

        private int m_nCurSelID = 1;
        private bool m_bDeviceOpened = false;

        public WelcomePage()
        {
            InitializeComponent();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //AddOwnedForm(new SysInfo());

        }

        //         ///重写窗体的消息处理函数DefWndProc，从中加入自己定义消息　MYMESSAGE　的检测的处理入口
        //         protected override void DefWndProc(ref Message m)
        //         {
        //             switch (m.Msg)
        //             {
        //                 //接收自定义消息MYMESSAGE，并显示其参数
        //                 case MYMESSAGE:
        //                     commonDefine.SENDDATASTRUCT myData = new commonDefine.SENDDATASTRUCT();//这是创建自定义信息的结构
        //                     Type mytype = myData.GetType();
        //                     myData = (commonDefine.SENDDATASTRUCT)m.GetLParam(mytype);   
        //                     //textBox1.Text = myData.lpData; //显示收到的自定义信息
        //                     break;
        //                 default:
        //                     base.DefWndProc(ref m);
        //                     break;
        //             }
        //         }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_bDeviceOpened)
            {
                axFP_CLOCK.CloseCommPort();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // open html
            System.Diagnostics.Process.Start("https://personeltakib.com/enka-teknoloji/");
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void customButton1_Click(object sender, EventArgs e)
        {

        }
        private void customButton3_Click(object sender, EventArgs e)
        {
            // Quit button
            this.Close();
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            // open MainForm but ı dont want to see WelcomePage
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();

        }

        //         public void GetDeivceObject( ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject, int nMechineNum)
        //         {
        //             ptrObject = axFP_CLOCK;    
        //         }
    }
}
