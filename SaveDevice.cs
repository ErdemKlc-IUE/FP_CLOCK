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
    public partial class SaveDevice : Form
    {
        private int m_nMachineNum;
        private AxFP_CLOCKLib.AxFP_CLOCK pOcxObject;
        public SaveDevice()
        {
            InitializeComponent();
        }
        public SaveDevice(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {
            InitializeComponent();
            this.pOcxObject = ptrObject;
            this.m_nMachineNum = nMachineNum;


        }
        private void SaveDeviceForm_Closing(object sender, FormClosingEventArgs e)
        {
            Owner.Visible = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // I want to save the device information here

        }
    }
}
