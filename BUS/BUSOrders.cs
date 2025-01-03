using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSOrders
    {
        private static BUSOrders instance;


        public static BUSOrders Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUSOrders();
                }
                return instance;
            }
            set => instance = value;
        }

        public static List<ORDER> GetORDERs()
        {
            return DAL.DALOrders.getOrders();
        }


        public static bool AddOrder(String employeeID, String customerID, DateTime dateTime, decimal total)
        {
            return DAL.DALOrders.AddOrder(employeeID, customerID, dateTime, total);
        }

        public static bool RemoveOrder(String orderId)
        {
            return DAL.DALOrders.RemoveOrder(orderId);
        }

        public static bool UpdateOrder(String orderId, String employeeID, String customerID, DateTime dateTime, decimal total)
        {
            return DAL.DALOrders.UpdateOrder(orderId, employeeID, customerID, dateTime, total);
        }

        public static ORDER GetORDERByID(String orderID)
        {
            return DAL.DALOrders.getOrderByOrderID(orderID);
        } 


    }
}
    