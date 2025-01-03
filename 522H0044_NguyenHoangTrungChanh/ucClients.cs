using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _522H0044_NguyenHoangTrungChanh
{
    public partial class ucClients : UserControl
    {
        public ucClients()
        {
            InitializeComponent();
            loadGRD();
        }

        public void loadGRD()
        {
            var clients = BUS.BUSClients.GetCLIENTs();
            guna2DataGridView1.Rows.Clear();
            foreach (var client in clients)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();

                guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = client.cID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = client.name;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgEmail"].Value = client.email;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgPhone"].Value = client.phone;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgAddress"].Value = client.address;
                
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["dvgDel"].Index)
            {
                MessageBox.Show("Delete clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString());
                String clientId = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();

                String clientName = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString();

                DialogResult result = MessageBox.Show($"Are you sure you want to delete the client '{clientName}'?",
                                               "Confirm Delete",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (BUS.BUSClients.deleteClient(clientId))
                    {
                        MessageBox.Show("Delete client success");
                        loadGRD();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete client");
                    }
                }
            }
            else if (e.ColumnIndex == guna2DataGridView1.Columns["dvgEdit"].Index)
            {
                MessageBox.Show("Edit clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString());
                String editClientID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();
                CLIENT editClient = BUS.BUSClients.GetClientsByID(editClientID);
                frmAddClient frmAddClient = new frmAddClient(editClient, this);
                frmAddClient.ShowDialog();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddClient frmAddClient = new frmAddClient(this);
            frmAddClient.ShowDialog();
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save to CSV";
                saveFileDialog.FileName = "ClientsExport.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportDataGridViewToCSV(guna2DataGridView1, saveFileDialog.FileName);
                }
            }
        }

        private void ExportDataGridViewToCSV(DataGridView gridView, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {

                    for (int i = 0; i < gridView.Columns.Count - 2; i++)
                    {
                        writer.Write(gridView.Columns[i].HeaderText);
                        if (i < gridView.Columns.Count - 3)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();


                    foreach (DataGridViewRow row in gridView.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int j = 0; j < gridView.Columns.Count - 2; j++)
                            {
                                writer.Write(row.Cells[j].Value?.ToString().Replace(",", " "));
                                if (j < gridView.Columns.Count - 3)
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.WriteLine();
                        }
                    }
                }

                MessageBox.Show("Data exported successfully to CSV!", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do the searching
                if (txtSearch.Text == "")
                {
                    loadGRD();
                }
                else
                {
                    var clients = BUS.BUSClients.GetClientsByName(txtSearch.Text);
                    guna2DataGridView1.Rows.Clear();
                    foreach (var client in clients)
                    {
                        int rowIndex = guna2DataGridView1.Rows.Add();

                        guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = client.cID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = client.name;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgEmail"].Value = client.email;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgPhone"].Value = client.phone;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgAddress"].Value = client.address;

                    }
                }
            }
        }
    }
}
