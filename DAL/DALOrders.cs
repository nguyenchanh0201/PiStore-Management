using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOrders
    {
        private static DALOrders instance;

        public static DALOrders Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALOrders();
                }
                return instance;
            }
            set => instance = value;
        }

        public static List<ORDER> getOrders()
        {
            try
            {
                var orders = PISTORE.Instance.ORDERS.ToList();
                return orders;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool AddOrder(String employeeID, String customerID, DateTime dateTime, decimal total)
        {
            try
            {
                String orderID = PISTORE.Instance.GenerateOrderID().FirstOrDefault();
                var newOrder = new ORDER
                {
                    oID = orderID,
                    eID = employeeID,
                    cID = customerID,
                    ORDERDATE = dateTime,
                    TOTAL = total
                };
                PISTORE.Instance.ORDERS.Add(newOrder);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool RemoveOrder(String orderID)
        {
            try
            {
                var order = getOrderByOrderID(orderID);
                if (order == null) { return false; }
                PISTORE.Instance.ORDERS.Remove(order);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool UpdateOrder(String orderID, String employeeID, String customerID, DateTime dateTime, decimal total)
        {
            try
            {
                var order = getOrderByOrderID(orderID);
                if (order == null) { return false; }
                order.eID = employeeID;
                order.cID = customerID;
                order.ORDERDATE = dateTime;
                order.TOTAL = total;
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        public static ORDER getOrderByOrderID(String orderID) {
            try
            {
                return PISTORE.Instance.ORDERS.Where(o => o.oID == orderID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }



    }
}
