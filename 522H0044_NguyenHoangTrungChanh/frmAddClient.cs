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
    public partial class frmAddClient : Form
    {
        private CLIENT _client;
        private ucClients _parentForm; 
        private int choice = 1;
        public frmAddClient(ucClients parentForm)
        {
            InitializeComponent();
            _parentForm = parentForm;
        }

        public frmAddClient(CLIENT client, ucClients parentForm)
        {
            InitializeComponent();
            _client = client;
            fillData(_client);
            _parentForm = parentForm;
            choice = 2;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Adding 
            String customerName = txtName.Text;
            String email = txtEmail.Text;
            String phone = txtPhone.Text;
            String address = txtAddress.Text;
            if (phone.Length > 11)
            {
                MessageBox.Show("Invalid phone Number");
            }
            else
            {
                if (choice == 1)
                {
                    bool status = BUS.BUSClients.addClient(customerName, email, phone, address);
                    if (status)
                    {
                        MessageBox.Show("Adding client success");
                        this.Close();
                        _parentForm.loadGRD();
                    }
                    else
                    {
                        MessageBox.Show("Failed to add client");
                        this.Close();
                    }
                }
                else
                {
                    bool status = BUS.BUSClients.updateClient(_client.cID, customerName, email, phone, address);

                    if (status)
                    {
                        MessageBox.Show("Update client success");
                        this.Close();
                        _parentForm.loadGRD();

                    }
                    else
                    {
                        MessageBox.Show("Update client failed");
                    }
                }
            }
            


            
        }

        private void fillData(CLIENT client)
        {
            txtName.Text = client.name;
            txtEmail.Text = client.email;
            txtPhone.Text = client.phone;
            txtAddress.Text = client.address;

        }
    }
}


