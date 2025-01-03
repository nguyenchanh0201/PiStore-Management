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
    public partial class frmHome : Form
    {
        public String username;
        public String role; 

        public frmHome(string username, string role)
        {
            InitializeComponent();
            UserControl ucDashboard = new ucDashboard();
            ShowUserControl(ucDashboard);
            this.username = username;
            this.role = role;
            lblUsername.Text = username;
            lblRole.Text = role;
        }

        private void resetButtonColor(Guna2Button btn, Color backColor, Color foreColor)
        {
            btn.BackColor = backColor;
            btn.ForeColor = foreColor;
        }

        private void ResetButtonColors()
        {
            resetButtonColor(btnDashboard, Color.Transparent, Color.SteelBlue);
            resetButtonColor(btnProducts, Color.Transparent, Color.SteelBlue);
            resetButtonColor(btnClients, Color.Transparent, Color.SteelBlue);
            resetButtonColor(btnEmploy, Color.Transparent, Color.SteelBlue);
            resetButtonColor(btnOrders, Color.Transparent, Color.SteelBlue);
            resetButtonColor(btnBill, Color.Transparent, Color.SteelBlue);
        }

        private void ShowUserControl(UserControl control)
        {
            guna2Panel2.Controls.Clear();
            control.Dock = DockStyle.Fill;
            guna2Panel2.Controls.Add(control);
        }



        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            resetButtonColor(btnDashboard, Color.LightSkyBlue, Color.White);
            lblPage.Text = "Dashboard";
            UserControl ucDashboard = new ucDashboard();
            ShowUserControl(ucDashboard);
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            resetButtonColor(btnProducts, Color.LightSkyBlue, Color.White);
            lblPage.Text = "Products";
            UserControl ucProduct = new ucProducts();
            ShowUserControl(ucProduct);
            

        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            resetButtonColor(btnClients, Color.LightSkyBlue, Color.White);
            lblPage.Text = "Clients";
            UserControl ucClients = new ucClients();
            ShowUserControl(ucClients);
        }

        private void btnEmploy_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            resetButtonColor(btnEmploy, Color.LightSkyBlue, Color.White);
            lblPage.Text = "Employees";
            UserControl ucEmployees = new ucEmployees();
            ShowUserControl(ucEmployees);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            resetButtonColor(btnOrders, Color.LightSkyBlue, Color.White);
            lblPage.Text = "Orders";
            var user = BUS.BUSUser.getUserByUsername(username);
            UserControl ucOrders = new ucOrders(user.UserID);
            ShowUserControl(ucOrders);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {

                Application.Restart();
            }
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            ResetButtonColors();
            resetButtonColor(btnBill, Color.LightSkyBlue, Color.White);
            lblPage.Text = "Bills";
            UserControl ucBills = new ucBills() ; 
            ShowUserControl(ucBills);
        }
    }
}
