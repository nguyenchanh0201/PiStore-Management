using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BUS
{
    public class BUSBill
    {
        private static BUSBill instance; 

        public static BUSBill Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUSBill();
                }
                return instance;
            }
            set => instance = value;
        }

        public static List<BILL> getAllBills()
        {
            return DAL.DALBill.getAllBills();
        }

        public static bool AddBill(String orderID, String customerID, String employeeID)
        {
            return DAL.DALBill.AddBills(orderID, customerID, employeeID);
        }

        public static bool RemoveBill(String bID)
        {
            return DAL.DALBill.RemoveBill(bID);
        }

        public static BILL GetBillByID(String bId)
        {
            return DAL.DALBill.getBillByID(bId);
        }

    }
}
