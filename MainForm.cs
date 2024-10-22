using FP_CLOCK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.IO;  Stream ?

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
        WelcomePage welcomePage = new WelcomePage();
        
        public MainForm()
        {
            InitializeComponent();
        }
        public MainForm(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {
            InitializeComponent();
            
            this.m_nMachineNum = nMachineNum;
            this.pOcxObject = ptrObject;


            //this.cmbInterface.SelectedIndex = 1;

        }

        //private void cmbInterface_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //this.cmbComPort.Enabled = false;

        //    //this.ipAddressControl1.Enabled = true;
        //    this.textPort.Enabled = true;
        //    this.textPassword.Enabled = true;
        //    /*
        //     * this.P2SPort.Enabled = false;
        //     * this.P2STimeOut.Enabled = false;
        //     */
        //}

        //private void cmbMachineNumber_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ComboBox comboBox = (ComboBox)sender;
        //    m_nCurSelID = comboBox.SelectedIndex + 1;
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
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
            welcomePage.GetDeviceObject(ref pOcxObject);


            this.AddOwnedForm(new LogManagement(m_nCurSelID, ref pOcxObject));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnLockCtrl_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new LockCtrl(m_nCurSelID, ref pOcxObject));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnBellSetting_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new bellTimeSetting(m_nCurSelID, ref pOcxObject));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnSetPassTime_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new SetPassTime(m_nCurSelID, ref pOcxObject));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }


        private void btnSysInfo_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new SysInfo(m_nCurSelID, ref pOcxObject));

            this.OwnedForms[0].Visible = true;


        }

        private void btnDeviceInfo_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new DeviceInfo(m_nCurSelID, ref pOcxObject));

            //int nCount = this.OwnedForms.Count();   //only one
            this.OwnedForms[0].Visible = true;
        }

        private void btnEnrollManagement_Click(object sender, EventArgs e)
        {
            Visible = false;

            this.AddOwnedForm(new EnrollDataManagement(m_nCurSelID, ref pOcxObject));
            this.OwnedForms[0].Visible = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner.Visible = true;
           
        }

        private void btnSaveDevice_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.AddOwnedForm(new SaveDevice(m_nCurSelID, ref pOcxObject));
            this.OwnedForms[0].Visible = true;
        }

        //         public void GetDeivceObject( ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject, int nMechineNum)
        //         {
        //             ptrObject = axFP_CLOCK;    
        //         }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\r\nAdres:\r\nPerpa Ticaret Merkezi A.Blok Kat:8 No:768 Şişli / İSTANBUL\r\n\n" +
               "Telefon:\r(0212) 320 10 60 - 61\r\n\n" +
               "E-Mail:\r\ninfo@enkateknoloji.com",
               "İletişim Bilgileri",
               MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.Owner.Visible = true;
        }

    }
}
