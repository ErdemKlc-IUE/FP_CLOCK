using FPClient;
using IPAddressControlLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
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

        private string ıd;
        // I want setter and getter for ıd
        

        //private int m_nMachineNum;

        private ListViewItem previousSelectedItem = null; // Önceki seçili cihazı takip etmek için
        
        SaveDevice saveDevice = new SaveDevice();
        string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
        string filePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\data.txt";





        public WelcomePage()
        {
            InitializeComponent();
            DeviceLogListView();
            RecordListView();
            createEnrollDataDB();



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
        public void createEnrollDataDB()
        {
            string enrolldbfPath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string tableName = "EnrollData"; // Explicitly specify the table name
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    bool tableExists = false;
                    try
                    {
                        string checkTableExistsQuery = $"SELECT * FROM {tableName}";
                        using (OleDbCommand checkCommand = new OleDbCommand(checkTableExistsQuery, conn))
                        {
                            checkCommand.ExecuteNonQuery();
                            tableExists = true; // If no exception occurs, the table exists
                        }
                    }
                    catch
                    {
                        tableExists = false;
                    }

                    if (!tableExists)
                    {
                        string createTableCommandText = $"CREATE TABLE {tableName} " +
                            "(EMNo FLOAT, ENumber FLOAT, FNumber FLOAT, Priv FLOAT, EnPw FLOAT, " +
                            "FpData MEMO, EName CHAR(30), Attend FLOAT)";
                        using (OleDbCommand createTableCommand = new OleDbCommand(createTableCommandText, conn))
                        {
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

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
        private void customButton1_Click(object sender, EventArgs e) // Bilgileri Aktar butonu
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
                ıd = currentItem.SubItems[0].Text;
                

                // Eğer önceki bir cihaz açık durumda ise, önce onu kapat
                if (m_bDeviceOpened)
                {
                    m_bDeviceOpened = false;
                    axFP_CLOCK.CloseCommPort();  // Mevcut bağlantıyı kapat
                    //previousSelectedItem.SubItems[5].Text = "Kapalı"; // Önceki cihazı "Kapalı" olarak işaretle
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
                    MessageBox.Show($"Cihaz {ıd} için port veya şifre formatı hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView1.Items[i].SubItems[5].Text = "Bağlantı Kurulamadı";
                    continue;  // Bu cihazı atla ve sonraki cihaza geç
                }

                // Cihazın IP, Port ve Şifresini ayarla
                bRet = axFP_CLOCK.SetIPAddress(ref strIP, nPort, password);
                if (!bRet)
                {
                    MessageBox.Show($"Cihaz {ıd} için bağlantı hatası.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView1.Items[i].SubItems[5].Text = "Bağlantı Kurulamadı";
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

                    LogManagement logManagement = new LogManagement(m_nCurSelID, ref axFP_CLOCK,ıd);
                    logManagement.btnReadAllGLogData_Click(sender, e);
                    //logManagement.Show();



                    SysInfo sysInfo = new SysInfo(m_nCurSelID, ref axFP_CLOCK);
                    sysInfo.btnSetDeviceTime_Click(sender, e);
                    count++;
                    currentItem.SubItems[5].Text = "Tamamlandı";

                }
                else
                {
                    MessageBox.Show($"Cihaz {ıd} bağlanamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    listView1.Items[i].SubItems[5].Text = "Bağlantı Kurulamadı";
                    //listView1.Items[i].SubItems[5].Font = new Font(listView1.Font, FontStyle.Bold);


                }

                // Yeni cihazı önceki cihaz olarak güncelle
                previousSelectedItem = currentItem;

                // Her cihaz arasında kısa bir bekleme süresi eklemek isterseniz:
                // System.Threading.Thread.Sleep(500); // 500ms bekleme süresi ekleyebilirsiniz.
                listView1.Refresh();
            }
            txtToListview();
            sendDatabase();


            // İşlem tamamlandığında, son cihazın bağlantısını kapat
            if (m_bDeviceOpened)
            {
                m_bDeviceOpened = false;
                axFP_CLOCK.CloseCommPort();  // Son bağlantıyı kapat
                previousSelectedItem.SubItems[5].Text = "Tamamlandı"; // Son cihazı "Kapalı" yap
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
            listView1.Columns.Add("Durum", 125, HorizontalAlignment.Left);
            listView1.Columns.Add("Seri Numarası", 100, HorizontalAlignment.Left);

            listView1.Items.Clear();
            dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
            saveDevice.LoadDBFDataToListView(listView1, dbfFilePath);

        }
        private void RecordListView()
        {
            recordsListview.GridLines = true;
            recordsListview.FullRowSelect = true;
            recordsListview.View = View.Details;
            recordsListview.Scrollable = true;
            recordsListview.MultiSelect = false;
            recordsListview.Columns.Add("ID", 50, HorizontalAlignment.Left);
            recordsListview.Columns.Add("Kart No", 100, HorizontalAlignment.Left);
            recordsListview.Columns.Add("FTUS", 50, HorizontalAlignment.Left);
            recordsListview.Columns.Add("Tarih", 100, HorizontalAlignment.Left);
            recordsListview.Columns.Add("Saat", 100, HorizontalAlignment.Left);
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
        /* public int GetListViewItemId(int index)
         {
             // Check if the ListView has any items
             if (listView1.Items.Count == 0)
             {
                 MessageBox.Show("ListView is empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return -1; // or any other value indicating an invalid state
             }

             // Validate the index
             if (index < 0 || index >= listView1.Items.Count)
             {
                 MessageBox.Show("Invalid index.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return -1; // or any other value indicating an invalid state
             }

             // Get the item at the specified index
             ListViewItem item = listView1.Items[index];
             string idText = item.SubItems[0].Text; // Get the ID from the first subitem
             int id = Convert.ToInt32(idText); // Convert it to an integer
             return id; // Return the ID
         }*/

        public void sendDatabase()
        {
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\ENGOPER\EnGoPer\Data;Extended Properties=dBase IV;";


            // Dosyayı satır satır oku
            List<string> lines = File.ReadAllLines(filePath).ToList();

            // Tarih ve saate göre sıralamak için liste oluştur
            var sortedLines = lines.Select(line =>
            {
                // Satırı virgülle ayırarak verileri al
                string[] parts = line.Split(',');

                // parts[3] = Tarih (yyyy/MM/dd)
                // parts[4] = Saat (HH:mm)
                DateTime dateTime = DateTime.ParseExact($"{parts[3]} {parts[4]}", "yyyy/MM/dd HH:mm", null);

                // DateTime ve orijinal satırı bir tuple olarak döndür
                return new { Line = line, DateTime = dateTime };
            })
            .OrderBy(entry => entry.DateTime) // Tarih ve saate göre sıralama
            .Select(entry => entry.Line) // Sadece satırları geri al
            .ToList();

            // Sıralanmış verileri dosyaya geri yaz
            File.WriteAllLines(filePath, sortedLines);



            Console.WriteLine("Veriler başarıyla sıralandı ve dosyaya yazıldı.");

            sendBackupDatabase();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                try
                {
                    // Veritabanı bağlantısını aç
                    connection.Open();
                    Console.WriteLine("Connection successful!");


                    // CSV dosyasını satır satır oku
                    using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            // Satırdaki verileri ayır (virgül ile ayrılmış)
                            string[] values = line.Split(',');

                            // CSV'den gelen değerlerin başındaki ve sonundaki tırnak işaretlerini temizleyin
                            string id = values[0].Trim('"');

                            // Toplam dakika hesaplamak için saat ve dakikayı ayırın
                            string[] timeParts = values[4].Trim('"').Split(':'); // Saat değerini al

                            // Saat ve dakikayı ayır
                            int hours = int.Parse(timeParts[0]); // Saat
                            int minutes = int.Parse(timeParts[1]); // Dakika
                            //int seconds = int.Parse(timeParts[2]); // Saniye

                            // Toplam dakika hesapla
                            float saatnum = hours * 60 + minutes; // Toplam dakikayı hesapla



                            string kartno = values[1].Trim('"');
                            string ftus = values[2].Trim('"');
                            string tarih = values[3].Trim('"');
                            string saat = values[4].Trim('"');
                            //float kimlik = float.Parse(values[7].Trim('"'));

                            DateTime date = DateTime.Parse(values[3].Trim('"'));

                            // Extract day and multiply by 1440
                            float dayInMinutes = date.Day * 1440;

                            // Concatenate Kartno with (dayInMinutes + saatnum) as a string, then parse to float
                            float kimlik = float.Parse(kartno + (dayInMinutes + saatnum).ToString());

                            // DBF dosyasına veri eklemek için INSERT INTO SQL komutunu oluştur
                            string insertQuery = "INSERT INTO Duzenleyici " +
                                "(ID,TARIHNUM ,SAATNUM, KARTNO, FTUS, TARIH, SAAT, KIMLIK) " +
                                "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";
                            OleDbCommand command = new OleDbCommand(insertQuery, connection);

                            // Parametrelerle veriyi ekle
                            command.Parameters.AddWithValue("@p1", id);
                            command.Parameters.AddWithValue("@p2", date);
                            command.Parameters.AddWithValue("@p3", saatnum);
                            command.Parameters.AddWithValue("@p4", kartno);
                            command.Parameters.AddWithValue("@p5", ftus);
                            command.Parameters.AddWithValue("@p6", tarih);
                            command.Parameters.AddWithValue("@p7", saat);
                            command.Parameters.AddWithValue("@p8", kimlik);


                            // Sorguyu çalıştır
                            command.ExecuteNonQuery();
                        }
                    }
                    File.WriteAllText(filePath, string.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        public void sendBackupDatabase()
        {
            string filePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\data.txt";
            string dbfFilePath2 = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\KayıtYedek.dbf";
            string directoryPath2 = Path.GetDirectoryName(dbfFilePath2);
            string connectionString2 = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath2 + ";Extended Properties=dBase IV;";

            using (OleDbConnection connection2 = new OleDbConnection(connectionString2))
            {
                try
                {
                    connection2.Open(); // Bağlantıyı bir kez açın
                    Console.WriteLine("Connection successful!");

                    // Tablo var mı kontrol et
                    bool tableExists = false;
                    try
                    {
                        string checkTableExistsQuery = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath2)}";
                        using (OleDbCommand checkCommand = new OleDbCommand(checkTableExistsQuery, connection2))
                        {
                            checkCommand.ExecuteNonQuery();
                            tableExists = true; // Eğer hata almazsa, tablo mevcut
                        }
                    }
                    catch
                    {
                        tableExists = false; // Hata alırsa tablo yok
                    }

                    // Eğer tablo yoksa, tabloyu oluştur
                    if (!tableExists)
                    {
                        string createTableCommandText = "CREATE TABLE " + Path.GetFileNameWithoutExtension(dbfFilePath2) +
                             " (ID CHAR(10), TARIHNUM DATETIME, SAATNUM FLOAT, KARTNO CHAR(10), FTUS CHAR(5), TARIH CHAR(10), SAAT CHAR(8))";
                        using (OleDbCommand createTableCommand = new OleDbCommand(createTableCommandText, connection2))
                        {
                            createTableCommand.ExecuteNonQuery();
                        }
                    }

                    // TXT dosyasını satır satır oku ve veritabanına ekle
                    using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] values = line.Split(',');

                            // Verileri al ve işleme hazırla
                            string id = values[0].Trim('"');
                            string[] timeParts = values[4].Trim('"').Split(':');
                            int hours = int.Parse(timeParts[0]);
                            int minutes = int.Parse(timeParts[1]);
                            float saatnum = hours * 60 + minutes;

                            string kartno = values[1].Trim('"');
                            string ftus = values[2].Trim('"');
                            string tarih = values[3].Trim('"');
                            string saat = values[4].Trim('"');
                            DateTime date = DateTime.Parse(values[3].Trim('"'));

                            // Yedek tablosuna veri ekle
                            string insertYedekQuery = "INSERT INTO KayıtYedek (ID, TARIHNUM, SAATNUM, KARTNO, FTUS, TARIH, SAAT) VALUES (?, ?, ?, ?, ?, ?, ?)";
                            using (OleDbCommand yedekCommand = new OleDbCommand(insertYedekQuery, connection2))
                            {
                                yedekCommand.Parameters.AddWithValue("@p1", id);
                                yedekCommand.Parameters.AddWithValue("@p2", date);
                                yedekCommand.Parameters.AddWithValue("@p3", saatnum);
                                yedekCommand.Parameters.AddWithValue("@p4", kartno);
                                yedekCommand.Parameters.AddWithValue("@p5", ftus);
                                yedekCommand.Parameters.AddWithValue("@p6", tarih);
                                yedekCommand.Parameters.AddWithValue("@p7", saat);

                                yedekCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }
        public void txtToListview()
        {
            recordsListview.Items.Clear();
            // ı want to add txt file to listview named record listview
            string[] lines = File.ReadAllLines(filePath);
            int i = 0;
            foreach (string line in lines)
            {
                string[] items = line.Split(',');
                ListViewItem item = new ListViewItem(items[0]);
                item.SubItems.Add(items[1]);
                item.SubItems.Add(items[2]);
                item.SubItems.Add(items[3]);
                item.SubItems.Add(items[4]);
                recordsListview.Items.Add(item);
                label1.Text = i.ToString();
                i++;
            }
            label1.Text = i.ToString("Aktarılan Toplam Kayıt : 0");
        }


    }
}
