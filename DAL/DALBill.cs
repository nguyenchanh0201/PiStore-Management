using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBill
    {
        private static DALBill instance; 

        public static DALBill Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALBill();
                }
                return instance;
            }
            set => instance = value;
        }


        public static List<BILL>  getAllBills()
        {
            try
            {
                return PISTORE.Instance.BILLs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool AddBills(String orderID, String customerID, String employeeID)
        {
            try
            {
                String billID = PISTORE.Instance.GenerateBillID().FirstOrDefault();
                DateTime billDate = DateTime.Now; 
                var newBill = new BILL {
                    bID = billID,
                    oID = orderID,
                    cID = customerID,
                    eID = employeeID,
                    BILLDATE = billDate
                };

                PISTORE.Instance.BILLs.Add(newBill);
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static BILL getBillByID(String billID)
        {
            try
            {
                var bill = PISTORE.Instance.BILLs.Where(b => b.bID == billID).FirstOrDefault();
                return bill; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public static bool RemoveBill(String bID)
        {
            try
            {
                var bill = getBillByID(bID);
                if (bill == null)
                {
                    return false; 
                }
                PISTORE.Instance.BILLs.Remove(bill);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        

    }
}
