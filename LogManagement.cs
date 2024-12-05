using AxFP_CLOCKLib;
using FP_CLOCK;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPClient
{
    public partial class LogManagement : Form
    {
        private int m_nMachineNum;
        private string id;
        private AxFP_CLOCKLib.AxFP_CLOCK pOcxObject;
        WelcomePage welcomePage = new WelcomePage();

        public LogManagement()
        {
            InitializeComponent();
        }
        public LogManagement(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject , string id)
        {
            InitializeComponent();

            this.m_nMachineNum = nMachineNum;
            this.pOcxObject = ptrObject;
            this.id = id;
            listView1.GridLines = true;

            checkBox1.Checked = true;
        }
        private void LogManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Visible = true;
        }
        /*private void btnReadGLogData_Click(object sender, EventArgs e)
        {
            InitGLogListView();

            bool bRet;
            GeneralLogInfo gLogInfo = new GeneralLogInfo();

            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();

            // if true, only read new log
            pOcxObject.ReadMark = checkBox1.Checked;

            DisableDevice();

            bRet = pOcxObject.ReadGeneralLogData(m_nMachineNum);
            if (!bRet)
            {
                ShowErrorInfo();

                pOcxObject.EnableDevice(m_nMachineNum, 1);
                return;
            }

            do
            {
                bRet = pOcxObject.GetGeneralLogData(m_nMachineNum,
                ref gLogInfo.dwTMachineNumber,
                ref gLogInfo.dwEnrollNumber,
                ref gLogInfo.dwEMachineNumber,
                ref gLogInfo.dwVerifyMode,
                ref gLogInfo.dwYear,
                ref gLogInfo.dwMonth,
                ref gLogInfo.dwDay,
                ref gLogInfo.dwHour,
                ref gLogInfo.dwMinute
                );

                if (bRet)
                {
                    myArray.Add(gLogInfo);
                }

            } while (bRet);

            int i = 1;
            foreach (GeneralLogInfo gInfo in myArray)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                i++;

                lvi.SubItems.Add(gInfo.dwTMachineNumber.ToString());
                lvi.SubItems.Add(gInfo.dwEnrollNumber.ToString("D8"));
                lvi.SubItems.Add(gInfo.dwEMachineNumber.ToString());

                int nInOut = gInfo.dwVerifyMode / 8;
                lvi.SubItems.Add(nInOut.ToString());                             //INOUT

                string str = common.FormString(gInfo.dwVerifyMode, gInfo.dwEnrollNumber);
                lvi.SubItems.Add(str);                                          // Verify Mode

                DateTime dt = new DateTime(gInfo.dwYear,
                    gInfo.dwMonth,
                    gInfo.dwDay,
                    gInfo.dwHour,
                    gInfo.dwMinute,
                    0);

                lvi.SubItems.Add(dt.ToString("yyyy/MM/dd HH:mm"));


                listView1.Items.Add(lvi);

            }

            labelInfo.Text = "success...";

            i -= 1;
            labelTotal.Text = i.ToString("Total Read 0");

            pOcxObject.EnableDevice(m_nMachineNum, 1);
        }*/
        private void InitGLogListView()
        {
            listView1.Clear();
            listView1.Columns.Add("", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("TMchNo", 90, HorizontalAlignment.Left);
            listView1.Columns.Add("EnrollNo", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("EMchNo", 90, HorizontalAlignment.Left);     
            listView1.Columns.Add("InOut", 60, HorizontalAlignment.Left);
            listView1.Columns.Add("VeriMode", 130, HorizontalAlignment.Left);
            listView1.Columns.Add("DateTime", 130, HorizontalAlignment.Left);
            listView1.Columns.Add("Time", 130, HorizontalAlignment.Left);

        }
        public void btnEmptyGLogData_Click(object sender, EventArgs e)
        {
            bool bRet;

            DisableDevice();

            bRet = pOcxObject.EmptyGeneralLogData(m_nMachineNum);
            if (bRet)
            {
                labelInfo.Text = "EmptyGeneralLogData OK";
            }
            else
            {
                ShowErrorInfo();
            }

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }
        public void btnReadAllGLogData_Click(object sender, EventArgs e)
        {
            InitGLogListView();
            
            bool bRet;
            GeneralLogInfo gLogInfo = new GeneralLogInfo();

            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();

            DisableDevice();
            bRet = pOcxObject.ReadAllGLogData(m_nMachineNum);
            if (!bRet)
            {
                ShowErrorInfo();

                pOcxObject.EnableDevice(m_nMachineNum, 1);
                return;
            }

            do
            {
                bRet = pOcxObject.GetAllGLogData(m_nMachineNum,
                ref gLogInfo.dwTMachineNumber,
                ref gLogInfo.dwEnrollNumber,
                ref gLogInfo.dwEMachineNumber,
                ref gLogInfo.dwVerifyMode,
                ref gLogInfo.dwYear,
                ref gLogInfo.dwMonth,
                ref gLogInfo.dwDay,
                ref gLogInfo.dwHour,
                ref gLogInfo.dwMinute
                );

                if (bRet)
                {
                    myArray.Add(gLogInfo);
                }

            } while (bRet);
            

            int i = 1;
            string str;

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Text.StringBuilder sb2 = new System.Text.StringBuilder();
            

            foreach (GeneralLogInfo gInfo in myArray)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = i.ToString();
                i++;

                int x = Convert.ToInt32(id);
                lvi.SubItems.Add(x.ToString("D3"));
                lvi.SubItems.Add(gInfo.dwEnrollNumber.ToString("D8"));
                lvi.SubItems.Add(x.ToString("D2"));

                int nInOut = gInfo.dwVerifyMode / 8;
                lvi.SubItems.Add(nInOut.ToString());                             // INOUT

                str = common.FormString(gInfo.dwVerifyMode, gInfo.dwEnrollNumber);
                lvi.SubItems.Add(str);                                           // Verify Mode

                string date = $"{gInfo.dwYear:D4}/{gInfo.dwMonth:D2}/{gInfo.dwDay:D2}";

                string hourMinute = $"{gInfo.dwHour:D2}:{gInfo.dwMinute:D2}";

                lvi.SubItems.Add(date);// '/' ile ayrılmış format
                lvi.SubItems.Add(hourMinute);

                listView1.Items.Add(lvi);

                ArrayList arrayList = new ArrayList
                {
                    lvi.SubItems[1].Text, // Machine Number
                    lvi.SubItems[2].Text, // Enroll Number
                    lvi.SubItems[3].Text, // EMachine Number
                    lvi.SubItems[6].Text, // Date
                    lvi.SubItems[7].Text, // Time                                         
                };

                string[] array = arrayList.ToArray(typeof(string)) as string[];
                string strArray = string.Join(",", array);
                sb.AppendLine(strArray);
                sb2.AppendLine(strArray+"|");
            }

            string filePath = @"C:\EnGoPer\Data\data.txt";
            string filePath2 = @"C:\EnGoPer\Data\backup.txt";
            string filePath3 = @"C:\EnGoPer\Data\"+welcomePage.getDeviceSerialNumber()+".txt";

            try // Gönder(sb2,seriNO)
            {
                System.IO.File.AppendAllText(filePath, sb.ToString());
                System.IO.File.AppendAllText(filePath2, sb.ToString());

                System.IO.File.AppendAllText(filePath3, sb2.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            i -= 1;
            labelTotal.Text = i.ToString("Total Read 0");
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            Owner.Visible = true;

            this.Close();
        }
        private bool DisableDevice()
        {
            labelInfo.Text = "Working...";
            bool bRet = pOcxObject.EnableDevice(m_nMachineNum, 0);
            if (bRet)
            {
                labelInfo.Text = "Disable Device Success!";
                return true;
            }
            else
            {
                labelInfo.Text = "No Device...";
                return false;
            }
        }
        private void ShowErrorInfo()
        {
            int nErrorValue = 0;
            pOcxObject.GetLastError(ref nErrorValue);
            labelInfo.Text = common.FormErrorStr(nErrorValue);
        }
        private void UDGLogRead_Click(object sender, EventArgs e)
        {
            string strFilePath;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFilePath = openFileDialog1.FileName;

            }
            else
            {
                return;
            }

            bool bRet;
            bRet = pOcxObject.USBReadGeneralLogData(strFilePath);
            if (!bRet)
            {
                ShowErrorInfo();
                return;
            }

            GeneralLogInfo gLogInfo = new GeneralLogInfo();
            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();

            do
            {
                bRet = pOcxObject.GetAllGLogData(m_nMachineNum,
                ref gLogInfo.dwTMachineNumber,
                ref gLogInfo.dwEnrollNumber,
                ref gLogInfo.dwEMachineNumber,
                ref gLogInfo.dwVerifyMode,
                ref gLogInfo.dwYear,
                ref gLogInfo.dwMonth,
                ref gLogInfo.dwDay,
                ref gLogInfo.dwHour,
                ref gLogInfo.dwMinute
                );

                if (bRet)
                {
                    myArray.Add(gLogInfo);
                }

            } while (bRet);

            InitGLogListView();

            int i = 1;
            string str;
            foreach (GeneralLogInfo gInfo in myArray)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.Text = i.ToString();
                i++;

                lvi.SubItems.Add(gInfo.dwTMachineNumber.ToString());
                lvi.SubItems.Add(gInfo.dwEnrollNumber.ToString("D8"));
                lvi.SubItems.Add(gInfo.dwEMachineNumber.ToString());

                int nInOut = gInfo.dwVerifyMode / 8;
                lvi.SubItems.Add(nInOut.ToString());                             //INOUT

                str = common.FormString(gInfo.dwVerifyMode, gInfo.dwEnrollNumber);
                lvi.SubItems.Add(str);                                          // Verify Mode

                DateTime dt = new DateTime(gInfo.dwYear,
                    gInfo.dwMonth,
                    gInfo.dwDay,
                    gInfo.dwHour,
                    gInfo.dwMinute,
                    0);

                lvi.SubItems.Add(dt.ToString("yyyy/MM/dd HH:mm"));

                listView1.Items.Add(lvi);
            }

            i -= 1;
            labelTotal.Text = i.ToString("Total Read 0");

            pOcxObject.EnableDevice(m_nMachineNum, 1);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked == false)
            {
                MessageBox.Show("this infulence GLog & SLog Read function \n pls. check it before use Read button",
                    "warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

            }
        }
        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\r\nAdres:\r\nPerpa Ticaret Merkezi A.Blok Kat:8 No:768 Şişli / İSTANBUL\r\n\n" +
                "Telefon:\r(0212) 320 10 60 - 61\r\n\n" +
                "E-Mail:\r\ninfo@enkateknoloji.com",
                "İletişim Bilgileri",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Gönder(),


    }
}
