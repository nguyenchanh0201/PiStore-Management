using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
    public class DALEmployees
    {
        private static DALEmployees instance;

        public static DALEmployees Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALEmployees();

                }
                return instance;

            }
            set => instance = value;
        }

        public static bool addEmployee(String userID , String name, String email, String phone, String address, decimal salary, DateTime hireDate)
        {
            try
            {
                String employeeID = PISTORE.Instance.GenerateEmployeeID().FirstOrDefault();


                var employee = new Employee()
                {
                    eID = employeeID,
                    UserID = userID,
                    Name = name,
                    Email = email,
                    Phone = phone,
                    Address = address,
                    Salary = salary,
                    HireDate = hireDate
                };
                PISTORE.Instance.Employees.Add(employee);
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }


        public static bool updateEmployee(String employeeID, String name, String email, String phone, String address, decimal salary, DateTime hireDate)
        {
            try
            {
                var employee = GetEmployeeByID(employeeID);
                if (employee == null)
                {
                    return false;
                }
                employee.Name = name;
                employee.Email = email;
                employee.Phone = phone;
                employee.Address = address;
                employee.Salary = salary;
                employee.HireDate = hireDate;
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool deleteEmployee(String employeeID)
        {
            try
            {
                var employee = GetEmployeeByID(employeeID); 
                if (employee == null) { return false; }
                PISTORE.Instance.Employees.Remove(employee);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static List<Employee> GetEmployees()
        {
            try
            {
                var employees = PISTORE.Instance.Employees.ToList();
                return employees;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public static Employee GetEmployeeByID(String id)
        {
            try
            {
                return PISTORE.Instance.Employees.Where(e => e.eID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static Employee GetEmployeeByUserID(String userId)
        {
            try
            {
                return PISTORE.Instance.Employees.Where(e => e.UserID == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public static List<Employee> GetEmployeeByName(String eName)
        {
            try
            {
                return PISTORE.Instance.Employees.Where(e => e.Name.ToLower().Contains(eName.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        
    }
}
