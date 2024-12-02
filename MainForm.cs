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
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
        }
        private void btnLockCtrl_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.AddOwnedForm(new LockCtrl(m_nCurSelID, ref pOcxObject));
            this.OwnedForms[0].Visible = true;
        }
        private void btnBellSetting_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.AddOwnedForm(new bellTimeSetting(m_nCurSelID, ref pOcxObject));
            this.OwnedForms[0].Visible = true;
        }
        private void btnSetPassTime_Click(object sender, EventArgs e)
        {
            Visible = false;
            this.AddOwnedForm(new SetPassTime(m_nCurSelID, ref pOcxObject));
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

    }
}
