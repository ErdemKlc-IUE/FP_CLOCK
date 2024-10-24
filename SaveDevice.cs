using AxFP_CLOCKLib;
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

namespace FP_CLOCK
{
    public partial class SaveDevice : Form
    {
        private int m_nMachineNum;
        private AxFP_CLOCKLib.AxFP_CLOCK pOcxObject;
        private ListViewItem selectedItem = null; // To hold the currently selected item for editing

        string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";

        public SaveDevice()
        {
            InitializeComponent();
            //InitializeListView();
        }
        public SaveDevice(int nMachineNum, ref AxFP_CLOCKLib.AxFP_CLOCK ptrObject)
        {
            InitializeComponent();
            this.pOcxObject = ptrObject;
            this.m_nMachineNum = nMachineNum;
            listView1.GridLines = true;
            InitializeListView();


        }
        private void SaveDeviceForm_Closing(object sender, FormClosingEventArgs e)
        {
            Owner.Visible = true;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
           /* dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";


            // Eğer ListView boşsa uyarı ver
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("There are no items to save.");
                return;
            }
            //Ip ve Port bilgileri aynı olmamalı
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                for (int j = i + 1; j < listView1.Items.Count; j++)
                {
                    if (listView1.Items[i].SubItems[2].Text == listView1.Items[j].SubItems[2].Text && listView1.Items[i].SubItems[3].Text == listView1.Items[j].SubItems[3].Text)
                    {
                        MessageBox.Show("Aynı IP ve Port bilgileri olan cihazlar olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        listView1.Clear();
                        InitializeListView();

                        return;
                    }
                }
            }

            // ListView öğelerini WelcomePage'e gönder

            MessageBox.Show("Bütün cihazlar başarılı bir şekilde kaydedildi.");

            dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.DBF";
            SaveListViewToDBF(listView1, dbfFilePath);
*/
            // Bu formu gizle ve WelcomePage'i göster
            WelcomePage welcomePage = new WelcomePage();
            this.Visible = false;
            welcomePage.Show();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Save the device to the listview
            
            string strDeviceName = deviceNameTextBox.Text;
            string strDeviceIP = ipTextBox.Text;
            string strDevicePort = portTextBox.Text;
            string strDevicePassword = pwTextBox.Text;
            string strDurum = "Kapalı";
            string strSeriNo = "Bilinmiyor";

            if (string.IsNullOrWhiteSpace(strDeviceName) || string.IsNullOrWhiteSpace(strDeviceIP) ||
                string.IsNullOrWhiteSpace(strDevicePort) || string.IsNullOrWhiteSpace(strDevicePassword))
            {
                MessageBox.Show("Please fill in all the fields before adding.");
                return;
            }

            try
            {
                // Ensure the correct DBF file path
                string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
                string directoryPath = Path.GetDirectoryName(dbfFilePath);

                // Create connection string for the DBF file
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Check if the table exists by attempting to read from it
                    bool tableExists = false;
                    try
                    {
                        string checkTableExistsQuery = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)}";
                        using (OleDbCommand checkCommand = new OleDbCommand(checkTableExistsQuery, connection))
                        {
                            checkCommand.ExecuteNonQuery();
                            tableExists = true; // If this runs without exception, the table exists
                        }
                    }
                    catch
                    {
                        tableExists = false; // If an exception occurs, the table does not exist
                    }

                    // If the table doesn't exist, create it
                    if (!tableExists)
                    {
                        string createTableCommandText = "CREATE TABLE " + Path.GetFileNameWithoutExtension(dbfFilePath) +
                            " (ID CHAR(50), DevName CHAR(10), IPAddr CHAR(15), DPort CHAR(4), Pwd CHAR(4), Durum CHAR(10), SeriNo CHAR(20))";
                        using (OleDbCommand createTableCommand = new OleDbCommand(createTableCommandText, connection))
                        {
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Check if the device already exists in the database

            if (CheckIfDeviceExists(strDeviceIP,strDevicePort))
            {
                MessageBox.Show("A device with the same port number already exists in the database.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Prevent adding the device
            }

            InsertIntoDBF(strDeviceName, strDeviceIP, strDevicePort, strDevicePassword, strDurum, strSeriNo);
            // Insert data into the database
            LoadDBFDataToListView(listView1, dbfFilePath);
            // Optionally, clear the textboxes after adding the item
            deviceNameTextBox.Clear();
            ipTextBox.Clear();
            portTextBox.Clear();
            pwTextBox.Clear();
        }
        // Method to check if a device with the same port exists in the database
        private bool CheckIfDeviceExists(string ip, string port)
        {
            string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
            string directoryPath = Path.GetDirectoryName(dbfFilePath);
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string checkQuery = $"SELECT COUNT(*) FROM {Path.GetFileNameWithoutExtension(dbfFilePath)} WHERE IPAddr = '{ip}' AND DPort = '{port}'";

                using (OleDbCommand command = new OleDbCommand(checkQuery, connection))
                {
                    int count = (int)command.ExecuteScalar();
                    return count > 0; // Return true if a record exists
                }
            }
        }
        // Method to insert data into the DBF file
        private void InsertIntoDBF(string deviceName, string ip, string port, string password, string durum, string seriNo)
        {
            listView1.Items.Clear();
            try
            {
                // Ensure the correct DBF file path
                string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
                string directoryPath = Path.GetDirectoryName(dbfFilePath);

                // Create connection string for the DBF file
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Check for existing entry with the same ID and Port
                    string checkExistingQuery = $"SELECT COUNT(*) FROM {Path.GetFileNameWithoutExtension(dbfFilePath)} WHERE IPAddr = '{ip}'AND DPort = '{port}'";
                    using (OleDbCommand checkCommand = new OleDbCommand(checkExistingQuery, connection))
                    {
                        int existingCount = (int)checkCommand.ExecuteScalar();
                        if (existingCount > 0)
                        {
                            MessageBox.Show("A device with the same port number already exists in the database.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return; // Prevent adding the device
                        }
                    }

                    // Get the last ID and increment it for the new entry
                    string lastIdQuery = $"SELECT MAX(ID) FROM {Path.GetFileNameWithoutExtension(dbfFilePath)}";
                    int newId = 1; // Default ID

                    using (OleDbCommand lastIdCommand = new OleDbCommand(lastIdQuery, connection))
                    {
                        object result = lastIdCommand.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            newId = Convert.ToInt32(result) + 1; // Increment last ID
                        }
                    }

                    // Insert the new record into the DBF file
                    string insertCommandText = $"INSERT INTO {Path.GetFileNameWithoutExtension(dbfFilePath)} " +
                        $"(ID, DevName, IPAddr, DPort, Pwd, Durum, SeriNo) VALUES ('{newId}', '{deviceName}', '{ip}', '{port}', '{password}', '{durum}', '{seriNo}')";

                    using (OleDbCommand insertCommand = new OleDbCommand(insertCommandText, connection))
                    {
                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Device successfully added to the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to add the device to the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding to the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("ID", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Cihaz İsmi", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("IP Adresi", 125, HorizontalAlignment.Left);
            listView1.Columns.Add("Port", 50, HorizontalAlignment.Left);
            listView1.Columns.Add("Şifre", 50, HorizontalAlignment.Left);

            dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
            LoadDBFDataToListView(listView1, dbfFilePath);


        }
        public void SaveListViewToDBF(ListView listView, string dbfFilePath)
        {
           /* try
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
                    // Check if the table exists by attempting to read from it
                    bool tableExists = false;
                    try
                    {
                        string checkTableExistsQuery = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)}";
                        using (OleDbCommand checkCommand = new OleDbCommand(checkTableExistsQuery, connection))
                        {
                            checkCommand.ExecuteNonQuery();
                            tableExists = true; // If this runs without exception, the table exists
                        }
                    }
                    catch
                    {
                        tableExists = false; // If an exception occurs, the table does not exist
                    }
                    // If the table doesn't exist, create it
                    if (!tableExists)
                    {
                        string createTableCommandText = "CREATE TABLE " + Path.GetFileNameWithoutExtension(dbfFilePath) +
                            " (ID CHAR(5), DevName CHAR(10), IPAddr CHAR(15), DPort CHAR(4), Pwd CHAR(4))";
                        using (OleDbCommand createTableCommand = new OleDbCommand(createTableCommandText, connection))
                        {
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                    // Insert data from the ListView into the DBF file
                    foreach (ListViewItem item in listView.Items)
                    {
                        string id = item.SubItems[0].Text;
                        string deviceName = item.SubItems[1].Text;
                        string ip = item.SubItems[2].Text;
                        string port = item.SubItems[3].Text;
                        string password = item.SubItems[4].Text;
                        // Insert the data into the table
                        string insertCommandText = $"INSERT INTO {Path.GetFileNameWithoutExtension(dbfFilePath)} " +
                            $"(ID, DevName, IPAddr, DPort, Pwd) VALUES ('{id}','{deviceName}', '{ip}', '{port}', '{password}')";
                        using (OleDbCommand insertCommand = new OleDbCommand(insertCommandText, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Data saved to .DBF file successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving to .DBF file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }
        public void LoadDBFDataToListView(ListView listView, string dbfFilePath)
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

                    // Check if the table exists by attempting to read from it
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

                    // If the table exists, load the data into the ListView
                    if (tableExists)
                    {
                        string selectQuery = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)}";
                        using (OleDbCommand selectCommand = new OleDbCommand(selectQuery, connection))
                        {
                            using (OleDbDataReader reader = selectCommand.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    // Assuming your DBF fields are DevName, IPAddr, DPort, Pwd
                                    string id = reader["ID"].ToString(); // Assuming there's an "ID" column
                                    string deviceName = reader["DevName"].ToString();
                                    string ip = reader["IPAddr"].ToString();
                                    string port = reader["DPort"].ToString();
                                    string password = reader["Pwd"].ToString();
                                    string durum = reader["Durum"].ToString();
                                    string seriNo = reader["SeriNo"].ToString();

                                    // Create ListViewItem and add it to the ListView
                                    ListViewItem item = new ListViewItem(id);
                                    item.SubItems.Add(deviceName);
                                    item.SubItems.Add(ip);
                                    item.SubItems.Add(port);
                                    item.SubItems.Add(password);
                                    item.SubItems.Add(durum);
                                    item.SubItems.Add(seriNo);
                                    listView.Items.Add(item);
                                }
                            }
                        }
                    }
                    else
                    {
                        //MessageBox.Show("The DBF table does not exist or contains no data.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data from .DBF file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Check if an item is selected in the ListView
            if (listView1.SelectedItems.Count > 0)
            {
                // Get the selected item
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Get the unique identifier (e.g., IP and Port)
                string deviceIp = selectedItem.SubItems[2].Text;
                string devicePort = selectedItem.SubItems[3].Text;

                // Delete the corresponding row from the DBF file based on Device IP and Port
                DeleteFromDBF(deviceIp, devicePort);

                // Remove the selected item from the ListView
                listView1.Items.Remove(selectedItem);

                // Reassign new IDs to close the gap
                ReassignIDs();

                LoadDBFDataToListView(listView1, dbfFilePath);
            }
        }

        // Method to delete the corresponding item from the DBF file
        private void DeleteFromDBF(string ip, string port)
        {
            listView1.Items.Clear();
            try
            {
                // Ensure the correct DBF file path
                string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
                string directoryPath = Path.GetDirectoryName(dbfFilePath);

                // Create connection string for the DBF file
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Delete the row from the DBF file where the IP and Port match
                    string deleteCommandText = $"DELETE FROM {Path.GetFileNameWithoutExtension(dbfFilePath)} WHERE IPAddr = '{ip}' AND DPort = '{port}'";

                    using (OleDbCommand deleteCommand = new OleDbCommand(deleteCommandText, connection))
                    {
                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Selected item successfully deleted from the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Item not found in the database!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting from the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to reassign IDs after deletion to maintain sequence
        private void ReassignIDs()
        {
            try
            {
                // Ensure the correct DBF file path
                string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
                string directoryPath = Path.GetDirectoryName(dbfFilePath);

                // Create connection string for the DBF file
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Select all remaining records ordered by the current ID
                    string selectCommandText = $"SELECT * FROM {Path.GetFileNameWithoutExtension(dbfFilePath)} ORDER BY ID";
                    using (OleDbCommand selectCommand = new OleDbCommand(selectCommandText, connection))
                    {
                        using (OleDbDataReader reader = selectCommand.ExecuteReader())
                        {
                            int newId = 1;
                            while (reader.Read())
                            {
                                int currentId = int.Parse(reader["ID"].ToString());

                                // If the current ID does not match the newId, update it
                                if (currentId != newId)
                                {
                                    string updateCommandText = $"UPDATE {Path.GetFileNameWithoutExtension(dbfFilePath)} SET ID = '{newId}' WHERE ID = '{currentId}'";
                                    using (OleDbCommand updateCommand = new OleDbCommand(updateCommandText, connection))
                                    {
                                        updateCommand.ExecuteNonQuery();
                                    }
                                }
                                newId++;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reassigning IDs in the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                // Get the selected item from the ListView
                ListViewItem selectedItem = listView1.SelectedItems[0];

                // Get the values from textboxes
                string strDeviceName = deviceNameTextBox.Text;
                string strDeviceIP = ipTextBox.Text;
                string strDevicePort = portTextBox.Text;
                string strDevicePassword = pwTextBox.Text;


                if (string.IsNullOrEmpty(strDeviceName) || string.IsNullOrEmpty(strDeviceIP) ||
                    string.IsNullOrEmpty(strDevicePort) || string.IsNullOrEmpty(strDevicePassword))
                {
                    MessageBox.Show("Please fill in all the fields before editing.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if a device with the same IP and port already exists (duplicate check)
                if (CheckIfDeviceExists(strDeviceIP, strDevicePort))
                {
                    MessageBox.Show("A device with the same IP or port number already exists in the database.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Prevent updating the device
                }

                // Get the ID from the selected item (ID assumed to be the first subitem)
                string id = selectedItem.SubItems[0].Text;

                // Update the database
                UpdateDatabase(strDeviceName, strDeviceIP, strDevicePort, strDevicePassword, id);

                // Reload the ListView to show updated data
                LoadDBFDataToListView(listView1, @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf");

                // Clear the textboxes after editing
                deviceNameTextBox.Clear();
                ipTextBox.Clear();
                portTextBox.Clear();
                pwTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please select an item to edit.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Method to update the database with new values
        private void UpdateDatabase(string deviceName, string ip, string port, string password, string id)
        {
            listView1.Items.Clear();
            try
            {
                // Ensure the correct DBF file path
                string dbfFilePath = @"C:\FP_CLOCK 2\FP_CLOCK\FP_CLOCK\dBase\example.dbf";
                string directoryPath = Path.GetDirectoryName(dbfFilePath);

                // Create connection string for the DBF file
                string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + directoryPath + ";Extended Properties=dBase IV;";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    // Prepare the update command, excluding "Durum" and "SeriNo"
                    // Ensure numeric fields (like ID) are not in quotes, and string fields are in quotes.
                    string updateCommandText = $"UPDATE {Path.GetFileNameWithoutExtension(dbfFilePath)} " +
                                               $"SET DevName = '{deviceName}', " +
                                               $"IPAddr = '{ip}', " +
                                               $"DPort = '{port}', " +
                                               $"Pwd = '{password}' " +  // Remove the extra comma here
                                               $"WHERE ID = '{id}'";  // Assuming ID is a number, no quotes

                    using (OleDbCommand updateCommand = new OleDbCommand(updateCommandText, connection))
                    {
                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Device successfully updated in the database!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to update the device in the database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating the database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when double click on the listview item, the item should be editable
            if (listView1.SelectedItems.Count > 0)
            {
                selectedItem = listView1.SelectedItems[0];

                // Load the selected item's data into the textboxes
                deviceNameTextBox.Text = selectedItem.SubItems[1].Text;
                ipTextBox.Text = selectedItem.SubItems[2].Text;
                portTextBox.Text = selectedItem.SubItems[3].Text;
                pwTextBox.Text = selectedItem.SubItems[4].Text;
            }
        }
        
    }
}
