using CarPooling.Data.Concerns;
using CarPooling.Data.RegisterAndLogin.Contracts;
using System;
using System.Collections.Generic;

namespace CarPooling.Data.RegisterAndLogin.Providers
{
    public class RegisterAndLogin : IRegisterAndLogin
    {
        /// <summary>
        /// All the users registered/created
        /// </summary>
        private List<User> AllUsers { get; set; } = new List<User>();
        /// <summary>
        /// int to generate the new id for a new user.
        /// </summary>
        private static int UserIdGenerator = 1;
        public User CheckAvailabilityOfUserName(string userName)
        {
            User user=AllUsers.Find(u=>u.Name==userName);
            if (user!=null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User UserLogin(string userName, string password)
        {
            User user = AllUsers.Find(u => u.Name == userName && u.Password == password);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User CreateUser(User user)
        {
            user.Id = UserIdGenerator++;
            AllUsers.Add(user);
            return user;
        }
    }
}
