using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using DTO; 

namespace DAL
{
    public class DALUser
    {
        private static DALUser instance;

        public static DALUser Instance
        {
            get
            {
                if (instance == null)
                    instance = new DALUser();
                return instance;
            }
            set => instance = value;
        }

        public static  User GetUserByUserID(String userID)
        {
            try
            {
                var user = PISTORE.Instance.Users.Where(u => u.UserID == userID).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                return null;
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            } 
        }
        public static User getUserByUserName(string username)
        {
            return PISTORE.Instance.Users.Where(u =>u.Username == username).FirstOrDefault();
        }

        public static bool AddUser(String username, String password, String role)
        {
            String userID = PISTORE.Instance.GenerateUserID().FirstOrDefault();
            try
            {
                var user = new User
                {
                    UserID = userID,
                    Username = username,
                    Password = password,
                    Role = role
                };
                PISTORE.Instance.Users.Add(user);

                try
                {
                    PISTORE.Instance.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Console.WriteLine($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                        }
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }




        public static bool UpdateUser(String userID, String username, String password, String role)
        {
            try
            {
                var user = GetUserByUserID(userID);
                if (user == null)
                {
                    return false;
                }
                user.Username = username;
                user.Password = password;
                user.Role = role;
                PISTORE.Instance.SaveChanges();
                return true;

            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public static bool DeleteUser(string userID)
        {
            try
            {
                var user = GetUserByUserID(userID);
                if (user == null) { return false; }
                PISTORE.Instance.Users.Remove(user);
                PISTORE.Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static List<User> GetUsers()
        {
            try
            {
                var users = PISTORE.Instance.Users.ToList();
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
