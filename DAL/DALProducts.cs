using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALProducts
    {
        private static DALProducts instance; 

        public static DALProducts Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALProducts();
                }
                return instance;
            }
            set => instance = value;
        }

        public static List<PRODUCT> GetPRODUCTs()
        {
            try
            {
                var products = PISTORE.Instance.PRODUCTs.ToList();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static PRODUCT getProductByProductID(String  productID)
        {
            
                return PISTORE.Instance.PRODUCTs.Where(p => p.pID == productID).FirstOrDefault();
            
        }


        public static bool addProduct(String productName, String description, decimal price, int quantity)
        {

            string productID = PISTORE.Instance.GenerateProductID().FirstOrDefault();
            try
            {
                var newProduct = new PRODUCT
                {
                    pID = productID,
                    NAME = productName,
                    DESCRIPTION = description,
                    PRICE = price,
                    QUANTITY = quantity
                };
                PISTORE.Instance.PRODUCTs.Add(newProduct);
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool UpdateProduct(String productID, String productName, String description, decimal price, int quantity)
        {
            try
            {
                var product = getProductByProductID(productID);
                if (product == null)
                {
                    return false;
                }
                product.NAME = productName;
                product.DESCRIPTION = description;
                product.PRICE = price;
                product.QUANTITY = quantity;
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public static bool removeProduct(String productID)
        {
            try
            {
                var foundProduct = getProductByProductID(productID);
                if (foundProduct == null)
                {
                    return false; 
                }
                PISTORE.Instance.PRODUCTs.Remove(foundProduct);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static List<PRODUCT> FindProductsByName(String productName)
        {
            try
            {
                return PISTORE.Instance.PRODUCTs.Where(p => p.NAME.ToLower().Contains(productName.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static bool UpdateProductQuantity(String productID, int quantity)
        {
            try
            {
                var product = getProductByProductID(productID);
                if (product == null)
                {
                    return false;
                }
                product.QUANTITY -= quantity;
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool restoreQuantityProduct(String productID, int quantity)
        {
            try
            {
                var product = getProductByProductID(productID);
                if (product == null)
                {
                    return false;
                }
                product.QUANTITY += quantity;
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
