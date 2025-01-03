using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _522H0044_NguyenHoangTrungChanh
{
    public partial class ucEmployees : UserControl
    {
        public ucEmployees()
        {
            InitializeComponent();
            loadGRD();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //Do the searching
                if (txtSearch.Text == "")
                {
                    loadGRD();
                }
                else
                {
                    var employees = BUS.BUSEmployee.GetEmployeesByName(txtSearch.Text);
                    guna2DataGridView1.Rows.Clear();
                    foreach (var employee in employees)
                    {

                        int rowIndex = guna2DataGridView1.Rows.Add();

                        guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = employee.eID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgUserId"].Value = employee.UserID;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = employee.Name;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgPhone"].Value = employee.Phone;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgEmail"].Value = employee.Email;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgAddress"].Value = employee.Address;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgSalary"].Value = employee.Salary;
                        guna2DataGridView1.Rows[rowIndex].Cells["dvgHiredate"].Value = employee.HireDate;


                    }
                }
            }
        }

        public void loadGRD()
        {
            var employees = BUS.BUSEmployee.GetEmployees();
            guna2DataGridView1.Rows.Clear();
            foreach (var employee in employees)
            {
                
                int rowIndex = guna2DataGridView1.Rows.Add();

                guna2DataGridView1.Rows[rowIndex].Cells["dvgSr"].Value = rowIndex + 1;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgId"].Value = employee.eID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgUserId"].Value = employee.UserID;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgName"].Value = employee.Name;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgPhone"].Value = employee.Phone;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgEmail"].Value = employee.Email;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgAddress"].Value = employee.Address;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgSalary"].Value = employee.Salary;
                guna2DataGridView1.Rows[rowIndex].Cells["dvgHiredate"].Value = employee.HireDate;

                
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["dvgDel"].Index)
            {
                MessageBox.Show("Delete clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString());

                String employeeName = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString();


                DialogResult result = MessageBox.Show($"Are you sure you want to delete the employee '{employeeName}'?",
                                               "Confirm Delete",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    String employeeID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString();
                    String userID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgUserId"].Value.ToString();
                    bool statusDeleteEmployee = BUS.BUSEmployee.DeleteEmployee(employeeID);
                    if (statusDeleteEmployee)
                    {
                        MessageBox.Show("Delete employee success");
                        bool statusDeleteUser = BUS.BUSUser.removeUser(userID);
                        if (statusDeleteUser)
                        {
                            MessageBox.Show("Delete user success");

                        }
                        else
                        {
                            MessageBox.Show("Delete user failed");
                        }

                        loadGRD();
                    }
                    else
                    {
                        MessageBox.Show("Delete employee failed");
                    }

                }

            }
            else if (e.ColumnIndex == guna2DataGridView1.Columns["dvgEdit"].Index)
            {
                MessageBox.Show("Edit clicked for " + guna2DataGridView1.Rows[e.RowIndex].Cells["dvgName"].Value.ToString());
                String employeeID = guna2DataGridView1.Rows[e.RowIndex].Cells["dvgId"].Value.ToString(); 
                Employee editEmployee = BUS.BUSEmployee.GetEmployeeByID(employeeID);


                if (editEmployee != null)
                {
                    frmAddEmployee frmAddEmployee = new frmAddEmployee(editEmployee, this);
                    frmAddEmployee.ShowDialog();
                }
                else
                {
                    Console.WriteLine("Not available");
                }
                
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEmployee frmAddEmployee = new frmAddEmployee(this);
            frmAddEmployee.ShowDialog();
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = "Save to CSV";
                saveFileDialog.FileName = "EmployeesExport.csv";

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
                            for (int j = 0; j < gridView.Columns.Count - 2; j++) 
                            {
                                writer.Write(row.Cells[j].Value?.ToString().Replace(",", " "));
                                if (j < gridView.Columns.Count - 3) 
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
    }
}
