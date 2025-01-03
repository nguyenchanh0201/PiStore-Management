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
    public partial class frmLogin : Form
    {
        private Boolean isChecked = false;
        public frmLogin()
        {
            InitializeComponent();
            txtPwd.UseSystemPasswordChar = true; 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String username = txtUser.Text;
            String password = txtPwd.Text;

            if (username == "" || password == "")
            {
                MessageBox.Show("Please enter username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (BUS.BUSLogin.Instance.checkLogin(username, password))
                {
                    MessageBox.Show("Login successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    var user = BUS.BUSUser.getUserByUsername(username);


                    this.Hide();
                    frmHome frmHome = new frmHome(user.Username, user.Role);
                    frmHome.ShowDialog();
                    this.Close();
                    //frmUser frmUser = new frmUser();
                    //frmUser.ShowDialog();
                    //this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }
        

        private void txtPwd_IconRightClick(object sender, EventArgs e)
            {
            if (!isChecked)
            {
                txtPwd.UseSystemPasswordChar = false;
                txtPwd.IconRight = Properties.Resources.hide;
                isChecked = true;
            }
            else
            {
                txtPwd.UseSystemPasswordChar = true;
                txtPwd.IconRight = Properties.Resources.view;
                isChecked = false;
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
