using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BUS
{
    public class BUSUser
    {
        private static BUSUser instance;

        public static BUSUser Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BUSUser();
                }
                return instance;
            }
            set => instance = value;
        }

        public static bool addUser(String username, String password, String role)
        {
            return DAL.DALUser.AddUser(username, password, role);
        }

        public static bool removeUser(String userID)
        {
            return DAL.DALUser.DeleteUser(userID);
        }


        public static bool updateUser(String userID, String username, String password, String role)
        {
            return DAL.DALUser.UpdateUser(userID, username, password, role);
        }

        public static User getUserByUsername(String username)
        {
            return DAL.DALUser.getUserByUserName(username);
        }

        public static User getUserByUserID(String userID)
        {
            return DAL.DALUser.GetUserByUserID(userID);
        }
    }
}
