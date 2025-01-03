using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSOrderItem
    {
        private static BUSOrderItem instance;

        public static BUSOrderItem Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUSOrderItem();
                return instance;
            }
            set => instance = value;
        }

        public static bool AddItem(String orderID, String productID, int quantity)
        {
            return DAL.DALOrderItem.AddItem(orderID, productID, quantity);
        }

        public static bool RemoveItem(String itemID)
        {
            return DAL.DALOrderItem.RemoveItem(itemID);
        }

        public static bool UpdateItem(String itemID, String orderID, String productID, int quantity)
        {
            return DAL.DALOrderItem.UpdateItem(itemID, orderID, productID, quantity);
        }

        public static bool EmptyOrderItems(String orderID)
        {
            return DAL.DALOrderItem.EmptyOrderItems(orderID);
        }

        public static List<ORDERITEM> GetORDERITEMsByID(String orderID)
        {
            return DAL.DALOrderItem.getItemsByOrderID(orderID);
        }

        public static List<ORDERITEM> getAllItems()
        {
            return DAL.DALOrderItem.getAllItems();
        }

    }
}
