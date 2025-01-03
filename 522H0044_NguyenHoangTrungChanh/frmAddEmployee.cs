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
    
    public partial class frmAddEmployee : Form
    {
        private int choice = 1 ; 
        private Employee _employee;
        private ucEmployees _parentForm; 
        public frmAddEmployee(ucEmployees parentForm)
        {
            InitializeComponent();
            txtRole.Items.Add("Admin");
            txtRole.Items.Add("Employee");
            _parentForm = parentForm; 
        }

        public frmAddEmployee(Employee employee, ucEmployees parentForm)
        {
            InitializeComponent();
            _employee = employee;
            fillData(_employee);
            _parentForm = parentForm;
            choice = 2; 

        }

        private void fillData(Employee employee)
        {
            //Employee Info
            txtPhone.Text = employee.Phone;
            txtAddress.Text = employee.Address;
            txtEmail.Text = employee.Email;
            txtName.Text = employee.Name;
            txtSalary.Value = employee.Salary;
            txtHireDate.Value = employee.HireDate;

            //User info
            User user = BUS.BUSUser.getUserByUserID(employee.UserID);
            txtUsername.Text = user.Username;
            txtRole.Items.Add("Admin");
            txtRole.Items.Add("Employee");
            txtRole.SelectedItem = user.Role;

            //Disable boxes
            txtRole.Enabled = false;
            txtUsername.Enabled = false; 
            txtHireDate.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String address = txtAddress.Text;
            String name = txtName.Text;
            String email = txtEmail.Text;
            decimal salary = txtSalary.Value;
            DateTime hireDate = txtHireDate.Value;
            String phone = txtPhone.Text;
            if (phone.Length > 11)
            {
                MessageBox.Show("Invalid phone Number");
            }
            else
            {
                if (choice == 1)
                {
                    //if (BUS.BUSEmployee.Instance)
                    String username = txtUsername.Text;

                    //Check for username 
                    var usercheck = BUS.BUSUser.getUserByUsername(username);
                    if (usercheck == null)
                    {

                    }
                    else
                    {

                    }
                    String role = txtRole.Text;
                    Console.WriteLine(role);

                    if (BUS.BUSUser.addUser(username, "123456", role))
                    {
                        //Get user by username 
                        User user = BUS.BUSUser.getUserByUsername(username);
                        if (BUS.BUSEmployee.AddEmployee(user.UserID, name, email, phone, address, salary, hireDate))
                        {
                            //Message Adding completed
                            MessageBox.Show("Employee added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                            _parentForm.loadGRD();
                        }
                        else
                        {
                            MessageBox.Show("Employee added failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Creating user failed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                }
                else
                {
                    if (BUS.BUSEmployee.UpdateEmployee(_employee.eID, name, email, phone, address, salary, hireDate))
                    {
                        //Message Updating completed and move back 
                        MessageBox.Show("Employee updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                        _parentForm.loadGRD();
                    }
                    else
                    {
                        MessageBox.Show("Employee updated failed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            
            
            
            //choice = 1 -> adding 
            // choice = 2 -> updating
            


        }
    }
}
