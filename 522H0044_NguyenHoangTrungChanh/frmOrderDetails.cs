using DTO;
using Guna.UI2.WinForms;
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
    public partial class frmOrderDetails : Form
    {
        private static ORDER _order;
        public frmOrderDetails(ORDER order)
        {
            InitializeComponent();
            _order = order;
            txtTotal.Enabled = false; 
            txtOrderID.Enabled = false;
            txtDate.Enabled = false;
            txtEmployID.Enabled = false;
            cbCustomer.Enabled = false;
            loadOrderInfo();
            loadItemsUI();
        }

        private void loadOrderInfo()
        {
            txtOrderID.Text = _order.oID; 
            txtEmployID.Text = _order.eID;
            txtDate.Value = _order.ORDERDATE;
            txtTotal.Text = _order.TOTAL.ToString();
            cbCustomer.Items.Add(loadCustomerName(_order.cID));
            cbCustomer.SelectedItem = loadCustomerName(_order.cID);
        } 

        private String loadCustomerName (String customerID)
        {
            return BUS.BUSClients.GetClientsByID(customerID).name;
        }

        private void loadItemsUI()
        {
            var items = BUS.BUSOrderItem.GetORDERITEMsByID(_order.oID);
            cartPanel.Controls.Clear(); // Clear existing items before loading new ones

            foreach (var item in items)
            {
                String productName = BUS.BUSProducts.GetProductByID(item.pID).NAME;

                var cartItemPanel = new Guna2Panel
                {
                    Height = 60,
                    Width = 430,
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderRadius = 23,
                };

                var elipse = new Guna2Elipse
                {
                    TargetControl = cartItemPanel,
                };

                var productImgBox = new Guna2PictureBox
                {
                    Height = 40,
                    Width = 40,
                    Location = new Point(10, 10), // Positioned at the top left corner
                    Image = Properties.Resources.box, // Make sure to set an actual image
                    SizeMode = PictureBoxSizeMode.Zoom // Ensure the image fits well
                };

                var productNameLabel = new Label
                {
                    Text = productName,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(60, 10), // Adjusted position to the right of the image
                    AutoSize = true
                };

                var quantityLabel = new Label
                {
                    Text = "Quantity: " + item.QUANTITY.ToString(),
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Black,
                    Location = new Point(60, 35), // Adjusted position below the product name
                    AutoSize = true
                };

                // Add controls to the panel
                cartItemPanel.Controls.Add(productImgBox);
                cartItemPanel.Controls.Add(productNameLabel);
                cartItemPanel.Controls.Add(quantityLabel);

                // Add the cart item panel to the cart panel
                cartPanel.Controls.Add(cartItemPanel);
            }
        }
    }
}
