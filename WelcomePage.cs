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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FP_CLOCK
{
    public partial class WelcomePage : Form
    {
        //自定义消息
        public const int USER = 0x500;
        public const int MYMESSAGE = USER + 1;

        private int m_nCurSelID = 1;
        private bool m_bDeviceOpened = false;

        //private int m_nMachineNum;


        public WelcomePage()
        {
            InitializeComponent();
            DeviceLogListView();

            ReceiveListViewItems(listView1.Items);
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

        private void WelcomePage_Load(object sesnder, EventArgs e)
        {
            //this.axFP_CLOCK.OnGeneralEvent += new AxFP_CLOCKLib._IFP_CLOCKEvents_OnGeneralEventEventHandler(this.axFP_CLOCK_OnGeneralEvent);
        }
        private void WelcomePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // open html
            System.Diagnostics.Process.Start("https://personeltakib.com/enka-teknoloji/");
        }
        private void customButton1_Click(object sender, EventArgs e)
        {
            //DeviceLogListView();

            LogManagement logManagement = new LogManagement(m_nCurSelID, ref axFP_CLOCK);
            logManagement.btnReadAllGLogData_Click(sender, e);




        }
        private void customButton3_Click(object sender, EventArgs e)
        {
            // This button should close the application and stop the process
            this.Close();
            Application.Exit();
        }

        private void customButton2_Click(object sender, EventArgs e)
        {
            Visible = false;
            // open MainForm but ı dont want to see WelcomePage
            this.AddOwnedForm(new MainForm(m_nCurSelID,ref axFP_CLOCK));
            this.OwnedForms[0].Visible = true;

        }

        private void DeviceLogListView()
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;
            listView1.Scrollable = true;
            listView1.MultiSelect = false;
            listView1.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("IP Adresi",125, HorizontalAlignment.Left);
            listView1.Columns.Add("Port", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Şifre", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Durum", 100, HorizontalAlignment.Left);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bRet;
            //int nCount = 0;


            if (m_bDeviceOpened)
            {
                m_bDeviceOpened = false;

                axFP_CLOCK.CloseCommPort();
                return;
            }
            this.axFP_CLOCK.OpenCommPort(m_nCurSelID);
            //int nConnectType = (int) CURDEVICETYPE.DEVICE_NET;


            //I should take the information from the listview and assign them to the variables
            string strIP = listView1.SelectedItems[0].SubItems[2].Text;
            //strIP = ipAddressControl1.IPAddress.ToString();
            //string strDeviceName = listView1.SelectedItems[0].SubItems[4].Text;
            string strDevicePort = listView1.SelectedItems[0].SubItems[3].Text;
            //string strDeviceSituation = listView1.SelectedItems[0].SubItems[5].Text;

            strDevicePort = strDevicePort.Trim();
            int nPort = Convert.ToInt32(strDevicePort);
            int password = Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text);


            bRet = axFP_CLOCK.SetIPAddress(ref strIP, nPort, password);
            if (!bRet)
            {
                MessageBox.Show("Bağlantı Hatası", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bRet = axFP_CLOCK.OpenCommPort(m_nCurSelID);
            if (bRet)
            {
                m_bDeviceOpened = true;
                listView1.SelectedItems[0].SubItems[5].Text = "Açık";
                MessageBox.Show("Cihaz Açıldı", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        public void GetDeviceObject(ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {

            ptrObject = axFP_CLOCK;

        }

        public void ReceiveListViewItems(System.Windows.Forms.ListView.ListViewItemCollection items)
        {
            foreach (ListViewItem item in items)
            {
                // Optionally, add to another ListView or process the items as needed
                listView1.Items.Add((ListViewItem)item.Clone()); // Assuming listView2 is the ListView in WelcomePage
            }
        }
        
    }
}
