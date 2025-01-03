using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALOrderItem
    {
        private static DALOrderItem instance;

        public static DALOrderItem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALOrderItem();

                }
                return instance;

            }
            set => instance = value;
        }

        public static List<ORDERITEM> getAllItems()
        {
            try
            {
                return PISTORE.Instance.ORDERITEMs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<ORDERITEM> getItemsByOrderID(String orderID)
        {
            try
            {
                var items = PISTORE.Instance.ORDERITEMs.Where(i => i.oID == orderID).ToList();
                return items;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool AddItem(String orderID, String productID, int quantity)
        {
            try
            {
                String itemID = PISTORE.Instance.GenerateOrderItemID().FirstOrDefault();
                var newItem = new ORDERITEM {
                    oiID = itemID,
                    oID = orderID,
                    pID = productID,
                    QUANTITY = quantity
                };

                PISTORE.Instance.ORDERITEMs.Add(newItem);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static ORDERITEM GetItemByItemID(String itemID)
        {
            try
            {
                return PISTORE.Instance.ORDERITEMs.Where(i => i.oiID == itemID).FirstOrDefault();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool RemoveItem(String itemID)
        {
            try
            {
                var item = GetItemByItemID(itemID);
                if (item == null) { return false;  }
                PISTORE.Instance.ORDERITEMs.Remove(item);
                PISTORE.Instance.SaveChanges();
                return true;

            } catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool UpdateItem(String itemID, String orderID, String productID, int quantity)
        {
            try
            {
                var item = GetItemByItemID(itemID);
                if (item == null) { return false; }
                item.oID = orderID;
                item.pID = productID;
                item.QUANTITY = quantity;
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool EmptyOrderItems(String orderID)
        {
            try
            {
                //Get all items 
                var orderItems = getItemsByOrderID(orderID);
                if (orderItems == null)
                {
                    return false;
                }


                //Restore product Quantity
                foreach (var item in orderItems)
                {
                    bool status = DAL.DALProducts.restoreQuantityProduct(item.pID, item.QUANTITY);
                    if (!status) {
                        return false; 
                    }
                }

                //Empty order items
                PISTORE.Instance.ORDERITEMs.RemoveRange(orderItems);
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
