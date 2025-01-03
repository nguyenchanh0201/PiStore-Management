using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO; 

namespace BUS
{
    public class BUSLogin
    {

        private static BUSLogin instance;

        public static BUSLogin Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUSLogin();
                return instance;
            }
            set => instance = value;
        }

        public bool checkLogin(string username, string password)
        {
            User user = DALUser.getUserByUserName(username);
            if (user == null)
                return false;
            return user.Password == password;
        }
    }
}
