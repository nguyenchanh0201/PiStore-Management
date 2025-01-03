using DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALClients
    {
        private static DALClients instance; 

        public static DALClients Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DALClients();
                }

                return instance;
            }
            set => instance = value;
        }


        public static bool addClient(String name, String email, String phone, String address )
        {
            try
            {
                string clientID = PISTORE.Instance.GenerateClientID().FirstOrDefault();
                var newClient = new CLIENT()
                {
                    cID = clientID,
                    name = name,
                    email = email,
                    phone = phone,
                    address = address
                };
                PISTORE.Instance.CLIENTs.Add(newClient);
                PISTORE.Instance.SaveChanges();
                return true;

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool removeClient(String clientID)
        {
            try
            {
                var client = DAL.DALClients.getClientByClientID(clientID);
                if (client == null)
                {
                    return false; 
                }

                PISTORE.Instance.CLIENTs.Remove(client);
                PISTORE.Instance.SaveChanges();
                return true;

            } catch(Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool updateClient(String clientID, String name, String email, String phone, String address)
        {
            try
            {
                var client = getClientByClientID(clientID);
                if (client == null)
                {
                    return false;
                }
                client.name = name;
                client.email = email;
                client.phone = phone;
                client.address = address;
                PISTORE.Instance.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

         

        public static CLIENT getClientByClientID(String clientID)
        {
            return PISTORE.Instance.CLIENTs.Where(c => c.cID == clientID).FirstOrDefault();
        }

        public static List<CLIENT> GetCLIENTs()
        {
            try
            {
                var clients = PISTORE.Instance.CLIENTs.ToList();
                return clients;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public static List<CLIENT> GetClientsByName(String cName)
        {
            try
            {
                return PISTORE.Instance.CLIENTs.Where(c => c.name.ToLower().Contains(cName.ToLower())).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


    }
}
