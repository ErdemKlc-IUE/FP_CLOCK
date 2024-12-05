using FP_CLOCK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace FPClient
{
    public partial class bellTimeSetting : Form
    {
        private int m_nMachineNum;
        private AxFP_CLOCKLib.AxFP_CLOCK pOcxObject;

        SaveDevice saveDevice = new SaveDevice();
        EnrollDataManagement en = new EnrollDataManagement();
        string dbfFilePath = @"C:\EnGoPer\Data\Cihazlar.dbf";


        public bellTimeSetting()
        {
            InitializeComponent();
        }
        public bellTimeSetting(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {
            InitializeComponent();

            this.m_nMachineNum = nMachineNum;
            this.pOcxObject = ptrObject;

        }

        private void bellTimeSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Visible = true;

        }

        private unsafe void btnReadSetting_Click(object sender, EventArgs e)
        {

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
                        int nBellCount = 0;

                        BellInfo* pBellInfo = stackalloc BellInfo[1];
                        IntPtr ptr = new IntPtr(pBellInfo);

                        bool bRet;
                        bRet = pOcxObject.GetBellTime(m_nMachineNum, ref nBellCount, ptr);
                       
                        if (!bRet)
                        {
                            ShowErrorInfo();
                            pOcxObject.EnableDevice(m_nMachineNum, 1);
                            return;
                        }
                        checkBox1.Checked = pBellInfo->bValid[0] == 1 ? true : false;
                        checkBox2.Checked = pBellInfo->bValid[1] == 1 ? true : false;
                        checkBox3.Checked = pBellInfo->bValid[2] == 1 ? true : false;
                        checkBox4.Checked = pBellInfo->bValid[3] == 1 ? true : false;
                        checkBox5.Checked = pBellInfo->bValid[4] == 1 ? true : false;
                        checkBox6.Checked = pBellInfo->bValid[5] == 1 ? true : false;
                        checkBox7.Checked = pBellInfo->bValid[6] == 1 ? true : false;
                        checkBox8.Checked = pBellInfo->bValid[7] == 1 ? true : false;

                        textHour1.Text = pBellInfo->bHour[0].ToString();
                        textHour2.Text = pBellInfo->bHour[1].ToString();
                        textHour3.Text = pBellInfo->bHour[2].ToString();
                        textHour4.Text = pBellInfo->bHour[3].ToString();
                        textHour5.Text = pBellInfo->bHour[4].ToString();
                        textHour6.Text = pBellInfo->bHour[5].ToString();
                        textHour7.Text = pBellInfo->bHour[6].ToString();
                        textHour8.Text = pBellInfo->bHour[7].ToString();

                        textMinute1.Text = pBellInfo->bMinute[0].ToString();
                        textMinute2.Text = pBellInfo->bMinute[1].ToString();
                        textMinute3.Text = pBellInfo->bMinute[2].ToString();
                        textMinute4.Text = pBellInfo->bMinute[3].ToString();
                        textMinute5.Text = pBellInfo->bMinute[4].ToString();
                        textMinute6.Text = pBellInfo->bMinute[5].ToString();
                        textMinute7.Text = pBellInfo->bMinute[6].ToString();
                        textMinute8.Text = pBellInfo->bMinute[7].ToString();

                        textBellCount.Text = nBellCount.ToString();

                        labelInfo.Text = "Success...";
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

        private unsafe void btnWriteSetting_Click(object sender, EventArgs e)
        {


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
                        int nBellCount = 0;

                        BellInfo* pBellInfo = stackalloc BellInfo[1];
                        IntPtr ptr = new IntPtr(pBellInfo);

                        bool bRet;
                        bRet = pOcxObject.SetBellTime(m_nMachineNum, nBellCount, ptr);
                        if (!bRet)
                        {
                            ShowErrorInfo();
                            pOcxObject.EnableDevice(m_nMachineNum, 1);
                            return;
                        }

                        pBellInfo->bValid[0] = checkBox1.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[1] = checkBox2.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[2] = checkBox3.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[3] = checkBox4.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[4] = checkBox5.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[5] = checkBox6.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[6] = checkBox7.Checked == true ? (byte)1 : (byte)0;
                        pBellInfo->bValid[7] = checkBox8.Checked == true ? (byte)1 : (byte)0;

                        pBellInfo->bHour[0] = Convert.ToByte(textHour1.Text);
                        pBellInfo->bHour[1] = Convert.ToByte(textHour2.Text);
                        pBellInfo->bHour[2] = Convert.ToByte(textHour3.Text);
                        pBellInfo->bHour[3] = Convert.ToByte(textHour4.Text);
                        pBellInfo->bHour[4] = Convert.ToByte(textHour5.Text);
                        pBellInfo->bHour[5] = Convert.ToByte(textHour6.Text);
                        pBellInfo->bHour[6] = Convert.ToByte(textHour7.Text);
                        pBellInfo->bHour[7] = Convert.ToByte(textHour8.Text);

                        pBellInfo->bMinute[0] = Convert.ToByte(textMinute1.Text);
                        pBellInfo->bMinute[1] = Convert.ToByte(textMinute2.Text);
                        pBellInfo->bMinute[2] = Convert.ToByte(textMinute3.Text);
                        pBellInfo->bMinute[3] = Convert.ToByte(textMinute4.Text);
                        pBellInfo->bMinute[4] = Convert.ToByte(textMinute5.Text);
                        pBellInfo->bMinute[5] = Convert.ToByte(textMinute6.Text);
                        pBellInfo->bMinute[6] = Convert.ToByte(textMinute7.Text);
                        pBellInfo->bMinute[7] = Convert.ToByte(textMinute8.Text);

                        nBellCount = Convert.ToInt32(textBellCount.Text);

                        textBellCount.Text = nBellCount.ToString();

                        labelInfo.Text = "Success...";
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            Owner.Visible = true;
            this.Close();
        }
    }
}
