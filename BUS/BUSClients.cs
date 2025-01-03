using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSClients
    {
        private static BUSClients instance;

        public static BUSClients Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUSClients();
                }
                return instance;
            }
            set => instance = value;
        }

        public static bool addClient(String customerName, String email, String phone, String address)
        {
            return DAL.DALClients.addClient(customerName, email, phone, address);
        }

        public static bool deleteClient(String customerId)
        {
            return DAL.DALClients.removeClient(customerId);
        }

        public static bool updateClient(String clientID, String customerName, String email, String phone, String address)
        {
            return DAL.DALClients.updateClient(clientID, customerName, email, phone, address);
        }

        public static CLIENT GetClientsByID(String id)
        {
            return DAL.DALClients.getClientByClientID(id);
        }


        public static List<CLIENT> GetCLIENTs()
        {
            return DAL.DALClients.GetCLIENTs();
        }

        public static List<CLIENT> GetClientsByName(String cName)
        {
            return DAL.DALClients.GetClientsByName(cName);
        }
    }
}
