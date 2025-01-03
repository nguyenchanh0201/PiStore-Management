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
    public partial class ucBills : UserControl
    {
        
        public ucBills()
        {
            InitializeComponent();
            loadGRD();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do the searching
                Console.WriteLine("Search");
                if (txtSearch.Text == "")
                {
                    loadGRD();
                }
                else
                {
                    var bill = BUS.BUSBill.GetBillByID(txtSearch.Text);
                    guna2DataGridView1.Rows.Clear();

                    
                        int rowIndex = guna2DataGridView1.Rows.Add();

                        guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = bill.bID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgOID"].Value = bill.oID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgcID"].Value = bill.cID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgeID"].Value = bill.eID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgDate"].Value = bill.BILLDATE;


                    
                }
            }
        }

        public void loadGRD()
        {
            var bills = BUS.BUSBill.getAllBills();
            guna2DataGridView1.Rows.Clear();

            foreach (var bill in bills)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();

                guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = bill.bID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgOID"].Value = bill.oID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgcID"].Value = bill.cID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgeID"].Value = bill.eID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgDate"].Value = bill.BILLDATE;


            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save to CSV";
                saveFileDialog.FileName = "BillsExport.csv";

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
                    // Write headers, excluding the last column
                    for (int i = 0; i < gridView.Columns.Count - 1; i++)
                    {
                        writer.Write(gridView.Columns[i].HeaderText);
                        if (i < gridView.Columns.Count - 2)
                        {
                            writer.Write(",");
                        }
                    }
                    writer.WriteLine();

                    // Write data rows, excluding the last column
                    foreach (DataGridViewRow row in gridView.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            for (int j = 0; j < gridView.Columns.Count - 1; j++)
                            {
                                writer.Write(row.Cells[j].Value?.ToString().Replace(",", " "));
                                if (j < gridView.Columns.Count - 2)
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

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["dvgDel"].Index)
            {
             
                String billID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();

                DialogResult result = MessageBox.Show($"Are you sure you want to delete the bill '{billID}'?",
                                               "Confirm Delete",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool status = BUS.BUSBill.RemoveBill(billID);
                    if (status)
                    {
                        MessageBox.Show($"Bill '{billID}' has been deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadGRD();
                    }
                    else
                    {
                        MessageBox.Show($"Failed to delete bill '{billID}'.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }
    }
}
