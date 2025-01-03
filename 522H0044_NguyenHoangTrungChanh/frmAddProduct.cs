using DTO;
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
    public partial class frmAddProduct : Form
    {
        private int choice = 1;
        private ucProducts _parentForm;
        private PRODUCT _product;

        public frmAddProduct(ucProducts parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
           
        }

        public frmAddProduct(ucProducts parentForm, PRODUCT product)
        {
            InitializeComponent();
            _parentForm = parentForm;
            _product = product;
            fillData(_product);
            choice = 2;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String productName = txtName.Text;
            String description = txtDesc.Text;
            decimal price = txtPrice.Value;
            int quantity = (int) txtQuantity.Value;

            if (quantity < 1)
            {
                MessageBox.Show("Product quantity should be higher than 1 !", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (choice == 1)
                {
                    if (BUS.BUSProducts.addProduct(productName, description, price, quantity))
                    {
                        MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        _parentForm.loadGRD();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
                else
                {
                    if (BUS.BUSProducts.updateProduct(_product.pID, productName, description, price, quantity))
                    {
                        MessageBox.Show("Product updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        _parentForm.loadGRD();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
            }
            
            
            

        }
        private void fillData(PRODUCT product) {
            txtName.Text = product.NAME;
            txtDesc.Text = product.DESCRIPTION;
            txtPrice.Value = product.PRICE;
            txtQuantity.Value = product.QUANTITY;
        }
    }
}
