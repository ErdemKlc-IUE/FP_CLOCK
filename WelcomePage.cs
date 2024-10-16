using FPClient;
using IPAddressControlLib;
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

        private void WelcomePage_FormClosing(object sender, FormClosingEventArgs e)
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
            Visible = false;
            this.AddOwnedForm(new MainForm(m_nCurSelID,ref axFP_CLOCK));
            this.OwnedForms[0].Visible = true;

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

            /*bool bRet;

            if (m_bDeviceOpened)
            {
                btnOpenDev.Text = "Open";
                m_bDeviceOpened = false;

                axFP_CLOCK.CloseCommPort();
                return;
            }

            this.axFP_CLOCK.OpenCommPort(m_nCurSelID);
            int nPort = Convert.ToInt32(textPort.Text);
            int nPassword = Convert.ToInt32(textPassword.Text);
            string strIP = ipAddressControl1.IPAddress.ToString();
            bRet = axFP_CLOCK.SetIPAddress(ref strIP, nPort, nPassword);
            if (!bRet)
            {
                return;
            }

            bRet = axFP_CLOCK.OpenCommPort(m_nCurSelID);
            if (bRet)
            {
                m_bDeviceOpened = true;
                btnOpenDev.Text = "Close";
            }
*/

        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\r\nAdres:\r\nPerpa Ticaret Merkezi A.Blok Kat:8 No:768 Şişli / İSTANBUL\r\n\n" +
               "Telefon:\r(0212) 320 10 60 - 61\r\n\n" +
               "E-Mail:\r\ninfo@enkateknoloji.com",
               "İletişim Bilgileri",
               MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        /* public void GetDeivceObject(ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject, int nMechineNum)
         {
             ptrObject = axFP_CLOCK;
         }*/
    }
}
