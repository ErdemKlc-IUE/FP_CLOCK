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

        private ListViewItem previousSelectedItem = null; // Önceki seçili cihazı takip etmek için
        
        SaveDevice saveDevice = new SaveDevice();
        string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";


        public WelcomePage()
        {
            InitializeComponent();
            DeviceLogListView();
            

            //ReceiveListViewItems(listView1.Items);
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
            // Eğer ListView boşsa uyarı ver ve işlemi durdur
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Listede işlem yapılacak cihaz yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool bRet;
            int count =0;

            // ListView'deki her cihazı sırayla işle
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                ListViewItem currentItem = listView1.Items[i];

                // Eğer önceki bir cihaz açık durumda ise, önce onu kapat
                if (m_bDeviceOpened)
                {
                    m_bDeviceOpened = false;
                    axFP_CLOCK.CloseCommPort();  // Mevcut bağlantıyı kapat
                    previousSelectedItem.SubItems[5].Text = "Kapalı"; // Önceki cihazı "Kapalı" olarak işaretle
                }

                // Şu anki cihazın bilgilerini al
                string strIP = currentItem.SubItems[2].Text;  // IP Adresi
                string strDevicePort = currentItem.SubItems[3].Text; // Port
                string strPassword = currentItem.SubItems[4].Text;   // Şifre

                // Port ve Şifreyi integer tipine dönüştür
                strDevicePort = strDevicePort.Trim();
                int nPort;
                int password;

                // Port ve Şifre formatlarının doğru olup olmadığını kontrol et
                if (!int.TryParse(strDevicePort, out nPort) || !int.TryParse(strPassword, out password))
                {
                    MessageBox.Show($"Cihaz {i + 1} için port veya şifre formatı hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentItem.SubItems[5].Text = "Fail.";
                    continue;  // Bu cihazı atla ve sonraki cihaza geç
                }

                // Cihazın IP, Port ve Şifresini ayarla
                bRet = axFP_CLOCK.SetIPAddress(ref strIP, nPort, password);
                if (!bRet)
                {
                    MessageBox.Show($"Cihaz {i + 1} için bağlantı hatası.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    currentItem.SubItems[5].Text = "Fail.";
                    continue;  // Bu cihazı atla ve sonraki cihaza geç
                }

                // Cihaz ile bağlantıyı aç
                bRet = axFP_CLOCK.OpenCommPort(m_nCurSelID);
                if (bRet)
                {
                    m_bDeviceOpened = true;
                    currentItem.SubItems[5].Text = "Açık";


                    // Opsiyonel: Cihazın seri numarasını al ve listeye ekle
                    string strDeviceSerialNumber = getDeviceSerialNumber();
                    currentItem.SubItems[6].Text = strDeviceSerialNumber;

                    LogManagement logManagement = new LogManagement(m_nCurSelID, ref axFP_CLOCK);
                    logManagement.btnReadAllGLogData_Click(sender, e);


                    SysInfo sysInfo = new SysInfo(m_nCurSelID, ref axFP_CLOCK);
                    sysInfo.btnSetDeviceTime_Click(sender, e);
                    count++;

                }
                else
                {
                    MessageBox.Show($"Cihaz {i + 1} bağlanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Yeni cihazı önceki cihaz olarak güncelle
                previousSelectedItem = currentItem;

                // Her cihaz arasında kısa bir bekleme süresi eklemek isterseniz:
                // System.Threading.Thread.Sleep(500); // 500ms bekleme süresi ekleyebilirsiniz.
            }

            // İşlem tamamlandığında, son cihazın bağlantısını kapat
            if (m_bDeviceOpened)
            {
                m_bDeviceOpened = false;
                axFP_CLOCK.CloseCommPort();  // Son bağlantıyı kapat
                //previousSelectedItem.SubItems[5].Text = "Kapalı"; // Son cihazı "Kapalı" yap
            }
            if(count>0)
            {
                MessageBox.Show("Tüm cihazlar işlendi.", "Tamamlandı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



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
            listView1.Columns.Add("Seri Numarası", 100, HorizontalAlignment.Left);

            listView1.Items.Clear();
            dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
            saveDevice.LoadDBFDataToListView(listView1, dbfFilePath);

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
                listView1.Items.Add((ListViewItem)item.Clone());  // Clone kullanımı ile her item benzersiz olur
            }
        }
        private string getDeviceSerialNumber()
        {
            string str = "";
            bool bRet = axFP_CLOCK.GetSerialNumber(m_nCurSelID, ref str);
            if (bRet)
            {
                return str;
            }
            else
            {
                return "No Device...";
            }
        }

    }
}
