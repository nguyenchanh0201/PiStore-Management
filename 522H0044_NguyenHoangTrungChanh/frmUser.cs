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
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            String username = guna2TextBox1.Text;
            String role = guna2TextBox2.Text;
            label1.Text = username + " " + role;
            
            if (BUS.BUSUser.addUser(username, "123456", role))
            {
                MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Employee added failed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            
        }
    }
}
