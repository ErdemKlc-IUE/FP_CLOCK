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
        string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
        string dbfFilePath2 = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\EnrollData.dbf";
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

            cmbBackupNum.SelectedIndex = 0;
            cmbEMachineNum.SelectedIndex = 0;
            cmbPrivilege.SelectedIndex = 0;

            /* OleDbConnection conn;

             string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\EnrollData.mdb";
             conn = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\EnrollData.mdb");
             conn.Open();
             string sql = "SELECT * FROM tblEnroll";

             OleDbCommand command = new OleDbCommand(sql, conn);

             OleDbDataReader reader = command.ExecuteReader();

             reader.Read();

             reader.Close();
             conn.Close();

             // You can count the records this way if needed
             OleDbCommand countCommand = new OleDbCommand("SELECT COUNT(*) FROM tblEnroll", conn);
             int recordCount = (int)countCommand.ExecuteScalar();

             MessageBox.Show($"Record count: {recordCount}");

             DAO.DBEngine DBE;
             DAO.Database DB;
             string DBPath = ".\\EnrollData.mdb";
             DBE = new DAO.DBEngine();
             DB = DBE.OpenDatabase(DBPath, false, false, "");
             MessageBox.Show(DB.Relations.Count.ToString());
             MessageBox.Show(DB.Recordsets.Count.ToString());
             DAO.TableDef daoTable = DB.TableDefs["tblEnroll"];
             DAO.Field daoField = daoTable.Fields["EnrollNumber"];*/

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
                        string selectQuery = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)}";
                        using (OleDbCommand command = new OleDbCommand(selectQuery, connection))
                        {
                            OleDbDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                string ipAddr = reader["IPADDR"].ToString();
                                string portNo = reader["DPORT"].ToString();
                                string password = reader["PWD"].ToString();
                                string deviceName = reader["DEVNAME"].ToString();
                                checkedListBox1.Items.Add(deviceName , CheckState.Checked);
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

        private void btnRmAllManager_Click(object sender, EventArgs e)
        {
            bool bRet;

            DisableDevice();
            bRet = pOcxObject.BenumbAllManager(m_nMachineNum);
            if (bRet)
            {
                labelInfo.Text = "Success...";
            } 
            else
            {
                ShowErrorInfo();
            }

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }

        //
        // take care of exceptions
        //
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
            
            //
            //
            //SetCompanyName
            bRet = pOcxObject.SetCompanyName(m_nMachineNum,
              1,
              ref ob 
              );

            // or
/*
            bRet = pOcxObject.SetCompanyNameWithString(m_nMachineNum,
                1,
                ref str
                );*/
            if (bRet)
            {
                labelInfo.Text = "Set Company Name OK"; 
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

        //
        //参数要检查
        //
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
                string enrolldbfPath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\EnrollData.dbf";
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
            DisableDevice();
            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
            int dwEnrollNum = Convert.ToInt32(tbEnrollNum.Text);
            int dwBackupNum = cmbBackupNum.SelectedIndex;

            int dwPrivilege = cmbPrivilege.SelectedIndex;
            bool bRet;

            bRet = pOcxObject.ModifyPrivilege(m_nMachineNum,
                dwEnrollNum, 
                dwEnMachineID,
                dwBackupNum, 
                dwPrivilege);
            if (bRet)
            {
                labelInfo.Text = "ModifyPrivilege Success...";
            }
            else
            {
                ShowErrorInfo();
            }

            EnableDevice();
        }

        private void btnGetEnrollInfo_Click(object sender, EventArgs e) // Cihazdan Bilgi Al
        {
            listView1.Items.Clear();

            // Step 1: Disable device before starting to read data
            if (!DisableDevice())
            {
                MessageBox.Show("Failed to disable device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            labelInfo.Text = "Reading user data...";

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
                labelInfo.Text = $"Successfully retrieved {nIndex} users.";
            }
            else
            {
                labelInfo.Text = "No user data retrieved.";
            }
            //readDevice();
            EnableDevice();
        }

        //need check about text box input strings
        private void btnDelEnData_Click(object sender, EventArgs e)//Kullanıcı Veri Sil
        {
            int dwEnrollNum = Int32.Parse(tbEnrollNum.Text);
            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
            int dwBackupNum = cmbBackupNum.SelectedIndex;

            // Cihazdan kullanıcıyı sil
            DisableDevice();

            bool bRet = pOcxObject.DeleteEnrollData(
                m_nMachineNum,
                dwEnrollNum,
                dwEnMachineID,
                dwBackupNum);

            if (bRet)
            {
                // Kullanıcı cihazdan başarıyla silindiyse veritabanından da sil
                string enrolldbfPath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\EnrollData.dbf";
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
                                labelInfo.Text = "DeleteEnrollData OK. Record deleted from database.";
                            }
                            else
                            {
                                labelInfo.Text = "DeleteEnrollData OK. No matching record in database.";
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
        private void readDevice()
        {
           /* listView1.Items.Clear();

            bool bRet;

            // Step 2: Start reading all user IDs from the device
            bRet = pOcxObject.ReadAllUserID(m_nMachineNum);
            if (!bRet)
            {
                ShowErrorInfo();
                EnableDevice();
                return;
            }

            labelInfo.Text = "Reading user data...";

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
                listView1.Items.Add(item);
            }

            // Step 5: Confirm completion
            if (nIndex > 0)
            {
                labelInfo.Text = $"Successfully retrieved {nIndex} users.";
            }
            else
            {
                labelInfo.Text = "No user data retrieved.";
            }*/
        }

        /* private void btnUDiskDownLoad_Click(object sender, EventArgs e)
         {
             string localFilePath = "";
             if (!GetFileFullPath_SaveFile(ref localFilePath))
             {
                 return;
             }           

             pOcxObject.UsbEnrollDataStart();

             bool bRet;

             int dwEMachineNumber;
             int dwEnrollNumber;
             int dwFingerNumber;
             int dwPrivilege;
             int dwPassword;
             int[] dwFPData = new int[1420 / 4];
             object objFPData = 0;
             object objStrName = 0;
             string str;

             OleDbConnection myAccessConn;
             string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\EnrollData.mdb";
             myAccessConn = new OleDbConnection(strConnection);
             myAccessConn.Open();

             if (myAccessConn.State != ConnectionState.Open)
             {                
                 MessageBox.Show("Access数据库的连接失败!", "Access数据库的连接");
                 return;
             }
             else
             {
             }

             string strAccessSelect = "SELECT * FROM tblEnroll";
             OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
             OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
             DataSet myDataSet = new DataSet();

             myDataAdapter.Fill(myDataSet, "Categories");

             DataRowCollection dra = myDataSet.Tables["Categories"].Rows;

             if (dra.Count  == 0)
             {
                 labelInfo.Text = "btnUDiskDownLoad_Click, DataBase is empty.";

                 myAccessConn.Close();
                 return;
             }

             //DataRow dRow = dra[1];

             foreach (DataRow dr in dra)
             {
                 dwEMachineNumber = Int32.Parse(dr["EMachineNumber"].ToString());
                 dwEnrollNumber = Int32.Parse(dr["EnrollNumber"].ToString());
                 dwFingerNumber = Int32.Parse(dr["FingerNumber"].ToString());
                 dwPrivilege = Int32.Parse(dr["Privilige"].ToString());
                 dwPassword = Int32.Parse(dr["enPassword"].ToString());
                 str = dr["EnrollName"].ToString();

                 objStrName = new System.Runtime.InteropServices.VariantWrapper( str );

                 if (dwFingerNumber < 10)
                 {

                     objFPData = new System.Runtime.InteropServices.VariantWrapper(dr["FPData"]);
                 }
                 else
                 {
                     objFPData = new System.Runtime.InteropServices.VariantWrapper(dwFPData);
                 }

                 bRet = pOcxObject.SetUsbEnrollData(                    
                     dwEnrollNumber,                    
                     dwFingerNumber,
                     dwPrivilege,
                     ref objFPData,
                     dwPassword,
                     ref objStrName
                     );
                 if (!bRet)
                 {
                     ShowErrorInfo();

                     myAccessConn.Close();

                     return;

                 }

             }//foreach

             bRet = pOcxObject.EnrollDataSaveTOFile(localFilePath);
             if (!bRet)
             {
                 ShowErrorInfo();
             }

             labelInfo.Text = "SBWriteAllEnrollDatatoFile OK";

             myAccessConn.Close();

         }
 */
        /*private bool GetFileFullPath_SaveFile(ref string localFilePath)
        {
            string fileNameExt, newFileName, FilePath;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = " data files(*.dat)|*.dat|All files(*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = DateTime.Now.ToString("yyyyMMdd") + ".dat";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径
                localFilePath = saveFileDialog1.FileName.ToString();
                //获取文件名，不带路径
                fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                //获取文件路径，不带文件名
                FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                //给文件名前加上时间
                newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;
                return true;
            }
            else
            {
                return false;
            }
        }
        */
        /*private void btnUDiskUpload_Click(object sender, EventArgs e)
        {
            string localFilePath = "";           

            if (!GetFileFullPath_OpenFile(ref localFilePath))
            {
                return;
            }

            labelInfo.Text = "Working ...";

            pOcxObject.UsbEnrollDataStart();

            bool bRet;
            bRet = pOcxObject.EnrollDataReadFromFile(localFilePath);
            if (!bRet)
            {
                ShowErrorInfo();

            }

            labelInfo.Text = "Working on GetUsbEnrollData ...";
            bool bExitFlag = false;

            OleDbConnection conn;
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\EnrollData.mdb";
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

            int dwEnrollNumber = 0;
            int dwBackupNumber = 0;
            int dwMachinePrivilege = 0;
            int dwPassWord = 0;
             int[] dwFPData = new int[1420/4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(dwFPData);
            string strName = "";
            object objName = new System.Runtime.InteropServices.VariantWrapper(strName);

            do 
            {
                int[] dwData = new int[1420 / 4];
                obj = new System.Runtime.InteropServices.VariantWrapper(dwData);

                strName = "";
                objName = new System.Runtime.InteropServices.VariantWrapper(strName);

                bRet = pOcxObject.GetUsbEnrollData(
                ref dwEnrollNumber,
                ref dwBackupNumber,
                ref dwMachinePrivilege,
                ref obj,
                ref dwPassWord,
                ref objName
                );

                if (!bRet)
                {
                    int dwError = 0;
                    pOcxObject.GetLastError(ref dwError);
                    if (dwError == 6)
                    {
                        bExitFlag = true;
                    }

                    break;
                }

                dwData = (int[])obj;

                byte[] _indexData = new byte[1420];
                //分配内存
                IntPtr _ptrIndex = Marshal.AllocHGlobal(_indexData.Length);
                //int[]  转成 byte[]
                Marshal.Copy(dwData, 0, _ptrIndex, 1420 / 4);  //be careful
                Marshal.Copy(_ptrIndex, _indexData, 0, 1420);
                Marshal.FreeHGlobal(_ptrIndex);

                string sql;

                OleDbParameter[] parameters = new OleDbParameter[ 6 ];
                parameters[0] = new OleDbParameter("@EMachineNumber", OleDbType.Integer);
                parameters[0].Value = 1;                

                parameters[1] = new OleDbParameter("@EnrollNumber", OleDbType.Integer);
                parameters[1].Value = dwEnrollNumber;

                parameters[2] = new OleDbParameter("@FingerNumber", OleDbType.Integer);
                parameters[2].Value = dwBackupNumber;

                parameters[3] = new OleDbParameter("@Privilige", OleDbType.Integer);
                parameters[3].Value = dwMachinePrivilege;

                parameters[4] = new OleDbParameter("@EnrollName", OleDbType.BSTR);
                parameters[4].Value = objName;

                if (dwBackupNumber == 10 ||
                     dwBackupNumber == 11)
                {
                    parameters[5] = new OleDbParameter("@enPassword", OleDbType.Integer);
                    parameters[5].Value = dwPassWord;

                    sql = "insert into tblEnroll(EMachineNumber,EnrollNumber,FingerNumber,Privilige,EnrollName,enPassword)" +
                    "values(@EMachineNumber,@EnrollNumber,@FingerNumber,@Privilige,?,@enPassword)";  //values(?,?,?,?,?)

                }
                else
                {
                    parameters[5] = new OleDbParameter("@FPData", OleDbType.Binary);
                    parameters[5].Value = _indexData;   //accept byte[]

                    sql = "insert into tblEnroll(EMachineNumber,EnrollNumber,FingerNumber,Privilige,EnrollName,FPData)" +
                        "values(@EMachineNumber,@EnrollNumber,@FingerNumber,@Privilige,?,@FPData)";

                }

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                try
                {
                    conn.Open();
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ec)
                {
                    throw ec;
                }

               
            } while (bRet);

           if (bExitFlag == true)
           {
               labelInfo.Text = "UdiskUpload OK";
           }
           else
           {
               ShowErrorInfo();
           }

           conn.Close(); 
            
        }
*/
        /* private bool GetFileFullPath_OpenFile(ref string strFilePath)
        {
            //string strFilePath;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = System.Environment.CurrentDirectory;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                strFilePath = openFileDialog1.FileName;
                return true;
            }
            else
            {
                return false;
            }
        }*/
        private void btnEmptyEnData_Click(object sender, EventArgs e)
        {
            DisableDevice();

            bool bRet = pOcxObject.EmptyEnrollData(m_nMachineNum);
            if (bRet)
            {
                labelInfo.Text = "EmptyEnrollData Success...";
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
            string enrolldbfPath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";
            conn = new OleDbConnection(strConnection);
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                //MessageBox.Show("Access数据库的连接成功!", "Access数据库的连接");
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

//             int[] dwData = new int[1420 / 4];
//             object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);

           
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
                    //EnableDevice();
                    bBreakFail = true;
                    //labelInfo.Text = "fail on GetAllUserID";
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
                //int[]  转成 byte[]
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
                        //else
                        //    MessageBox.Show("Bu kayıt veritabanında mevcut...");
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
                        //else
                        //    MessageBox.Show("Bu kayıt veritabanında mevcut...");
                    }
                }
            } while (bRet);
            
            //TODO
            //btnSetUserName_Click(sender, e);

            conn.Close();

            EnableDevice();
        } 

        private void btnSetAllEnData_Click(object sender, EventArgs e)   // Database Cihaza Yolla

        {
            bool bRet;

            int dwEMachineNumber;
            int dwEnrollNumber;
            int dwFingerNumber;
            int dwPrivilege;
            int dwPassword;
            int[] dwFPData = new int[1420 / 4];
            object obj = 0;

            OleDbConnection myAccessConn;
            string enrolldbfPath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";
            myAccessConn = new OleDbConnection(strConnection);
            myAccessConn.Open();

            if (myAccessConn.State != ConnectionState.Open)
            {
                //MessageBox.Show("Access数据库的连接成功!", "Access数据库的连接");
                MessageBox.Show("Access数据库的连接失败!", "Access数据库的连接");
                return;
            }
            else
            {
            }

            string strAccessSelect = "SELECT * FROM EnrollData";
            OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect, myAccessConn);
            OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);
            DataSet myDataSet = new DataSet();

            myDataAdapter.Fill(myDataSet, "EnrollData");

            DataRowCollection dra = myDataSet.Tables["EnrollData"].Rows;

            if (dra.Count == 0)
            {
                labelInfo.Text = "btnUDiskDownLoad_Click, DataBase is empty.";

                myAccessConn.Close();
                return;
            }
            //DataRow dRow = dra[1];

            foreach (DataRow dr in dra)
            {
                dwEMachineNumber = Int32.Parse(dr["EMNo"].ToString());
                dwEnrollNumber = Int32.Parse(dr["ENumber"].ToString());
                dwFingerNumber = Int32.Parse(dr["FNumber"].ToString());
                dwPrivilege = Int32.Parse(dr["Priv"].ToString());
                dwPassword = Int32.Parse(dr["EnPw"].ToString());

                if (dwFingerNumber < 10)
                {
                    
                    obj = new System.Runtime.InteropServices.VariantWrapper(dr["FPData"]);
                }
                else
                {
                    obj = new System.Runtime.InteropServices.VariantWrapper(dwFPData);
                }

                bRet = pOcxObject.SetEnrollData(
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
                    DialogResult dlgr;
                    dlgr = MessageBox.Show("Continue?", "SetEnrollData", MessageBoxButtons.YesNo);
                    if (dlgr == DialogResult.Yes)
                    {
                        bRet = true;
                    }
                    else
                    {
                        EnableDevice();
                        labelInfo.Text = "fail on SetEnrollData";

                        myAccessConn.Close();
                        return;
                    }
                }

                
               
            }

            myAccessConn.Close();

            labelInfo.Text = "SetEnrollData Success...";

        }
       /* private void btnSendAllEnrollData_Click(object sender, EventArgs e)
        {
            //no implementation
        }*/

        private void btnDelDBData_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Continue?", "Delete All data in database? ", MessageBoxButtons.YesNo,  MessageBoxIcon.Asterisk);
            if (dr == DialogResult.No)
            {
                return;
            }

            OleDbConnection conn;
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\EnrollData.mdb";
            conn = new OleDbConnection(strConnection);
            conn.Open();

            if (conn.State != ConnectionState.Open)
            {
                //MessageBox.Show("Access数据库的连接成功!", "Access数据库的连接");
                MessageBox.Show("Access数据库的连接失败!", "Access数据库的连接");
                return;
            }
            else
            {
            }

            OleDbParameter[] parameters = null/*new OleDbParameter[5]*/;

            string sql = "delete * from tblEnroll";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            try
            {
                conn.Open();
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

            int dwBackupNum = cmbBackupNum.SelectedIndex;
            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
            int dwPrivilegeNum = cmbPrivilege.SelectedIndex;
            int dwEnrollNumber = Convert.ToInt32(tbEnrollNum.Text);

            int[] dwData = new int[1420 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);
            int dwPassword = 0;
            int dwCardNum = 0;

            if ( tbCardNum.TextLength > 0 )
            {
                dwCardNum = Int32.Parse(tbCardNum.Text);
            }

            if (dwBackupNum == 11)
            {
                if (dwCardNum != 0)
                {
                    dwPassword = dwCardNum;
                }
            }
            if (dwBackupNum <11)
            {
                return;
            }

            bool bRet;
            DisableDevice();

            bRet = pOcxObject.SetEnrollData(m_nMachineNum,
                dwEnrollNumber,
                dwEnMachineID,
                dwBackupNum,
                dwPrivilegeNum,
                ref obj,
                dwPassword);

            if (bRet)
            {
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
            
            //readDevice();
            //TODO
            btnGetEnrollInfo_Click(sender, e);

            btnGetAllEnData_Click(sender, e);
            listView1.Items.Clear();
            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2);
        }
        public void ConnectToSelectedDevices()
        {
            string enrolldbfPath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
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
                        string query = "SELECT IPAddr, DPort, Pwd FROM example WHERE DevName = ?";
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
                                    // ı need to convert string to int
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
                                                labelInfo.Text = $"Connected to {selectedName} ({ip}:{port}).";
                                            }
                                            else
                                            {
                                                MessageBox.Show($"Failed to connect to {selectedName} ({ip}:{port}).", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show($"Connection failed for {selectedName} ({ip}:{port}). Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Invalid IP format for {selectedName}: {ip}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"No matching record found for {selectedName} in the database.", "Record Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                tbEnrollNum.Text = item.SubItems[0].Text;
                cmbBackupNum.SelectedIndex = Convert.ToInt32(item.SubItems[1].Text);
                cmbPrivilege.SelectedIndex = Convert.ToInt32(item.SubItems[2].Text);
                tbEnrollName.Text = item.SubItems[3].Text;
                tbCardNum.Text = item.SubItems[4].Text;
            }
        }
    }
}
