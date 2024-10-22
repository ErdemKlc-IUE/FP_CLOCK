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
        private ListViewItem selectedItem = null; // To hold the currently selected item for editing
        // Create an instance of the WelcomePage
        WelcomePage welcomePage = new WelcomePage();

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


            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("There are no items to save.");
                return;
            }
            

            // Send the ListView items to WelcomePage
            welcomePage.ReceiveListViewItems(listView1.Items); // yourListView is the ListView in SaveDevice


            MessageBox.Show("All devices have been successfully saved.");
            Visible = false;
            //this.Owner.Visible = true;
            welcomePage.Show();

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // Save the device to the listview
            string strDeviceName = deviceNameTextBox.Text;
            string strDeviceIP = ipTextBox.Text;
            string strDevicePort = portTextBox.Text;
            string strDevicePassword = pwTextBox.Text;

            if (string.IsNullOrWhiteSpace(strDeviceName) || string.IsNullOrWhiteSpace(strDeviceIP) ||
                string.IsNullOrWhiteSpace(strDevicePort) || string.IsNullOrWhiteSpace(strDevicePassword))
            {
                MessageBox.Show("Please fill in all the fields before adding.");
                return;
            }

            // Generate an ID based on the current number of items
            int id = listView1.Items.Count + 1;

            // Create a ListViewItem with the ID and device details
            ListViewItem item = new ListViewItem(id.ToString()); // ID as the first column
            item.SubItems.Add(strDeviceName);
            item.SubItems.Add(strDeviceIP);
            item.SubItems.Add(strDevicePort);
            item.SubItems.Add(strDevicePassword);
            item.SubItems.Add("Kapalı");

            // Add the item to the ListView
            listView1.Items.Add(item);

            // Optionally, clear the textboxes after adding the item
            deviceNameTextBox.Clear();
            ipTextBox.Clear();
            portTextBox.Clear();
            pwTextBox.Clear();
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
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            // Selected listview item should be deleted from listview
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (selectedItem != null)
            {
                // Update the selected item's data from the textboxes
                selectedItem.SubItems[1].Text = deviceNameTextBox.Text;
                selectedItem.SubItems[2].Text = ipTextBox.Text;
                selectedItem.SubItems[3].Text = portTextBox.Text;
                selectedItem.SubItems[4].Text = pwTextBox.Text;

                // Optionally, clear the textboxes after editing
                deviceNameTextBox.Clear();
                ipTextBox.Clear();
                portTextBox.Clear();
                pwTextBox.Clear();

                // Clear the reference to the selected item
                selectedItem = null;
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
