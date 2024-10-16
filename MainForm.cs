using AxFP_CLOCKLib;
using FP_CLOCK;
using FP_CLOCKLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPClient
{
    public partial class MainForm : Form
    {
        //自定义消息
        public const int USER = 0x500;
        public const int MYMESSAGE = USER + 1;

        private int m_nCurSelID = 1;
        private bool m_bDeviceOpened = false;


        private int m_nMachineNum;
        private AxFP_CLOCKLib.AxFP_CLOCK pOcxObject;
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {

            InitializeComponent();
            this.m_nMachineNum = nMachineNum;
            this.pOcxObject = ptrObject;


            this.cmbInterface.SelectedIndex = 1;

            this.ipAddressControl1.Text = "192.168.1.224";
            this.textPort.Text = "5005";
            textPassword.Text = "0";
        }

        private void cmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.cmbComPort.Enabled = false;

            this.ipAddressControl1.Enabled = true;
            this.textPort.Enabled = true;
            this.textPassword.Enabled = true;
            /*
             * this.P2SPort.Enabled = false;
             * this.P2STimeOut.Enabled = false;
             */
        }

        private void cmbMachineNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            m_nCurSelID = comboBox.SelectedIndex + 1;
        }

        /*private void btnOpenDevice_Click(object sender, EventArgs e)
        {
            bool bRet;

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
            if(bRet)
            {
                m_bDeviceOpened = true;
                btnOpenDev.Text = "Close";
            }
        }*/


        private void MainForm_Load(object sender, EventArgs e)
        {
            /*this.axFP_CLOCK.OpenCommPort(m_nCurSelID);
            //int nConnectType = (int)CURDEVICETYPE.DEVICE_NET;
            int nPort = Convert.ToInt32(5005);
            string strIP = "192.168.1.224";
            int nPassword = 0;
            bool bRet = axFP_CLOCK.SetIPAddress(ref strIP, nPort, nPassword);
            if (!bRet)
            {
                return;
            }*/

        }

        /* ///重写窗体的消息处理函数DefWndProc，从中加入自己定义消息　MYMESSAGE　的检测的处理入口
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

        private void btnLogManagement_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new LogManagement(m_nCurSelID, ref axFP_CLOCK));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnLockCtrl_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new LockCtrl(m_nCurSelID, ref axFP_CLOCK));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnBellSetting_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new bellTimeSetting(m_nCurSelID, ref axFP_CLOCK));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnSetPassTime_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new SetPassTime(m_nCurSelID, ref axFP_CLOCK));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }


        private void btnSysInfo_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new SysInfo(m_nCurSelID, ref axFP_CLOCK));

            this.OwnedForms[0].Visible = true;


        }

        private void btnDeviceInfo_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new DeviceInfo(m_nCurSelID, ref axFP_CLOCK));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnEnrollManagement_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new EnrollDataManagement(m_nCurSelID, ref axFP_CLOCK));
            this.OwnedForms[0].Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Visible = true;
           
        }

        private void btnSaveDevice_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.AddOwnedForm(new SaveDevice(m_nCurSelID, ref axFP_CLOCK));
            this.OwnedForms[0].Visible = true;
        }

        //         public void GetDeivceObject( ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject, int nMechineNum)
        //         {
        //             ptrObject = axFP_CLOCK;    
        //         }



    }
}
