using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using DAO = Microsoft.Office.Interop.Access.Dao;
using System.Runtime.InteropServices;
using FP_CLOCK;
using System.IO;
using System.Net.Sockets;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.AxHost;
using System.Data.SqlClient;

namespace FPClient
{
    public partial class EnrollDataManagement : Form
    {
        private int m_nMachineNum;
        private AxFP_CLOCKLib.AxFP_CLOCK pOcxObject;
        string dbfFilePath = @"C:\EnGoPer\Data\Cihazlar.dbf";
        string dbfFilePath2 = @"C:\EnGoPer\Data\EnrollData.dbf";
        SaveDevice saveDevice = new SaveDevice();

        public EnrollDataManagement()
        {
            InitializeComponent();
            InitializeCheckListBox();
        }
        public EnrollDataManagement(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {
            InitializeComponent();
            InitializeCheckListBox();
            InitializeListview();


            this.m_nMachineNum = nMachineNum;
            this.pOcxObject = ptrObject;
            var backupOptions = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Pin", 10),
                new KeyValuePair<string, int>("Kart", 11),

            };
            // Set the DataSource for the ComboBox
            this.cmbBackupNum.DataSource = backupOptions;
            this.cmbBackupNum.DisplayMember = "Key"; // Text to show to the user
            this.cmbBackupNum.ValueMember = "Value"; // Value to use in the application

            var backupOptions2 = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Kullancı", 0),
                new KeyValuePair<string, int>("Admin", 1),
            };
            this.cmbPrivilege.DataSource = backupOptions2;
            this.cmbPrivilege.DisplayMember = "Key";
            this.cmbPrivilege.ValueMember = "Value";

            cmbBackupNum.SelectedIndex = 0;
            cmbEMachineNum.SelectedIndex = 0;
            cmbPrivilege.SelectedIndex = 0;
        }
        public void InitializeCheckListBox()
        {
            AddToCheckListBoxFromDBF();             
        }
        public void AddToCheckListBoxFromDBF()
        {
            try
            {
                // Ensure the directory for the DBF file exists
                string directoryPath = Path.GetDirectoryName(dbfFilePath);
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath); // Create directory if it doesn't exist
                }

                // Create connection string for the DBF file
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    bool tableExists = false;
                    try
                    {
                        string checkTableExistsQuery = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)}";
                        using (OleDbCommand checkCommand = new OleDbCommand(checkTableExistsQuery, connection))
                        {
                            OleDbDataReader reader = checkCommand.ExecuteReader();
                            if (reader.HasRows)
                            {
                                tableExists = true; // The table exists and has rows
                            }
                        }
                    }
                    catch
                    {
                        tableExists = false; // If an exception occurs, the table does not exist
                    }
                    if (tableExists)
                    {
                        string selectQuery1 = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)} WHERE ID = ?";
                        string selectQuery2 = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)} WHERE ID <> ?";

                        using (OleDbCommand command = new OleDbCommand(selectQuery1, connection))
                        {
                            // Populate checkedListBox1 with ID = 1
                            command.Parameters.AddWithValue("?", 1);
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string deviceName = reader["DEVNAME"].ToString();
                                    checkedListBox1.Items.Add(deviceName, CheckState.Checked);
                                    // the user cannot change this item's check state
                                    checkedListBox1.SetItemCheckState(checkedListBox1.Items.Count - 1, CheckState.Indeterminate);
                                }
                            }
                        }

                        using (OleDbCommand command = new OleDbCommand(selectQuery2, connection))
                        {
                            // Populate sendedDeviceList with ID <> 1
                            command.Parameters.AddWithValue("?", 1);
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string deviceName = reader["DEVNAME"].ToString();
                                    if (!string.IsNullOrEmpty(deviceName)) // Ensure deviceName is valid
                                    {
                                        sendedDeviceList.Items.Add(deviceName); // Add to sendedDeviceList
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from .DBF file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void InitializeListview()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("EnrollNumber", 100);
            listView1.Columns.Add("FingerNumber", 100);
            listView1.Columns.Add("Privilige", 100);
            listView1.Columns.Add("EnrollName", 100);
            listView1.Columns.Add("CardNumber", 100);

            //Add items in the listview
            listView1.Items.Clear();
            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
        }
        private void EnrollDataManagement_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Visible = true;
        }
        private bool DisableDevice()
        {
            //labelInfo.Text = "Çalışıyor...";
            bool bRet = pOcxObject.EnableDevice(m_nMachineNum, 0);
            
            if (bRet)
            {
                //labelInfo.Text = "Bağlantı Başarılı!";
                return true;
            }
            else
            {
                labelInfo.Text = "Cihaz Bulunamadı!";
                return false;
            }
        }
        private void ShowErrorInfo()
        {
            int nErrorValue = 0;
            pOcxObject.GetLastError(ref nErrorValue);
            labelInfo.Text = common.FormErrorStr(nErrorValue);
        }
        private void btnClearAllData_Click(object sender, EventArgs e)
        {
           DialogResult dr = MessageBox.Show("Clear all data on the machine?!!",
                "Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
            {
                return;
            }

            DisableDevice();

            bool bRet = pOcxObject.ClearKeeperData(m_nMachineNum);
            if (bRet)
            {
                labelInfo.Text = "ClearKeeperData ...";
            } 
            else
            {
                labelInfo.Text = "Error";
            }

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }
        private void btnRmAllManager_Click(object sender, EventArgs e) // Tüm yöneticileri sil
        {
            bool bRet;

            // Disable the device before removing all managers
            DisableDevice();

            // Call the method to remove all managers
            bRet = pOcxObject.BenumbAllManager(m_nMachineNum);

            if (bRet)
            {
                // If the managers are removed from the device, update the database
                string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
                string directoryPath = Path.GetDirectoryName(enrolldbfPath);
                string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection conn = new OleDbConnection(strConnection))
                {
                    try
                    {
                        conn.Open();
                        // Query to update all privilege values not equal to 0
                        string query = "UPDATE EnrollData SET PRIV = 0 WHERE PRIV <> 0";

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            int rowsAffected = cmd.ExecuteNonQuery(); // Execute the update query
                            labelInfo.Text = $"Tüm yönetici izinleri kaldırıldı. {rowsAffected} kayıt güncellendi.";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log or display the error
                        MessageBox.Show("Veritabanı güncellemesi hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Refresh any related UI or database if necessary
                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
            }
            else
            {
                // Show error information if the operation failed
                ShowErrorInfo();
            }

            // Enable the device after the operation
            EnableDevice();
        }
        private void btnSetCompanyString_Click(object sender, EventArgs e)
        {
            DisableDevice();

            string str;


            if (tbcompanyString.TextLength == 0)
            {
                str = " ";
            }
            else
            {
                str = tbcompanyString.Text;
            }        
            

            bool bRet;
            
            Object ob = new System.Runtime.InteropServices.VariantWrapper(str);

            //SetCompanyName
            bRet = pOcxObject.SetCompanyName(m_nMachineNum,
              1,
              ref ob 
              );

            if (bRet)
            {
                labelInfo.Text = "Cihaz şirket ismi gönderildi."; 
            } 
            else
            {
                ShowErrorInfo();
            }

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }
        private void btnDelCompanyString_Click(object sender, EventArgs e)
        {
            DisableDevice();

            string strName = "";
            Object ob = new System.Runtime.InteropServices.VariantWrapper(strName);

            try
            {
                pOcxObject.SetCompanyName(m_nMachineNum,
                0,   //clean
                ref ob);
            }
            
            catch(Exception ev)
            {
                pOcxObject.EnableDevice(m_nMachineNum, 1);

                common.DebugOut( ev.ToString());
            }
        }
        private void btnGetUserName_Click(object sender, EventArgs e)
        {
            //clear
            tbEnrollName.Text = "";
            labelInfo.Text = "";

            DisableDevice();

            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
            int dwEnrollNum = Convert.ToInt32(tbEnrollNum.Text);

            string strName = "";
            object obj = new System.Runtime.InteropServices.VariantWrapper(strName);
            object ob = new object();
            ob = strName;

            bool bRet = pOcxObject.GetUserName(0,
                m_nMachineNum,
                dwEnrollNum,
                dwEnMachineID,
                ref obj
                );
            if (bRet)
            {
                labelInfo.Text = "Success...";
                tbEnrollName.Text = (string)obj;
            } 
            else
            {
                ShowErrorInfo();
            }

            EnableDevice();
            
        }
        private void EnableDevice()
        {
            pOcxObject.EnableDevice(m_nMachineNum, 1);
        }
        private void btnSetUserName_Click(object sender, EventArgs e)
        {
            DisableDevice();
            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
            int dwEnrollNum = Convert.ToInt32(tbEnrollNum.Text);
            string strName = tbEnrollName.TextLength == 0 ? "" : tbEnrollName.Text;

            object obj = new System.Runtime.InteropServices.VariantWrapper(strName);

            // Kullanıcı adı cihazda ayarlanıyor
            bool bRet = pOcxObject.SetUserName(0, m_nMachineNum, dwEnrollNum, dwEnMachineID, ref obj);
            if (bRet)
            {
                string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
                string directoryPath = Path.GetDirectoryName(enrolldbfPath);
                string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection conn = new OleDbConnection(strConnection))
                {
                    try
                    {
                        conn.Open();

                        // EName alanını belirli bir ENumber'a göre güncelle
                        string updateQuery = "UPDATE EnrollData SET EName = ? WHERE ENumber = ?";
                        using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@EName", strName);
                            cmd.Parameters.AddWithValue("@ENumber", dwEnrollNum);

                            // Eşleşen satırları güncelle ve etkilenen satır sayısını kontrol et
                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                // Eğer etkilenen satır yoksa kayıt bulunamadı
                                MessageBox.Show("Error: The specified EnrollNumber does not exist in the database.",
                                    "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                labelInfo.Text = "EnrollNumber not found in database.";

                            }
                            else
                            {
                                // Başarılı güncelleme mesajı
                                labelInfo.Text = "Success: User name updated in the database.";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Database Error: " + ex.Message);
                    }
                }

                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
            }
            else
            {
                ShowErrorInfo();
            }

            EnableDevice();
        }
        private void btnModifyPrivilege_Click(object sender, EventArgs e)
        {
            try
            {
                DisableDevice();

                // Ensure a valid item is selected in the ComboBoxes
                if (cmbEMachineNum.SelectedIndex == -1 ||
                    cmbBackupNum.SelectedItem == null ||
                    cmbPrivilege.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen geçerli bir kayıt tipi seçiniz.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
                int dwEnrollNum = Convert.ToInt32(tbEnrollNum.Text);

                // Extract values from the ComboBoxes
                int dwBackupNum = ((KeyValuePair<string, int>)cmbBackupNum.SelectedItem).Value;
                int dwPrivilege = ((KeyValuePair<string, int>)cmbPrivilege.SelectedItem).Value;

                bool bRet = pOcxObject.ModifyPrivilege(m_nMachineNum, dwEnrollNum, dwEnMachineID, dwBackupNum, dwPrivilege);

                if (bRet)
                {
                    string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
                    string directoryPath = Path.GetDirectoryName(enrolldbfPath);
                    string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                    using (OleDbConnection conn = new OleDbConnection(strConnection))
                    {
                        try
                        {
                            conn.Open();
                            string updateQuery = "UPDATE EnrollData SET PRIV = ? WHERE ENumber = ? AND FNumber = ?";
                            using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("@PRIV", dwPrivilege);
                                cmd.Parameters.AddWithValue("@ENumber", dwEnrollNum);
                                cmd.Parameters.AddWithValue("@FNumber", dwBackupNum);

                                int rowsAffected = cmd.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    MessageBox.Show("Girilen veriler veritabanında bulunamadı.",
                                        "Kayıt Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    labelInfo.Text = "Veri bulunamadı";
                                }
                                else
                                {
                                    labelInfo.Text = "Başarılı: İzin güncellendi";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Veritabanı Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    // Refresh UI
                    listView1.Items.Clear();
                    saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
                }
                else
                {
                    ShowErrorInfo();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Geçersiz kayıt numarası. Lütfen geçerli bir numara giriniz.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Ensure the device is re-enabled even if an exception occurs
                EnableDevice();
            }
        }
        private void btnGetEnrollInfo_Click(object sender, EventArgs e) // Cihazdan Bilgi Al
        {
            listView1.Items.Clear();

            // Step 1: Disable device before starting to read data
            if (!DisableDevice())
            {
                MessageBox.Show("Cihaz bağlantı hatası", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            listView1.Items.Clear();

            bool bRet;

            // Step 2: Start reading all user IDs from the device
            bRet = pOcxObject.ReadAllUserID(m_nMachineNum);
            if (!bRet)
            {
                ShowErrorInfo();
                EnableDevice();
                return;
            }

            labelInfo.Text = "Kullanıcı verisi okunuyor...";

            // Initialize variables
            int nIndex = 0;
            List<UserInfo> userInfoList = new List<UserInfo>();

            // Step 3: Loop to retrieve and store data
            do
            {
                UserInfo userInfo = new UserInfo(); // Create a new instance for each iteration

                bRet = pOcxObject.GetAllUserID(
                    m_nMachineNum,
                    ref userInfo.dwEnrollNumber,
                    ref userInfo.dwMachineNumber,
                    ref userInfo.dwBackupNumber,
                    ref userInfo.dwUserPrivilege,
                    ref userInfo.dwAttendenceEnable
                );
                //btnGetUserName_Click(sender, e);

                if (bRet)
                {
                    userInfoList.Add(userInfo);
                    nIndex++;
                }

            } while (bRet);

            // Step 4: Populate the ListView with the data
            foreach (UserInfo user in userInfoList)
            {
                ListViewItem item = new ListViewItem(user.dwEnrollNumber.ToString());
                item.SubItems.Add(user.dwBackupNumber.ToString());
                item.SubItems.Add(user.dwUserPrivilege.ToString());
                item.SubItems.Add(user.strName);
                listView1.Items.Add(item);
            }

            // Step 5: Confirm completion
            if (nIndex > 0)
            {
                labelInfo.Text = $"Kullanıcı kaydı başarıyla çekildi : {nIndex} .";
            }
            else
            {
                labelInfo.Text = "Kullanıcı verisi yok!";
            }
            EnableDevice();
        }
        //need check about text box input strings
        private void btnDelEnData_Click(object sender, EventArgs e)//Kullanıcı Veri Sil
        {
            // Disable the device to prevent conflicts
            DisableDevice();

            DialogResult dr;
            dr = MessageBox.Show("Devam edilsin mi?", "Kullanıcı veri sil? ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.No)
            {
                return;
            }

            // Loop through all the items in the ListView to find the selected ones
            foreach (ListViewItem item in listView1.Items)
            {
                // Check if the checkbox is selected for this item
                if (item.Checked)
                {
                    // Extract the necessary data from the ListView item
                    int dwEnrollNum = Int32.Parse(item.SubItems[0].Text); // Assuming the first column is EnrollNum
                    int dwEnMachineID = Int32.Parse("1"); // Assuming the second column is EnMachineID
                    int dwBackupNum = Int32.Parse(item.SubItems[1].Text); // Assuming the third column is BackupNum
                    
                    // Call the method to delete the user from the device
                    bool bRet = pOcxObject.DeleteEnrollData(m_nMachineNum, dwEnrollNum, dwEnMachineID, dwBackupNum);

                    if (bRet)
                    {
                        // If the user is deleted from the device, delete from the database as well
                        string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
                        string directoryPath = Path.GetDirectoryName(enrolldbfPath);
                        string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                        using (OleDbConnection conn = new OleDbConnection(strConnection))
                        {
                            try
                            {
                                conn.Open();
                                string deleteQuery = "DELETE FROM EnrollData WHERE ENumber = ?";

                                using (OleDbCommand cmd = new OleDbCommand(deleteQuery, conn))
                                {
                                    cmd.Parameters.AddWithValue("?", dwEnrollNum);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        labelInfo.Text = "Kullanıcı kayıt silindi.";
                                    }
                                    else
                                    {
                                        labelInfo.Text = "Eşleşen kayıt bulunamadı.";
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                            }
                        }

                        // Optionally, remove the item from the ListView
                        listView1.Items.Remove(item);
                    }
                    else
                    {
                        // If deletion from the device fails, show an error
                        ShowErrorInfo();
                    }
                }
            }

            // Re-enable the device after the operation
            EnableDevice();
        }
        private void btnEmptyEnData_Click(object sender, EventArgs e)//Cihazı Temizle
        {
            DisableDevice();

            DialogResult dr;
            dr = MessageBox.Show("Devam edilsin mi?", "Cihaz verileri sil? ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.No)
            {
                return;
            }
            bool bRet = pOcxObject.EmptyEnrollData(m_nMachineNum);
            if (bRet)
            {
                labelInfo.Text = "Cihaz temizlendi.";
                //TODO : cihaz temizlenince, database de temizlenecek?????
                //btnDelDBData_Click(sender, e);
            } 
            else
            {
                ShowErrorInfo();
            }

            EnableDevice();
        }
        private void btnGetAllEnData_Click(object sender, EventArgs e)    // Database Kaydet
        {
            DisableDevice();
            OleDbConnection conn;
            string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";
            conn = new OleDbConnection(strConnection);
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                MessageBox.Show("Access数据库的连接失败!", "Access数据库的连接");
                return;
            }

            bool bBreakFail = false;
            bool bRet;
            bRet = pOcxObject.ReadAllUserID(m_nMachineNum);
            if (!bRet)
            {
                ShowErrorInfo();
                EnableDevice();

                return;
            }
            
            int dwEnrollNumber = 0;
            int dwEnMachineID = 0;
            int dwBackupNum = 0;
            int dwPrivilegeNum = 0;
            int dwEnable = 0;
            int dwPassWord = 0;
      
            do 
            {

                int[] dwData = new int[1420 / 4];
                object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);

                bRet = pOcxObject.GetAllUserID(
                    m_nMachineNum,
                    ref dwEnrollNumber,
                    ref dwEnMachineID,
                    ref dwBackupNum,
                    ref dwPrivilegeNum,
                    ref dwEnable
                    );

                //read finished
                if (bRet == false)
                {
                    bBreakFail = true;
                    break;
                }

                bRet = pOcxObject.GetEnrollData(
                    m_nMachineNum,
                    dwEnrollNumber,
                    dwEnMachineID,
                    dwBackupNum,
                    ref dwPrivilegeNum,
                    ref obj,
                    ref dwPassWord);
                if (!bRet)
                {
                    ShowErrorInfo();
                    DialogResult dr;
                    dr = MessageBox.Show("Continue?", "GetEnrollData", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        bRet = true;
                    }
                    else
                    {
                        EnableDevice();
                        labelInfo.Text = "fail on GetEnrollData";

                        return; 
                    }

                }

                dwData = (int[])obj;
                
                byte[] _indexData = new byte[1420];
                //分配内存
                IntPtr _ptrIndex = Marshal.AllocHGlobal(_indexData.Length);
                Marshal.Copy(dwData, 0, _ptrIndex, 1420/4);  //be careful
                Marshal.Copy(_ptrIndex, _indexData, 0, 1420);
                Marshal.FreeHGlobal(_ptrIndex);
                
                

                string sql;

                OleDbParameter[] parameters = new OleDbParameter[5];
                parameters[0] = new OleDbParameter("@EMNo", OleDbType.Integer);
                parameters[0].Value =dwEnMachineID;

                parameters[1] = new OleDbParameter("@ENumber", OleDbType.Integer);
                parameters[1].Value = dwEnrollNumber;

                parameters[2] = new OleDbParameter("@FNumber", OleDbType.Integer);
                parameters[2].Value = dwBackupNum;

                parameters[3] = new OleDbParameter("@Priv", OleDbType.Integer);
                parameters[3].Value = dwPrivilegeNum;

                if (dwBackupNum == 10 || dwBackupNum == 11)
                {
                    parameters[4] = new OleDbParameter("@EnPw", OleDbType.Integer);
                    parameters[4].Value = dwPassWord;

                    // Correct query for 5 columns
                    string checkQuery = "SELECT COUNT(*) FROM EnrollData WHERE ENumber = ? AND FNumber = ?";
                    using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ENumber", dwEnrollNumber);
                        checkCmd.Parameters.AddWithValue("@FNumber", dwBackupNum);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count == 0) // Only insert if the combination doesn't exist
                        {
                            // Correct query for 6 columns
                            sql = "INSERT INTO EnrollData (EMNo, ENumber, FNumber, Priv, EnPw) " +
                                "VALUES (@EMNo, @ENumber, @FNumber, @Priv, @EnPw)";

                            OleDbCommand cmd = new OleDbCommand(sql, conn);
                            try
                            {
                                //conn.Open();
                                if (parameters != null) cmd.Parameters.AddRange(parameters);
                                cmd.ExecuteNonQuery();

                            }
                            catch (Exception ec)
                            {
                                throw ec;
                            }

                            //reset
                            dwPassWord = 0;

                            if (!bBreakFail)
                            {
                                labelInfo.Text = "Saved all Enroll Data to database...";
                            }

                        }
                    }
                }
                else
                {
                    parameters[4] = new OleDbParameter("@FpData", OleDbType.VarChar);
                    string str = Convert.ToBase64String(_indexData);
                    parameters[4].Value = str;

                    // Check if the combination of ENumber and FNumber already exists
                    string checkQuery = "SELECT COUNT(*) FROM EnrollData WHERE ENumber = ? AND FNumber = ?";
                    using (OleDbCommand checkCmd = new OleDbCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ENumber", dwEnrollNumber);
                        checkCmd.Parameters.AddWithValue("@FNumber", dwBackupNum);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count == 0) // Only insert if the combination doesn't exist
                        {
                            // Correct query for 6 columns
                            sql = "INSERT INTO EnrollData (EMNo, ENumber, FNumber, Priv, EnPw, FpData) " +
                                  "VALUES (@EMNo, @ENumber, @FNumber, @Priv, 0, @FpData)";

                            OleDbCommand cmd = new OleDbCommand(sql, conn);
                            try
                            {
                                //conn.Open();
                                if (parameters != null) cmd.Parameters.AddRange(parameters);
                                cmd.ExecuteNonQuery();

                            }
                            catch (Exception ec)
                            {
                                throw ec;
                            }

                            //reset
                            dwPassWord = 0;
                            if (!bBreakFail)
                            {
                                labelInfo.Text = "Saved all Enroll Data to database...";
                            }

                        }
                    }
                }
            } while (bRet);

            conn.Close();

            EnableDevice();
        }
        private void btnSetAllEnData_Click(object sender, EventArgs e)   // Database Cihaza Yolla
        {
            ConnectToSelectedDevices2();

            string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

            try
            {
                using (OleDbConnection conn = new OleDbConnection(strConnection))
                {
                    conn.Open();

                    if (conn.State != ConnectionState.Open)
                    {
                        MessageBox.Show("Database connection failed!", "Connection Error");
                        return;
                    }

                    string query = "SELECT * FROM EnrollData";
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            labelInfo.Text = "Database is empty.";
                            return;
                        }

                        bool hasCheckedItems = false; // Seçili item var mı kontrolü için flag
                        while (reader.Read())
                        {
                            foreach (ListViewItem item in listView1.Items)
                            {
                                if (!item.Checked) continue; // Sadece seçili olanları işleme al

                                hasCheckedItems = true; // En az bir seçili item var
                                try
                                {
                                    int dwEMachineNumber = Convert.ToInt32(reader["EMNo"]);
                                    int dwEnrollNumber = Convert.ToInt32(reader["ENumber"]);
                                    int dwFingerNumber = Convert.ToInt32(reader["FNumber"]);
                                    int dwPrivilege = Convert.ToInt32(reader["PRIV"]);
                                    int dwPassword = Convert.ToInt32(reader["EnPw"]);

                                    object obj;
                                    if (dwFingerNumber < 10)
                                    {
                                        string fpDataString = reader["FpData"].ToString();
                                        byte[] fpDataBytes = Convert.FromBase64String(fpDataString);
                                        obj = new System.Runtime.InteropServices.VariantWrapper(fpDataBytes);
                                    }
                                    else
                                    {
                                        obj = new System.Runtime.InteropServices.VariantWrapper(new int[1420 / 4]);
                                    }

                                    // Send data to the device
                                    bool bRet = pOcxObject.SetEnrollData(
                                        m_nMachineNum,
                                        dwEnrollNumber,
                                        dwEMachineNumber,
                                        dwFingerNumber,
                                        dwPrivilege,
                                        ref obj,
                                        dwPassword);

                                    if (!bRet)
                                    {
                                        ShowErrorInfo();
                                        if (MessageBox.Show($"Failed to set data for {dwEnrollNumber}. Continue?", "Error", MessageBoxButtons.YesNo) == DialogResult.No)
                                        {
                                            labelInfo.Text = $"Failed to set enroll data for {dwEnrollNumber}";
                                            return;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Error processing row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        // Eğer hiç seçili öğe yoksa uyarı göster
                        if (!hasCheckedItems)
                        {
                            MessageBox.Show("Please select at least one record to send to the device.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnDelDBData_Click(object sender, EventArgs e)//Database Temizle
        {
            DialogResult dr;
            dr = MessageBox.Show("Devam?", "Veritabanı temizlenecek? ", MessageBoxButtons.YesNo,  MessageBoxIcon.Asterisk);
            if (dr == DialogResult.No)
            {
                return;
            }

            OleDbConnection conn;
            string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";
            conn = new OleDbConnection(strConnection);
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                MessageBox.Show("Access数据库的连接失败!", "Access数据库的连接");
                return;
            }
            else
            {
            }

            OleDbParameter[] parameters = null;

            string sql = "delete * from EnrollData";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            try
            {
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ec)
            {
                throw ec;
            }

        }
        private void btnSetEnrollData_Click(object sender, EventArgs e) //Cihaza Kullanıcı Gönder
        {
            DisableDevice();

            // Extract selected values from ComboBoxes using ValueMember
            int dwBackupNum = ((KeyValuePair<string, int>)cmbBackupNum.SelectedItem).Value;
            int dwPrivilegeNum = ((KeyValuePair<string, int>)cmbPrivilege.SelectedItem).Value;

            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1; // Assuming this ComboBox doesn't need changes
            int dwEnrollNumber = Convert.ToInt32(tbEnrollNum.Text);

            int[] dwData = new int[1420 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);
            int dwPassword = 0;
            int dwCardNum = 0;

            // Parse CardNum if present
            if (tbCardNum.TextLength > 0)
            {
                dwCardNum = int.TryParse(tbCardNum.Text, out int parsedValue) ? parsedValue : 0;
            }

            // Set password if the BackupNum corresponds to specific cases
            if (dwBackupNum == 10 || dwBackupNum == 11) // "Pin" or "Kart"
            {
                if (dwCardNum != 0)
                {
                    dwPassword = dwCardNum;
                }
            }

            bool bRet;

            // Call the external method with the updated parameters
            bRet = pOcxObject.SetEnrollData(
                m_nMachineNum,
                dwEnrollNumber,
                dwEnMachineID,
                dwBackupNum,
                dwPrivilegeNum,
                ref obj,
                dwPassword
            );

            if (bRet)
            {
                // Refresh the data and update UI
                btnGetAllEnData_Click(sender, e);
                btnSetUserName_Click(sender, e);
                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
                labelInfo.Text = "SetEnrollData OK";
            }
            else
            {
                ShowErrorInfo();
            }

            EnableDevice();
        }

        /*private void btnGetEnrollData_Click(object sender, EventArgs e) // Kart Numarası Getir
        {
            listBox1.Items.Clear();

            DisableDevice();

            bool bRet;

            int dwBackupNum = cmbBackupNum.SelectedIndex;
            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
            //int dwPrivilegeNum = cmbPrivilege.SelectedIndex;
            int dwPrivilegeNum = 0;
            //need check
            int dwEnrollNumber = Convert.ToInt32(tbEnrollNum.Text);

            int[] dwData = new int[1420 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);

            
           
            int dwPassword = 0;          

            bRet = pOcxObject.GetEnrollData(
                m_nMachineNum,
                dwEnrollNumber,
                dwEnMachineID,
                dwBackupNum,
                ref dwPrivilegeNum,
                ref obj,
                ref dwPassword
                );

            if (bRet)
            {
                labelInfo.Text = "GetEnrollData OK";
                if (dwBackupNum == 10)
                {
                    listBox1.Items.Add(dwPassword.ToString("password: 0"));
                } 
                else if (dwBackupNum == 11)
                {
                    tbCardNum.Text = dwPassword.ToString();
                }
                else
                {
                    int[] intArrar = (int[])obj;
                    
                    int arrayLength = 355;
                    if (arrayLength > intArrar.Length)
                    {
                        arrayLength = intArrar.Length;
                    }

                    //for (int i = 0; i < intArrar.Length; i++ )
                    for (int i = 0; i < arrayLength; i++)
                    {
                        listBox1.Items.Add(intArrar[i].ToString());
                    }
                }

            } 
            else
            {
                ShowErrorInfo();
            }

            EnableDevice();
        }*/
        private void connectButton_Click(object sender, EventArgs e)
        {
            ConnectToSelectedDevices();
            if (!DisableDevice())
            {
                MessageBox.Show("Failed to disable device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                btnGetEnrollInfo_Click(sender, e);

                btnGetAllEnData_Click(sender, e);
                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
            }
        }
        public void ConnectToSelectedDevices()
        {
            string enrolldbfPath = @"C:\EnGoPer\Data\Cihazlar.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

            using (OleDbConnection con = new OleDbConnection(strConnection))
            {
                try
                {
                    con.Open();

                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        // Assuming each item in the checklist box is "EName"
                        string selectedName = item.ToString();

                        // Fetch device details from the database
                        string query = "SELECT IPAddr, DPort, Pwd FROM Cihazlar WHERE DevName = ?";
                        using (OleDbCommand cmd = new OleDbCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("?", selectedName);
                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string ip = reader.GetString(0);
                                    string port = reader.GetString(1);
                                    string password = reader.GetString(2);

                                    int portInt = Convert.ToInt32(port);
                                    int passwordInt = Convert.ToInt32(password);

                                    // Attempt to connect to the device
                                    if (IPAddress.TryParse(ip, out IPAddress ipAddress))
                                    {
                                        try
                                        {
                                            bool bRet = pOcxObject.SetIPAddress(ref ip, portInt, passwordInt);
                                            bRet = pOcxObject.OpenCommPort(m_nMachineNum);

                                            if (bRet)
                                            {
                                                labelInfo.Text = $"Bağlandı! {selectedName} ({ip}:{port}).";
                                                MessageBox.Show($"Bağlandı! {selectedName} ({ip}:{port}).", "Bağlantı Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            }
                                            else
                                            {
                                                MessageBox.Show($"Cihaza Bağlanılamadı! {selectedName} ({ip}:{port}).", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Cihaza Bağlanmadı! {selectedName} ({ip}:{port}). Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Geçersiz IP formatı {selectedName}: {ip}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Veritabanında eşleşen kayıt yok : {selectedName} .", "Kayıt Bulunamadı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void ConnectToSelectedDevices2()
        {
            string enrolldbfPath = @"C:\EnGoPer\Data\Cihazlar.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

            using (OleDbConnection con = new OleDbConnection(strConnection))
            {
                try
                {
                    con.Open();

                    foreach (var item in sendedDeviceList.CheckedItems)
                    {
                        // Assuming each item in the checklist box is "EName"
                        string selectedName = item.ToString();

                        // Fetch device details from the database
                        string query = "SELECT IPAddr, DPort, Pwd FROM Cihazlar WHERE DevName = ?";
                        using (OleDbCommand cmd = new OleDbCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("?", selectedName);
                            using (OleDbDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string ip = reader.GetString(0);
                                    string port = reader.GetString(1);
                                    string password = reader.GetString(2);

                                    int portInt = Convert.ToInt32(port);
                                    int passwordInt = Convert.ToInt32(password);

                                    // Attempt to connect to the device
                                    if (IPAddress.TryParse(ip, out IPAddress ipAddress))
                                    {
                                        try
                                        {
                                            bool bRet = pOcxObject.SetIPAddress(ref ip, portInt, passwordInt);
                                            bRet = pOcxObject.OpenCommPort(m_nMachineNum);

                                            if (bRet)
                                            {
                                                labelInfo.Text = $"Bağlandı! {selectedName} ({ip}:{port}).";
                                                MessageBox.Show($"Bağlandı! {selectedName} ({ip}:{port}).", "Bağlantı Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            }
                                            else
                                            {
                                                MessageBox.Show($"Cihaza Bağlanılamadı! {selectedName} ({ip}:{port}).", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Bağlantı Yapılmadı {selectedName} ({ip}:{port}). Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Geçersiz IP format {selectedName}: {ip}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Veritabanında eşleşen kayıt yok :{selectedName} ", "Kayıt Bulunamadı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];

                // Update Enroll Number
                tbEnrollNum.Text = item.SubItems[0].Text;

                // Update BackupNum ComboBox
                int backupValue = Convert.ToInt32(item.SubItems[1].Text);
                var backupIndex = ((List<KeyValuePair<string, int>>)cmbBackupNum.DataSource)
                    .FindIndex(x => x.Value == backupValue);
                cmbBackupNum.SelectedIndex = backupIndex >= 0 ? backupIndex : -1;

                // Update Privilege ComboBox
                int privilegeValue = Convert.ToInt32(item.SubItems[2].Text);
                var privilegeIndex = ((List<KeyValuePair<string, int>>)cmbPrivilege.DataSource)
                    .FindIndex(x => x.Value == privilegeValue);
                cmbPrivilege.SelectedIndex = privilegeIndex >= 0 ? privilegeIndex : -1;

                // Update Enroll Name
                tbEnrollName.Text = item.SubItems[3].Text;

                // Update Card Number
                tbCardNum.Text = item.SubItems[4].Text;
            }
        }
        private void selectAllBox_CheckedChanged(object sender, EventArgs e)
        {
            // Loop through all items in the ListView
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                // Set the Checked property based on the CheckBox state
                listView1.Items[i].Checked = selectAllBox.Checked;
            }
        }

    }
}
