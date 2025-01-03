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
    public partial class frmAddOrder : Form
    {
        private ucOrders _parentForm; 
        private Employee _employee;
        public frmAddOrder(ucOrders parentForm, Employee employee)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _employee = employee;
            loadProduct();
            loadOrderInfo();
            loadCustomers();
            
        }

        private void loadOrderInfo()
        {
            String orderID = PISTORE.Instance.GenerateOrderID().FirstOrDefault();
            txtOrderID.Text = orderID;
            txtOrderID.Enabled = false; 
            txtEmployID.Enabled = false;
            txtTotal.Enabled = false;
            txtDate.Enabled = false;
            txtEmployID.Text = _employee.eID.ToString();
            txtDate.Value = DateTime.Now;

        }

        private Dictionary<string, int> quantities = new Dictionary<string, int>(); 
        private void addToCart(PRODUCT product)
        {
            if (quantities.ContainsKey(product.pID))
            {
                quantities[product.pID] += 1; 
            }
            else
            {
                quantities[product.pID] = 1; 
            }

            UpdateCartUI();
            updateTotalUI();
        }

        private void UpdateCartUI()
        {
            cartPanel.Controls.Clear(); 

            foreach (var item in quantities)
            {
                string productID = item.Key;
                PRODUCT product =BUS.BUSProducts.GetProductByID(productID);
                String productName = product.NAME;
                int quantity = item.Value;


                var cartItemPanel = new Guna2Panel
                {
                    Height = 60,
                    Width = 410,
                    BackColor = Color.White,
                    Margin = new Padding(5),
                    BorderRadius = 23,
                };

                var productNameLabel = new Label
                {
                    Text = productName, 
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    ForeColor = Color.Black,
                    Location = new Point(10, 10),
                    AutoSize = true
                };

                var quantityLabel = new Label
                {
                    Text = quantity.ToString(), 
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Black,
                    Location = new Point(200, 10),
                    AutoSize = true
                };

                var elipse = new Guna2Elipse
                {
                    TargetControl = cartItemPanel,
                };

                var increaseButton = new Button
                {
                    Text = "+",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(240, 10),
                    Size = new Size(30, 30)
                };

                increaseButton.Click += (sender, e) =>
                {
                    if (!validateQuantity(quantities[productID] + 1 , product.QUANTITY))
                    {
                        MessageBox.Show("Out of Stock");
                    }
                    else
                    {
                        quantities[productID] += 1;
                        quantityLabel.Text = quantities[productID].ToString();
                        updateTotalUI();
                    }
                };

                var decreaseButton = new Button
                {
                    Text = "-",
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    Location = new Point(150, 10),
                    Size = new Size(30, 30)
                };

                decreaseButton.Click += (sender, e) =>
                {
                    if (quantities[productID] > 1) 
                    {
                        quantities[productID] -= 1; 
                        quantityLabel.Text = quantities[productID].ToString();
                        updateTotalUI();
                    }
                };

                var deleteButton = new Button
                {
                    Font = new Font("Segoe UI", 10F),
                    Location = new Point(280, 10),
                    Size = new Size(70, 30),
                    BackgroundImage = Properties.Resources.trash,
                    BackgroundImageLayout = ImageLayout.Zoom,
                };

                deleteButton.Click += (sender, e) =>
                {
                    quantities.Remove(productID); 
                    UpdateCartUI();
                    updateTotalUI();
                };

                cartItemPanel.Controls.Add(productNameLabel);
                cartItemPanel.Controls.Add(quantityLabel);
                cartItemPanel.Controls.Add(increaseButton);
                cartItemPanel.Controls.Add(decreaseButton);
                cartItemPanel.Controls.Add(deleteButton);

                cartPanel.Controls.Add(cartItemPanel);
                updateTotalUI();
            }
        }

        private void loadCustomers()
        {
            var clients = BUS.BUSClients.GetCLIENTs();
            foreach (var client in clients)
            {
                cbCustomer.Items.Add(client.cID + "." + client.name);
            }
        }

        private void loadProduct()
        {
            var products = BUS.BUSProducts.getProducts();


            productPanel.Controls.Clear();

            foreach (var product in products)
            {
                if (product.QUANTITY < 1)
                {
                    continue;
                }
                else
                {
                    var productButton = new Guna.UI2.WinForms.Guna2Button
                    {
                        Height = 130,
                        Width = 450,
                        FillColor = Color.White,
                        BorderRadius = 23,
                        Margin = new Padding(10)
                    };


                    var nameLabel = new Label
                    {
                        Text = product.NAME,
                        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                        ForeColor = Color.Black,
                        Location = new Point(10, 10),
                        BackColor = Color.Transparent,
                        AutoSize = true
                    };


                    var quantityLabel = new Label
                    {
                        Text = $"{product.QUANTITY} left",
                        Font = new Font("Segoe UI", 14F),
                        ForeColor = Color.Black,
                        Location = new Point(10, 40),
                        BackColor = Color.Transparent,
                        AutoSize = true
                    };

                    // Create a label for the price
                    var priceLabel = new Label
                    {
                        Text = $"{product.PRICE} VND",
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.Black,
                        Location = new Point(10, 100),
                        BackColor = Color.Transparent,
                        AutoSize = true

                    };


                    productButton.Image = Properties.Resources.box;
                    productButton.ImageSize = new Size(80, 80);
                    productButton.ImageAlign = HorizontalAlignment.Right;


                    productButton.Click += (sender, e) =>
                    {
                        MessageBox.Show($"Product ID: {product.pID}\nName: {product.NAME}");
                        addToCart(product);
                    };


                    productButton.Controls.Add(nameLabel);
                    productButton.Controls.Add(quantityLabel);
                    productButton.Controls.Add(priceLabel);


                    productPanel.Controls.Add(productButton);
                }

            }
        }

        private decimal calculateTotal(Dictionary<String, int> quantities )
        {
            decimal total = 0;


            foreach (var quant in quantities)
            {
                string productID = quant.Key;
                int quantity = quant.Value;

                var product = BUS.BUSProducts.GetProductByID(productID);

                if (product != null)
                {
                    decimal price = product.PRICE;
                    total += price * quantity ;
                }

            }
            return total; 

        }

        private void updateTotalUI()
        {
            txtTotal.Text = calculateTotal(quantities).ToString();
        }

        private bool validateQuantity(int quantityCart, int quantity)
        {
            return quantityCart <= quantity;
        }

        private void addOrderItems(Dictionary<String, int> quantities, String orderID)
        {
            foreach (var quant in quantities)
            {
                var productID = quant.Key;
                var quantity = quant.Value;
                if (BUS.BUSOrderItem.AddItem(orderID, productID, quantity))
                {
                    MessageBox.Show("Adding items success");
                    //Update productQuantity
                    bool status = BUS.BUSProducts.UpdateProductQuantity(productID, quantity);
                    if (status)
                    {
                        MessageBox.Show("Update product success");
                    }
                    else
                    {
                        MessageBox.Show("Update product failed");
                    }
                }
                else
                {
                    MessageBox.Show("Adding items failed");
                }
            }
        }

        private String getCustomerID(String customerInfo)
        {
            //Split string by "."
            //Get the first one str[0]
            String [] customerArr = customerInfo.Split('.');

            return customerArr[0];

        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            //Adding order
            // Decrease quantity in product using updateProduct
            //Adding orderItem 
            //Validate 
            
            //Check if customerSelected
            if (cbCustomer.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a customer");
            } else
            {
                String orderID = txtOrderID.Text;
                DateTime dateTime = txtDate.Value;
                String employeeID = txtEmployID.Text;
                String customerID = getCustomerID(cbCustomer.Text);
                decimal total = Decimal.Parse(txtTotal.Text);
                

                bool status = BUS.BUSOrders.AddOrder(employeeID, customerID, dateTime, total);

                if (status)
                {
                    MessageBox.Show("Adding order success");
                    addOrderItems(quantities, orderID );
                    this.Close();
                    _parentForm.loadGRD();

                }
                else
                {
                    MessageBox.Show("Adding order failed");
                }

            }


            
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do the searching
                if (txtSearch.Text == "")
                {
                    loadProduct();
                }
                else
                {
                    List<PRODUCT> foundProducts = BUS.BUSProducts.FindProductsByName(txtSearch.Text);
                    loadProductByName(foundProducts);
                }
            }
        }


        private void loadProductByName(List<PRODUCT> products)
        {
            


            productPanel.Controls.Clear();

            foreach (var product in products)
            {
                if (product.QUANTITY < 1)
                {
                    continue;
                }
                else
                {
                    var productButton = new Guna.UI2.WinForms.Guna2Button
                    {
                        Height = 130,
                        Width = 450,
                        FillColor = Color.White,
                        BorderRadius = 23,
                        Margin = new Padding(10)
                    };


                    var nameLabel = new Label
                    {
                        Text = product.NAME,
                        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                        ForeColor = Color.Black,
                        Location = new Point(10, 10),
                        BackColor = Color.Transparent,
                        AutoSize = true
                    };


                    var quantityLabel = new Label
                    {
                        Text = $"{product.QUANTITY} left",
                        Font = new Font("Segoe UI", 14F),
                        ForeColor = Color.Black,
                        Location = new Point(10, 40),
                        BackColor = Color.Transparent,
                        AutoSize = true
                    };

                    // Create a label for the price
                    var priceLabel = new Label
                    {
                        Text = $"{product.PRICE} VND",
                        Font = new Font("Segoe UI", 12F),
                        ForeColor = Color.Black,
                        Location = new Point(10, 100),
                        BackColor = Color.Transparent,
                        AutoSize = true

                    };


                    productButton.Image = Properties.Resources.box;
                    productButton.ImageSize = new Size(80, 80);
                    productButton.ImageAlign = HorizontalAlignment.Right;


                    productButton.Click += (sender, e) =>
                    {
                        MessageBox.Show($"Product ID: {product.pID}\nName: {product.NAME}");
                        addToCart(product);
                    };


                    productButton.Controls.Add(nameLabel);
                    productButton.Controls.Add(quantityLabel);
                    productButton.Controls.Add(priceLabel);


                    productPanel.Controls.Add(productButton);
                }
                
            }
        }
    }
}
