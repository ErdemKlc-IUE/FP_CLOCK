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
using ListView = System.Windows.Forms.ListView;
using Button = System.Windows.Forms.Button;
using System.Security.Cryptography;
using SortOrder = System.Windows.Forms.SortOrder;
using System.Windows.Forms.VisualStyles;

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
                new KeyValuePair<string, int>("Parmak İzi",0),
                new KeyValuePair<string, int>("Pin", 10),
                new KeyValuePair<string, int>("Kart", 11),

            };
            // Set the DataSource for the ComboBox
            this.cmbBackupNum.DataSource = backupOptions;
            this.cmbBackupNum.DisplayMember = "Key"; // Text to show to the user
            this.cmbBackupNum.ValueMember = "Value"; // Value to use in the application

            var backupOptions2 = new List<KeyValuePair<string, int>>()
            {
                new KeyValuePair<string, int>("Kullanıcı", 0),
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
        // Class-level variable to track the current sort order and column
        private int sortColumn = -1;
        private SortOrder sortOrder = SortOrder.None;

        public void InitializeListview()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            //listView1.OwnerDraw = true;
            //listView1.DrawItem += ListView1_DrawItem;
            listView1.DrawSubItem += ListView1_DrawSubItem;
            listView1.DrawColumnHeader += ListView1_DrawColumnHeader;


            // Add column headers
            listView1.Columns.Add("Kullanıcı No", 100);
            listView1.Columns.Add("Kayıt Tipi", 100);
            listView1.Columns.Add("Kullanıcı Yetki", 100);
            listView1.Columns.Add("Kullanıcı Adı", 100);
            listView1.Columns.Add("Kart Numarası", 100);

            // Add items to the ListView
            listView1.Items.Clear();
            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2,label1);

            // Assign the ColumnClick event
            listView1.ColumnClick += ListView1_ColumnClick;
        }
        // DrawSubItem olayı
        private void ListView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            Color backColor = e.ItemIndex % 2 == 0 ? Color.LightGray : Color.White;
            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);

            if (e.Item.Selected)
            {
                e.Graphics.FillRectangle(Brushes.Blue, e.Bounds);
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                listView1.Font,
                e.Bounds,
                e.Item.Selected ? Color.White : Color.Black,
                e.Item.Selected ? Color.Blue : backColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );
        }
        private void ListView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            // Sütun başlıklarını çiz
            using (SolidBrush backBrush = new SolidBrush(Color.WhiteSmoke))
            {
                e.Graphics.FillRectangle(backBrush, e.Bounds);
            }

            TextRenderer.DrawText(
                e.Graphics,
                e.Header.Text,
                listView1.Font,
                e.Bounds,
                Color.Black,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
            );

            // Kenarlık çiz (isteğe bağlı)
            //e.DrawEdge(EdgeStyle.Raised, EdgeEffects.None);
        }

        // ColumnClick event handler
        private void ListView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Toggle sort order or reset if a different column is clicked
            if (e.Column == sortColumn)
            {
                sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                sortColumn = e.Column;
                sortOrder = SortOrder.Ascending;
            }

            // Sort the items in the ListView
            List<ListViewItem> items = listView1.Items.Cast<ListViewItem>().ToList();

            if (sortOrder == SortOrder.Ascending)
            {
                items = items.OrderBy(item => ParseValue(item.SubItems[e.Column].Text)).ToList();
            }
            else
            {
                items = items.OrderByDescending(item => ParseValue(item.SubItems[e.Column].Text)).ToList();
            }

            // Clear the ListView and re-add sorted items
            listView1.Items.Clear();
            listView1.Items.AddRange(items.ToArray());
        }

        // Helper method to parse values
        private static IComparable ParseValue(string value)
        {
            // Try to parse as a number, fallback to string comparison if it fails
            if (double.TryParse(value, out double number))
            {
                return number; // Sort numerically
            }

            return value; // Fallback to string if not numeric
        }

        private void EnrollDataManagementForm_Closing(object sender, FormClosingEventArgs e)
        {
            //Owner.Visible = true;
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
                //labelInfo.Text = "Cihaz Bulunamadı!";
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
            DisableDevice();

            DialogResult dr = MessageBox.Show("Cihaz fabrika ayarlarına dönecektir. Tüm kullanıcı ve loglar silinecektir. Onaylıyor musunuz?",
                "Warning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (dr == DialogResult.No)
            {
                return;
            }

            Form form = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false // Ensures only one item can be selected
            };

            Button button1 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

            // Add controls to the form
            form.Controls.Add(listView);
            form.Controls.Add(button1);

            // Handle the button click event
            button1.Click += (sender2, e2) =>
            {
                if (listView.SelectedItems.Count > 0) // Check if an item is selected
                {
                    var selectedItem = listView.SelectedItems[0];
                    string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column

                    // Call the method to connect to the selected device
                    ConnectToSelectedDevices(new List<string> { selectedDeviceName });
                    if (!DisableDevice())
                    {
                        MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool bRet = pOcxObject.ClearKeeperData(m_nMachineNum);
                        if (bRet)
                        {
                            labelInfo.Text = "ClearKeeperData ...";
                        }
                        else
                        {
                            labelInfo.Text = "Error";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                form.Close(); // Close the form after selection
            };

            // Show the form as a dialog
            form.ShowDialog();

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }

        private void btnRmAllManager_Click(object sender, EventArgs e) // Tüm yöneticileri sil
        {
            bool bRet;

            // Disable the device before removing all managers
            DisableDevice();


            Form form = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false // Ensures only one item can be selected
            };

            Button button1 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

            // Add controls to the form
            form.Controls.Add(listView);
            form.Controls.Add(button1);

            // Handle the button click event
            button1.Click += (sender2, e2) =>
            {
                if (listView.SelectedItems.Count > 0) // Check if an item is selected
                {
                    var selectedItem = listView.SelectedItems[0];
                    string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column

                    // Call the method to connect to the selected device
                    ConnectToSelectedDevices(new List<string> { selectedDeviceName });
                    if (!DisableDevice())
                    {
                        MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bRet = pOcxObject.BenumbAllManager(m_nMachineNum);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                form.Close(); // Close the form after selection
            };

            // Show the form as a dialog
            form.ShowDialog();

            pOcxObject.EnableDevice(m_nMachineNum, 1);
        }

        private void btnSetCompanyString_Click(object sender, EventArgs e)
        {
            DisableDevice();
            Form form = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false // Ensures only one item can be selected
            };

            Button button1 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

            // Add controls to the form
            form.Controls.Add(listView);
            form.Controls.Add(button1);

            // Handle the button click event
            button1.Click += (sender2, e2) =>
            {
                if (listView.SelectedItems.Count > 0) // Check if an item is selected
                {
                    var selectedItem = listView.SelectedItems[0];
                    string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column

                    // Call the method to connect to the selected device
                    ConnectToSelectedDevices(new List<string> { selectedDeviceName });
                    if (!DisableDevice())
                    {
                        MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
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

                    }
                }
                else
                {
                    MessageBox.Show("Lütfen cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                form.Close(); // Close the form after selection
            };

            // Show the form as a dialog
            form.ShowDialog();

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }

        private void btnDelCompanyString_Click(object sender, EventArgs e)
        {
            DisableDevice();
            Form form = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false // Ensures only one item can be selected
            };

            Button button1 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

            // Add controls to the form
            form.Controls.Add(listView);
            form.Controls.Add(button1);

            // Handle the button click event
            button1.Click += (sender2, e2) =>
            {
                if (listView.SelectedItems.Count > 0) // Check if an item is selected
                {
                    var selectedItem = listView.SelectedItems[0];
                    string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column

                    // Call the method to connect to the selected device
                    ConnectToSelectedDevices(new List<string> { selectedDeviceName });
                    if (!DisableDevice())
                    {
                        MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string strName = "";

                        bool bRet;

                        Object ob = new System.Runtime.InteropServices.VariantWrapper(strName);

                        try
                        {
                            pOcxObject.SetCompanyName(m_nMachineNum,
                            0,   //clean
                            ref ob);
                            labelInfo.Text="Şirket ismi silindi";
                            MessageBox.Show("Cihaz şirket ismi başarıyla silindi", "Şirket ismi başarıyla silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        catch (Exception ev)
                        {
                            MessageBox.Show("Cihaz şirket ismi silinemedi","Şirket ismi silinemedi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            common.DebugOut(ev.ToString());
                        }

                        /*if (bRet)
                        {
                            labelInfo.Text = "Cihaz şirket ismi gönderildi.";
                        }
                        else
                        {
                            ShowErrorInfo();
                        }*/

                    }
                }
                else
                {
                    MessageBox.Show("Lütfen cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                form.Close(); // Close the form after selection
            };

            // Show the form as a dialog
            form.ShowDialog();

            pOcxObject.EnableDevice(m_nMachineNum, 1);

        }

        /*private void btnGetUserName_Click(object sender, EventArgs e)
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
            
        }*/

        private void EnableDevice()
        {
            pOcxObject.EnableDevice(m_nMachineNum, 1);
        }

        /* private void btnSetUserName_Click(object sender, EventArgs e)//Önce Cihaz sonra Database Kullanıcı Adı kaydet
         {
             DisableDevice();
             int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1;
             int dwEnrollNum = Convert.ToInt32(tbEnrollNum.Text);
             int dwBackupNum = ((KeyValuePair<string, int>)cmbBackupNum.SelectedItem).Value;

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
                         string updateQuery = "UPDATE EnrollData SET EName = ? WHERE ENumber = ? AND FNumber = ?";
                         using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
                         {
                             cmd.Parameters.AddWithValue("@EName", strName);
                             cmd.Parameters.AddWithValue("@ENumber", dwEnrollNum);
                             cmd.Parameters.AddWithValue("@FNumber", dwBackupNum);

                             // Eşleşen satırları güncelle ve etkilenen satır sayısını kontrol et
                             int rowsAffected = cmd.ExecuteNonQuery();
                             if (rowsAffected == 0)
                             {
                                 // Eğer etkilenen satır yoksa kayıt bulunamadı
                                 MessageBox.Show("Error: Belirtilen kullanıcı numarası bilgisayarda bulunamadı.",
                                     "Kayıt Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 labelInfo.Text = "Kullanıcı numarası bulunamadı.";

                             }
                             else
                             {
                                 // Başarılı güncelleme mesajı
                                 labelInfo.Text = "Başarılı!";
                             }
                         }
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                     }
                 }

                 listView1.Items.Clear();
                 saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);
             }
             else
             {
                 ShowErrorInfo();
             }

             EnableDevice();
         }*/

        private void btnModifyPrivilege_Click(object sender, EventArgs e)
        {
            DisableDevice();

            bool isAnyItemSelected = false;
            bool isAnyPrivilegeUpdated = false; // Yetki güncellenip güncellenmediğini izlemek için bir bayrak

            // Seçili öğe olup olmadığını kontrol et
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)  // Eğer öğe işaretlenmişse
                {
                    isAnyItemSelected = true;
                    break;  // Bir öğe seçili olduğunda döngüyü kır
                }
            }

            // Eğer hiç seçili öğe yoksa
            if (!isAnyItemSelected)
            {
                MessageBox.Show("Yetki güncellemesi için kullanıcı seçilmedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Kodun devam etmesini engelle
            }

            foreach (ListViewItem item in listView1.Items)
            {
                // Check if the checkbox is selected for this item
                if (item.Checked)
                {
                    // Extract the necessary data from the ListView item
                    int dwEnrollNum = Int32.Parse(item.SubItems[0].Text); // Assuming the first column is EnrollNum
                    int dwEnMachineID = Int32.Parse("1"); // Assuming the second column is EnMachineID
                    int dwBackupNum = Int32.Parse(item.SubItems[1].Text); // Assuming the third column is BackupNum
                    int dwPrivilegeOld = Int32.Parse(item.SubItems[2].Text);

                    int dwPrivilege = ((KeyValuePair<string, int>)cmbPrivilege.SelectedItem).Value;

                    // Eğer yetki değişmemişse bir sonraki öğeye geç
                    if (dwPrivilegeOld == dwPrivilege)
                        continue;

                    // Yetki değişikliği yapıldığını belirtmek için bayrağı güncelle
                    isAnyPrivilegeUpdated = true;

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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                        }
                    }
                }
            }

            // Eğer hiçbir yetki güncellenmediyse, metottan çık
            if (!isAnyPrivilegeUpdated)
            {
                EnableDevice(); // Cihazı etkinleştirmeyi unutmayın
                return;
            }

            MessageBox.Show("İzin güncelleme başarılı.", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            labelInfo.Text = "Başarılı: İzin güncellendi";

            selectAllBox.CheckState = CheckState.Unchecked;
            listView1.Items.Clear();
            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);

            EnableDevice();
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
        private void btnDelEnData_Click(object sender, EventArgs e)//Seçili Kullanıcı Sil [[  HEM DATABASE HEM CİHAZ SORU-CEVAP İLE  ]]
        {
            // Disable the device to prevent conflicts
            DisableDevice();
            bool isAnyItemSelected = false;

            // Seçili öğe olup olmadığını kontrol et
            foreach (ListViewItem item2 in listView1.Items)
            {
                if (item2.Checked)  // Eğer öğe işaretlenmişse
                {
                    isAnyItemSelected = true;
                    break;  // Bir öğe seçili olduğunda döngüyü kır
                }
            }

            // Eğer hiç seçili öğe yoksa
            if (!isAnyItemSelected)
            {
                MessageBox.Show("Silmek için kullanıcı seçilmedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;  // Kodun devam etmesini engelle
            }

            // Eğer bir öğe seçili ise, burada işleminiz devam eder.



            DialogResult dr;
            dr = MessageBox.Show("Cihazdan silinsin mi?", "Kullanıcı veri sil? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                return;
            }
            else if (dr == DialogResult.Yes)
            {
                Form form = new Form
                {
                    Text = "Cihaz Seç",
                    Size = new Size(500, 350),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false,
                    ControlBox = true,
                    ShowIcon = true,
                    ShowInTaskbar = false,
                    TopMost = true,
                    AutoScaleMode = AutoScaleMode.Font,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    HelpButton = false,
                    BackColor = SystemColors.InactiveCaption,
                    ForeColor = SystemColors.ControlText
                };

                // ListView creation
                ListView listView = new ListView
                {
                    Size = new Size(410, 210),
                    Location = new Point(30, 30),
                    View = View.Details,
                    FullRowSelect = true,
                    GridLines = true,
                    MultiSelect = false // Ensures only one item can be selected
                };

                Button button1 = new Button
                {
                    Text = "Seç",
                    Size = new Size(100, 30),
                    Location = new Point(200, 250),
                    DialogResult = DialogResult.OK
                };

                // Add columns to the ListView
                listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
                listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
                listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
                listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
                listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

                // Add items to the ListView
                listView.Items.Clear();
                saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

                // Add controls to the form
                form.Controls.Add(listView);
                form.Controls.Add(button1);

                // Handle the button click event
                button1.Click += (sender2, e2) =>
                {
                    if (listView.SelectedItems.Count > 0) // Check if an item is selected
                    {
                        var selectedItem = listView.SelectedItems[0];
                        string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column

                        // Call the method to connect to the selected device
                        ConnectToSelectedDevices(new List<string> { selectedDeviceName });
                        if (!DisableDevice())
                        {
                            MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
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
                                        DialogResult dr2 = MessageBox.Show("Bilgisayardan silinsin mi?", "Bilgisayar temizle.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                        if (dr2 == DialogResult.No)
                                            return;
                                        else if (dr2 == DialogResult.Yes)
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
                                                    string deleteQuery = "DELETE FROM EnrollData WHERE ENumber = ? AND FNumber = ?";

                                                    using (OleDbCommand cmd = new OleDbCommand(deleteQuery, conn))
                                                    {
                                                        cmd.Parameters.AddWithValue("?", dwEnrollNum);
                                                        cmd.Parameters.AddWithValue("?", dwBackupNum);

                                                        int rowsAffected = cmd.ExecuteNonQuery();

                                                        if (rowsAffected > 0)
                                                        {
                                                            labelInfo.Text = "Kullanıcı kayıt silindi.";
                                                                                                    listView1.Items.Remove(item);

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
                                        }
                                    }
                                    else
                                    {
                                        // If deletion from the device fails, show an error
                                        ShowErrorInfo();
                                    }
                                }
                                
                            }
                            listView1.Items.Clear();
                            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Lütfen cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    form.Close(); // Close the form after selection
                };

                // Show the form as a dialog
                form.ShowDialog();
            }            

            // Re-enable the device after the operation
            EnableDevice();
        }

        private void btnEmptyEnData_Click(object sender, EventArgs e)//Tüm Kullanıcı Cihazdan Temizle
        { 
            DisableDevice();

            DialogResult dr;
            dr = MessageBox.Show("Cihazdaki tüm kullanıcılar silinecek, onaylıyor musunuz?", "Cihaz temizle? ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.No)
            {
                return;
            }
            Form form = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false // Ensures only one item can be selected
            };

            Button button1 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

            // Add controls to the form
            form.Controls.Add(listView);
            form.Controls.Add(button1);

            button1.Click += (sender2, e2) =>
            {
                if (listView.SelectedItems.Count > 0) // Check if an item is selected
                {
                    List<string> selectedDeviceNames = new List<string>();

                    // Iterate through all selected items and get their device names
                    foreach (ListViewItem selectedItem in listView.SelectedItems)
                    {
                        string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column
                        selectedDeviceNames.Add(selectedDeviceName);
                    }

                    // Call the method to connect to the selected devices
                    ConnectToSelectedDevices(selectedDeviceNames);

                    if (!DisableDevice())
                    {
                        MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        bool bRet = pOcxObject.EmptyEnrollData(m_nMachineNum);
                        if (bRet)
                        {
                            labelInfo.Text = "Cihaz temizlendi.";
                            // TODO : cihaz temizlendiğinde, veritabanındaki verileri temizle
                            //btnDelDBData_Click(sender, e);
                        }
                        else
                        {
                            ShowErrorInfo();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir cihaz seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                form.Close(); // Close the form after selection
            };

            form.ShowDialog();

            EnableDevice();

        }

        //Bu method önce cihazdan verileri çekiyor sonra database kaydediyor.
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
            //ConnectToSelectedDevices2();

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
                        MessageBox.Show("Veritabanı bağlantı başarısız!", "Bağlantı Hatası");
                        return;
                    }

                    string query = "SELECT * FROM EnrollData";
                    using (OleDbCommand command = new OleDbCommand(query, conn))
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            labelInfo.Text = "Veritabanı boş.";
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
                                        if (MessageBox.Show($"Kayıt hatası {dwEnrollNumber}. Continue?", "Hata", MessageBoxButtons.YesNo) == DialogResult.No)
                                        {
                                            labelInfo.Text = $"Bilgi kayıt edilirken hata {dwEnrollNumber}";
                                            return;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Satır işlenirken hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }

                        // Eğer hiç seçili öğe yoksa uyarı göster
                        if (!hasCheckedItems)
                        {
                            MessageBox.Show("Lütfen bir kayıt seçiniz.", "Kayıt seçilmedi!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Veritabanı bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelDBData_Click(object sender, EventArgs e)// Tüm Kullanıcı Bilgisayardan Temizle
        {
            DialogResult dr;
            dr = MessageBox.Show("Tüm kullanıcılar bilgisayardan silinsin mi?", "Bilgisayar temizlenecek? ", MessageBoxButtons.YesNo,  MessageBoxIcon.Asterisk);
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
                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);

            }
            catch (Exception ec)
            {
                throw ec;
            }

        }

        private void addDatabase(int dwEnMachineID, int dwEnrollNumber, int dwBackupNum, int dwPrivilegeNum, int dwPassWord) // Sadece Database kaydetme işlemi yapan method
        {
            OleDbConnection conn;
            string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";
            conn = new OleDbConnection(strConnection);
            conn.Open();

            string sql;
            bool bBreakFail = false;


            OleDbParameter[] parameters = new OleDbParameter[5];
            parameters[0] = new OleDbParameter("@EMNo", OleDbType.Integer);
            parameters[0].Value = dwEnMachineID;

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
                    else
                        MessageBox.Show("Bu kayıt veritabanında bulunduğu için tekrar kayıt edilemez. Kullanıcı numarasını veya Kayıt tipini değiştirin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Bu kayıt tipinde veri eklenemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }
        
        private void addDatabaseUserName(int dwEnrollNum, string strName ,int dwBackupNum)//Sadece Database Kullanıcı Adı Ekle Methodu
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
                    string updateQuery = "UPDATE EnrollData SET EName = ? WHERE ENumber = ? AND FNumber = ?";
                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@EName", strName);
                        cmd.Parameters.AddWithValue("@ENumber", dwEnrollNum);
                        cmd.Parameters.AddWithValue("@FNumber", dwBackupNum);


                        // Eşleşen satırları güncelle ve etkilenen satır sayısını kontrol et
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Eğer etkilenen satır yoksa kayıt bulunamadı
                            MessageBox.Show("Error: Belirtilen kullanıcı numarası bilgisayarda bulunamadı.",
                                "Kayıt Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            labelInfo.Text = "Kullanıcı numarası bulunamadı.";

                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı güncellendi. ", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Başarılı güncelleme mesajı
                            labelInfo.Text = "Başarılı!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                }
            }

            listView1.Items.Clear();
            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);
        }
        
        private void updateDatabaseCardNumber(int dwEnrollNum, int dwBackupNum, int dwCardNumber)
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
                    string updateQuery = "UPDATE EnrollData SET EnPw = ? WHERE ENumber = ? AND FNumber = ?";
                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@EnPw", dwCardNumber);
                        cmd.Parameters.AddWithValue("@ENumber", dwEnrollNum); 
                        cmd.Parameters.AddWithValue("@FNumber", dwBackupNum);


                        // Eşleşen satırları güncelle ve etkilenen satır sayısını kontrol et
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            // Eğer etkilenen satır yoksa kayıt bulunamadı
                            MessageBox.Show("Error: Belirtilen kullanıcı numarası bilgisayarda bulunamadı.",
                                "Kayıt Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            labelInfo.Text = "Kullanıcı numarası bulunamadı.";

                        }
                        else
                        {
                            MessageBox.Show("Kart numarası güncellendi. ", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Başarılı güncelleme mesajı
                            labelInfo.Text = "Başarılı!";
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veritabanı Hatası: " + ex.Message);
                }
            }

            listView1.Items.Clear();
            saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);
        }
        private void btnSetEnrollData_Click(object sender, EventArgs e) //Ekle / Düzenle
        {
            DisableDevice();

            // Extract selected values from ComboBoxes using ValueMember
            int dwBackupNum = ((KeyValuePair<string, int>)cmbBackupNum.SelectedItem).Value;
            int dwPrivilegeNum = ((KeyValuePair<string, int>)cmbPrivilege.SelectedItem).Value;

            int dwEnMachineID = cmbEMachineNum.SelectedIndex + 1; // Assuming this ComboBox doesn't need changes
            int dwEnrollNumber = Convert.ToInt32(tbEnrollNum.Text);

            string strName = tbEnrollName.TextLength == 0 ? "" : tbEnrollName.Text;

            //object obj2 = new System.Runtime.InteropServices.VariantWrapper(strName);


            //int[] dwData = new int[1420 / 4];
            //object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);
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
                // Refresh the data and update UI
                addDatabase(dwEnMachineID, dwEnrollNumber, dwBackupNum, dwPrivilegeNum, dwPassword);
                if(tbEnrollName.TextLength > 0)
                {
                    DialogResult dr = MessageBox.Show("Girilen kullanıcı numarası için kullanıcı adı güncellensin mi", "Kullanıcı Adı Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // if user click yes
                    if(dr == DialogResult.Yes)
                        addDatabaseUserName(dwEnrollNumber, strName,dwBackupNum);
                }
                if (tbCardNum.TextLength > 0)
                {
                    DialogResult dr = MessageBox.Show("Girilen kullanıcı numarası için kart numarası güncellensin mi", "Kart Numarası Güncelleme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    // if user click yes
                    if (dr == DialogResult.Yes)
                        updateDatabaseCardNumber(dwEnrollNumber, dwBackupNum, dwCardNum);
                }
                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);

            }
            else
            {
                MessageBox.Show("Bilgisayara parmak izi eklemesi uygulamadan yapılamaz!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        /*private void connectButton_Click(object sender, EventArgs e)
        {
            //ConnectToSelectedDevices();
            if (!DisableDevice())
            {
                MessageBox.Show("Failed to disable device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                btnGetEnrollInfo_Click(sender, e);

                btnGetAllEnData_Click(sender, e);
                listView1.Items.Clear();
                saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);
            }
        }*/

        /*public void ConnectToSelectedDevices()
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
        }*/

        /*public void ConnectToSelectedDevices2()
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
        }*/

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (listView1.SelectedItems.Count == 0)
            {
                tbEnrollNum.Text = string.Empty;
                cmbBackupNum.SelectedIndex = -1;
                cmbPrivilege.SelectedIndex = -1;
                tbEnrollName.Text = string.Empty;
                tbCardNum.Text = string.Empty;
                return;
            }
            else if (listView1.SelectedItems.Count > 0)
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

        // This method will create the form with the ListView and Button
        public void customButton1_Click(object sender, EventArgs e) // Cihazdan Al Butonu
        {
            Form form = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false // Ensures only one item can be selected
            };

            Button button1 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView, dbfFilePath);

            // Add controls to the form
            form.Controls.Add(listView);
            form.Controls.Add(button1);

            // Handle the button click event
            button1.Click += (sender2, e2) =>
            {
                if (listView.SelectedItems.Count > 0) // Check if an item is selected
                {
                    var selectedItem = listView.SelectedItems[0];
                    string selectedDeviceName = selectedItem.SubItems[1].Text; // Assuming device name is in the second column

                    // Call the method to connect to the selected device
                    ConnectToSelectedDevices(new List<string> { selectedDeviceName });
                    if (!DisableDevice())
                    {
                        MessageBox.Show("Cihaz bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        btnGetEnrollInfo_Click(sender, e);

                        btnGetAllEnData_Click(sender, e);

                        listView1.Items.Clear();
                        saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                form.Close(); // Close the form after selection
            };

            // Show the form as a dialog
            form.ShowDialog();
        }

        // This method will connect to the selected devices
        public void ConnectToSelectedDevices(List<string> selectedDevices)
        {
            string enrolldbfPath = @"C:\EnGoPer\Data\Cihazlar.dbf";
            string directoryPath = Path.GetDirectoryName(enrolldbfPath);
            string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

            using (OleDbConnection con = new OleDbConnection(strConnection))
            {
                try
                {
                    con.Open();

                    foreach (string selectedName in selectedDevices)
                    {
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

        private void customButton2_Click(object sender, EventArgs e) // Cihaza Yolla Butonu
        {
            Form form2 = new Form
            {
                Text = "Cihaz Seç",
                Size = new Size(500, 350),
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ControlBox = true,
                ShowIcon = true,
                ShowInTaskbar = false,
                TopMost = true,
                AutoScaleMode = AutoScaleMode.Font,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                HelpButton = false,
                BackColor = SystemColors.InactiveCaption,
                ForeColor = SystemColors.ControlText
            };

            // ListView creation
            ListView listView2 = new ListView
            {
                Size = new Size(410, 210),
                Location = new Point(30, 30),
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                CheckBoxes = true
            };

            Button button2 = new Button
            {
                Text = "Seç",
                Size = new Size(100, 30),
                Location = new Point(200, 250),
                DialogResult = DialogResult.OK
            };

            // Add columns to the ListView
            listView2.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView2.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView2.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView2.Columns.Add("Port", 60, HorizontalAlignment.Left);
            listView2.Columns.Add("Şifre", 70, HorizontalAlignment.Left);

            // Add items to the ListView
            listView2.Items.Clear();
            saveDevice.LoadDBFDataToListView(listView2, dbfFilePath);

            // Add controls to the form
            form2.Controls.Add(listView2);
            form2.Controls.Add(button2);

            // Handle the button click event
            button2.Click += (sender3, e3) =>
            {
                // Collect selected items from ListView
                List<string> selectedDevices2 = new List<string>();
                foreach (ListViewItem item in listView2.CheckedItems)
                {
                    selectedDevices2.Add(item.SubItems[1].Text);  // Assuming the device name is in the second column
                }

                if (selectedDevices2.Count == 0)
                {
                    MessageBox.Show("Lütfen en az bir cihaz seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Call the method to connect to the selected devices
                //ConnectToSelectedDevices(selectedDevices2);

                // Perform actions for each connected device
                foreach (ListViewItem item in listView2.CheckedItems)
                {
                    string selectedName = item.SubItems[1].Text;  // Assuming the device name is in the second column
                    string ipAddress = item.SubItems[2].Text;     // IP address from 3rd column
                    string port = item.SubItems[3].Text;          // Port number from 4th column

                    try
                    {
                        int portInt = Convert.ToInt32(port);
                        int passwordInt = Convert.ToInt32(item.SubItems[4].Text); // Password from 5th column

                        // Attempt to connect to the device
                        bool bRet = pOcxObject.SetIPAddress(ref ipAddress, portInt, passwordInt);
                        bRet = pOcxObject.OpenCommPort(m_nMachineNum);

                        if (bRet)
                        {
                            //labelInfo.Text = $"Bağlandı! {selectedName} ({ipAddress}:{port}).";
                            MessageBox.Show($"Bağlandı! {selectedName} ({ipAddress}:{port}).", "Bağlantı Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Call the method for enrolling data or setting data on the device
                            btnSetAllEnData_Click(sender, e);
                        }
                        else
                        {
                            MessageBox.Show($"Cihaza Bağlanılamadı! {selectedName} ({ipAddress}:{port}).", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Cihaza Bağlanmadı! {item.SubItems[1].Text}. Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                form2.Close();  // Close the form after processing
            };

            // Show the form as a dialog
            form2.ShowDialog();
        }

        private void customButton3_Click(object sender, EventArgs e) // Sadece Database Sil Butonu
        {
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


                    // If the user is deleted from the device, delete from the database as well
                    string enrolldbfPath = @"C:\EnGoPer\Data\EnrollData.dbf";
                    string directoryPath = Path.GetDirectoryName(enrolldbfPath);
                    string strConnection = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                    using (OleDbConnection conn = new OleDbConnection(strConnection))
                    {
                        try
                        {
                            conn.Open();
                            string deleteQuery = "DELETE FROM EnrollData WHERE ENumber = ? AND FNumber = ?"; 

                            using (OleDbCommand cmd = new OleDbCommand(deleteQuery, conn))
                            {
                                cmd.Parameters.AddWithValue("?", dwEnrollNum);
                                cmd.Parameters.AddWithValue("?", dwBackupNum);
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

                    listView1.Items.Clear();
                    saveDevice.LoadDBFDataToListView2(listView1, dbfFilePath2, label1);

                }
                else
                {
                    // If deletion from the device fails, show an error
                    ShowErrorInfo();
                }
            }
        }


    }
}