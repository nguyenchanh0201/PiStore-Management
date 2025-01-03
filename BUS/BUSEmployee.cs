using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BUS
{
    public class BUSEmployee
    {
        private static BUSEmployee instance;

        public static BUSEmployee Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUSEmployee();

                }
                return instance;
            }
            set => instance = value;
        }

        public static List<Employee> GetEmployees()
        {
            return DAL.DALEmployees.GetEmployees();
        }


        public static Employee GetEmployeeByID(String id)
        {
            return DAL.DALEmployees.GetEmployeeByID(id);
        }


        public static bool AddEmployee(String userID, String name, String email, String phone, String address, decimal salary, DateTime hireDate)
        {
            return DAL.DALEmployees.addEmployee(userID, name, email, phone, address, salary, hireDate);
        }

        public static bool UpdateEmployee(String employeeID, String name, String email, String phone, String address, decimal salary, DateTime hireDate)
        {
            return DAL.DALEmployees.updateEmployee(employeeID, name, email, phone, address, salary, hireDate);
        }

        public static bool DeleteEmployee(String employeeID)
        {
            return DAL.DALEmployees.deleteEmployee(employeeID);
        }

        public static Employee GetEmployeeByUserID(String userId)
        {
            return DAL.DALEmployees.GetEmployeeByUserID(userId);
        }

        public static List<Employee> GetEmployeesByName(String eName)
        {
            return DAL.DALEmployees.GetEmployeeByName(eName);
        }
    }
}
