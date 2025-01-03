using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _522H0044_NguyenHoangTrungChanh
{
    public partial class ucDashboard : UserControl
    {
        private int numOrders; 
        public ucDashboard()
        {
            InitializeComponent();
            loadInfo();
            loadBestSellers();
        }

        private void loadInfo()
        {
            lblRevenue.Text = calculateRevenue().ToString();
            lblOrders.Text = numOrders.ToString();
            var clients = BUS.BUSClients.GetCLIENTs();
            lblClients.Text = clients.Count.ToString();

        }

        private decimal calculateRevenue()
        {
            decimal result = 0;
            var orders = BUS.BUSOrders.GetORDERs();
            numOrders = orders.Count;

            foreach (var order in orders)
            {
                result += order.TOTAL;
            }

            return result;
        }

        private void loadBestSellers()
        {
            Dictionary<string, int> bestSellers = new Dictionary<string, int>();

            var items = BUS.BUSOrderItem.getAllItems();

            foreach (var item in items)
            {
                if (!bestSellers.ContainsKey(item.pID))
                {
                    bestSellers.Add(item.pID, item.QUANTITY);
                }
                else
                {
                    bestSellers[item.pID] += item.QUANTITY;
                }
            }

            var sortedBestSellers = bestSellers.OrderByDescending(entry => entry.Value).ToList();

            guna2DataGridView1.Rows.Clear();
            foreach (var entry in sortedBestSellers)
            {
                var product = BUS.BUSProducts.GetProductByID(entry.Key);
                int rowIndex = guna2DataGridView1.Rows.Add();
                guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = product.pID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = product.NAME;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgDes"].Value = product.DESCRIPTION;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgPrice"].Value = product.PRICE;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgQuantity"].Value = entry.Value;
            }

            

        }
    }
}
