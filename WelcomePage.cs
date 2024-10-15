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
        public WelcomePage(AxFP_CLOCKLib.AxFP_CLOCK axFP_CLOCK)
        {
            InitializeComponent();
            this.axFP_CLOCK = axFP_CLOCK;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Scrollable = true;
            listView1.MultiSelect = false;
        }
        /*private void MainForm_Load(object sender, EventArgs e)
        {
            AddOwnedForm(new SysInfo());

        }*/
        /*


                ///重写窗体的消息处理函数DefWndProc，从中加入自己定义消息　MYMESSAGE　的检测的处理入口
                protected override void DefWndProc(ref Message m)
                {
                    switch (m.Msg)
                    {
                        //接收自定义消息MYMESSAGE，并显示其参数
                        case MYMESSAGE:
                            commonDefine.SENDDATASTRUCT myData = new commonDefine.SENDDATASTRUCT();//这是创建自定义信息的结构
                            Type mytype = myData.GetType();
                            myData = (commonDefine.SENDDATASTRUCT)m.GetLParam(mytype);
                            //textBox1.Text = myData.lpData; //显示收到的自定义信息
                            break;
                        default:
                            base.DefWndProc(ref m);
                            break;
                    }
                }*/

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
        private void customButton1_Click(object sender, EventArgs e)
        {
            DeviceLogListView();

            bool bRet; // return value
            int nCount = 0; // device count
            int nID = 0; // device id
            string strIP = ""; // device ip
            int nPort = 0; // device port
            string strName = ""; // device name
            string strPwd = ""; // device password
            string strState = ""; // device state

        }
        private void customButton3_Click(object sender, EventArgs e)
        {
            // This button should close the application and stop the process
            this.Close();
            Application.Exit();
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            // open MainForm but ı dont want to see WelcomePage
            MainForm mainForm = new MainForm();
            mainForm.Show();
            this.Hide();

        }

        private void DeviceLogListView()
        {
            listView1.Clear();
            listView1.Columns.Add("ID", 50);
            listView1.Columns.Add("IP",50);
            listView1.Columns.Add("Port", 50);
            listView1.Columns.Add("Cihaz İsmi", 100);
            listView1.Columns.Add("Şifre", 50);
            listView1.Columns.Add("Durum", 100);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bRet;
            int nCount = 0;

            if (m_bDeviceOpened)
            {
                m_bDeviceOpened = false;

                axFP_CLOCK.CloseCommPort();
                return;
            }
            this.axFP_CLOCK.OpenCommPort('1');
            int nConnectType = (int) CURDEVICETYPE.DEVICE_NET;

            int nPort = Convert.ToInt32(5005);


        }

        /* public void GetDeivceObject(ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject, int nMechineNum)
         {
             ptrObject = axFP_CLOCK;
         }*/
    }
}
