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
using BUS;
using DTO;

namespace _522H0044_NguyenHoangTrungChanh
{
    public partial class ucProducts : UserControl
    {
        public ucProducts()
        {
            InitializeComponent();
            loadGRD();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do the searching
                if (txtSearch.Text == "")
                {
                    loadGRD();
                } else
                {
                    List<PRODUCT> foundProducts = BUS.BUSProducts.FindProductsByName(txtSearch.Text);
                    guna2DataGridView1.Rows.Clear();

                    foreach (var product in foundProducts)
                    {
                        int rowIndex = guna2DataGridView1.Rows.Add();
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = product.pID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = product.NAME;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgDes"].Value = product.DESCRIPTION;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgPrice"].Value = product.PRICE;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgQuantity"].Value = product.QUANTITY;
                    }
                }
            }
        }
        public void loadGRD()
        {   
            var products = BUS.BUSProducts.getProducts();
            guna2DataGridView1.Rows.Clear();

            foreach (var product in products)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();

                guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1; 
                guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = product.pID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = product.NAME;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgDes"].Value = product.DESCRIPTION;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgPrice"].Value = product.PRICE;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgQuantity"].Value = product.QUANTITY;
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddProduct frmAddProduct = new frmAddProduct(this);
            frmAddProduct.ShowDialog();
            
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["dvgDel"].Index)
            {
                String productName = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString();
                String pID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();

                DialogResult result = MessageBox.Show($"Are you sure you want to delete the product '{productName}'?",
                                               "Confirm Delete",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    bool status = BUS.BUSProducts.removeProduct(pID);
                    if (status)
                    {
                        MessageBox.Show($"Product '{productName}' has been deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadGRD();
                    } else
                    {
                        MessageBox.Show($"Failed to delete Product '{productName}'.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
            }
            else if (e.ColumnIndex == guna2DataGridView1.Columns["dvgEdit"].Index)
            {
                MessageBox.Show("Edit clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString());
                String productID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();
                PRODUCT product = BUS.BUSProducts.GetProductByID(productID);
                frmAddProduct frmAddProduct = new frmAddProduct(this, product);
                frmAddProduct.ShowDialog();
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save to CSV";
                saveFileDialog.FileName = "Product   sExport.csv";

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
    }
}
