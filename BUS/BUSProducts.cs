using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSProducts
    {
        private static BUSProducts instance;

        public static BUSProducts Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUSProducts();
                }
                return instance;
            }
            set => instance = value;
        }

        public static List<PRODUCT> getProducts()
        {
            return DAL.DALProducts.GetPRODUCTs();
        } 

        public static bool removeProduct(String pID) {
            return DAL.DALProducts.removeProduct(pID);
        }

        public static bool addProduct(String productName, String description, decimal price, int quantity)
        {
            return DAL.DALProducts.addProduct(productName, description, price, quantity);

        }

        public static bool updateProduct(String productID, String productName, String description, decimal price, int quantity)
        {
            return DAL.DALProducts.UpdateProduct(productID, productName, description, price, quantity);
        }

        public static List<PRODUCT> FindProductsByName(String productName)
        {
            return DAL.DALProducts.FindProductsByName(productName);
        }

        public static PRODUCT GetProductByID(String productID)
        {
            return DAL.DALProducts.getProductByProductID(productID);    
        }

        public static bool UpdateProductQuantity (String productID, int quantity)
        {
            return DAL.DALProducts.UpdateProductQuantity(productID, quantity);
        }

        public static bool RestoreProductQuantity(String productID, int quantity)
        {
            return DAL.DALProducts.restoreQuantityProduct(productID, quantity);
        }
    }
}
