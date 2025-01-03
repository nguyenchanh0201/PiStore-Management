using DTO;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Xml.Linq;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Font = System.Drawing.Font;


namespace _522H0044_NguyenHoangTrungChanh
{
    public partial class ucOrders : UserControl
    {
        private String userID;
        public ucOrders(string userID)
        {
            InitializeComponent();
            loadGRD();
            this.userID = userID;
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
                    var order = BUS.BUSOrders.GetORDERByID(txtSearch.Text);
                    guna2DataGridView1.Rows.Clear();

                    
                    int rowIndex = guna2DataGridView1.Rows.Add();
                    guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                    guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = order.oID;
                    guna2DataGridView1.Rows[rowIndex].Cells["dvgCID"].Value = order.cID;
                    guna2DataGridView1.Rows[rowIndex].Cells["dvgEID"].Value = order.eID;
                    guna2DataGridView1.Rows[rowIndex].Cells["dvgOrderDate"].Value = order.ORDERDATE;
                    guna2DataGridView1.Rows[rowIndex].Cells["dvgTotal"].Value = order.TOTAL;

                }
            }
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save to CSV";
                saveFileDialog.FileName = "OrdersExport.csv";

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
                            for (int j = 0; j < gridView.Columns.Count - 3; j++)
                            {
                                writer.Write(row.Cells[j].Value?.ToString().Replace(",", " "));
                                if (j < gridView.Columns.Count - 4)
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

        public void loadGRD()
        {
            var orders = BUS.BUSOrders.GetORDERs();
            guna2DataGridView1.Rows.Clear();
            foreach (var order in orders)
            {
                int rowIndex = guna2DataGridView1.Rows.Add();

                guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = order.oID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgCID"].Value = order.cID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgEID"].Value = order.eID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgOrderDate"].Value = order.ORDERDATE;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgTotal"].Value = order.TOTAL;
            }
        }




        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            String orderID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();
            if (e.ColumnIndex == guna2DataGridView1.Columns["dvgDel"].Index)
            {
                MessageBox.Show("Delete clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString());
                //Yes, no 
                
                DialogResult result = MessageBox.Show($"Are you sure you want to delete the order '{orderID}'? This will delete all items in the order",
                                               "Confirm Delete",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    // Empty the orderItems first
                    bool status = BUS.BUSOrderItem.EmptyOrderItems(orderID);

                    if (status)
                    {
                        MessageBox.Show("Delete Items success");
                        if (BUS.BUSOrders.RemoveOrder(orderID))
                        {
                            MessageBox.Show("Delete Order success");
                            loadGRD();
                        }
                        else
                        {
                            MessageBox.Show("Delete Order failed");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Delete Items failed");
                    }
                    //Delete order
                }


            }
            else if (e.ColumnIndex == guna2DataGridView1.Columns["dvgEdit"].Index)
            {
                MessageBox.Show("Edit clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString());
                //Display frmEditOrder with Order and all orderItems inside
                var order = BUS.BUSOrders.GetORDERByID(orderID);
                frmOrderDetails frmOrderDetails = new frmOrderDetails(order);
                frmOrderDetails.ShowDialog();
            }
            else if ((e.ColumnIndex == guna2DataGridView1.Columns["dvgPrintBill"].Index))
            {
                MessageBox.Show("Print clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString());
                //Display frmPrintbill with all infomation of Bills and button Print()
                //Get Bill by OrderID
                var order = BUS.BUSOrders.GetORDERByID(orderID);
                bool status = BUS.BUSBill.AddBill(orderID, order.cID, order.eID);

                if (status)
                {
                    MessageBox.Show("Create Bill Completed");
                    //printBills();
                    //Open dialog to save the file as pdf
                    printBills(orderID);
                }
                else
                {
                    MessageBox.Show("Error creating bill");
                }

            }
        }

       


        private void btnAdd_Click(object sender, EventArgs e)
        {
            var employee = BUS.BUSEmployee.GetEmployeeByUserID(userID);
            frmAddOrder frmAddOrder = new frmAddOrder(this, employee);
            frmAddOrder.ShowDialog();
        }


        private void printBills(string orderID)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                // Customize your printing logic here based on the image layout
                Graphics g = e.Graphics;
                System.Drawing.Font font = new Font("Arial", 12);
                System.Drawing.Font titleFont = new Font("Arial", 18, FontStyle.Bold);
                System.Drawing.Font orderFont = new Font("Arial", 14, FontStyle.Bold);
                float lineHeight = font.GetHeight();
                float titleHeight = titleFont.GetHeight();
                float x = 100;
                float y = 100;

                System.Drawing.Image logo = Properties.Resources.icons8_coffee_shop_64;
                g.DrawImage(logo, x, y, 100, 50);
                y += titleHeight;
                y += titleHeight;


                Pen dashPen = new Pen(Color.Black) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                g.DrawLine(dashPen, x, y, x + 500, y);

                // Print the title
                g.DrawString("Receipt", titleFont, Brushes.Black, x, y);
                y += titleHeight;

                // Print the dashed line
                g.DrawLine(dashPen, x, y, x + 500, y);
                y += lineHeight;

                g.DrawString("Order ID: " + orderID, orderFont, Brushes.Black, x, y);
                y += titleHeight;

                var items = BUS.BUSOrderItem.GetORDERITEMsByID(orderID);
                foreach (var item in items)
                {
                    var product = BUS.BUSProducts.GetProductByID(item.pID);
                    decimal totalPrice = item.QUANTITY * product.PRICE;
                    g.DrawString($"{item.QUANTITY}x {product.NAME}", font, Brushes.Black, x, y);
                    g.DrawString($"${totalPrice:F2}", font, Brushes.Black, x + 400, y);
                    y += lineHeight;
                }
                var order = BUS.BUSOrders.GetORDERByID(orderID);
                // Print the total amount
                y += lineHeight;
                g.DrawString("TOTAL AMOUNT", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x, y);
                g.DrawString($"${order.TOTAL:F2}", new Font("Arial", 14, FontStyle.Bold), Brushes.Black, x + 400, y);
                y += lineHeight;
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }




    }
}
