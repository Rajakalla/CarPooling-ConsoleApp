using System;
using System.Collections.Generic;
using System.Text;
using CarPooling.Data.Models;

namespace CarPooling.Data.UserService
{
    public class UserService : IUserService
    {
        /// <summary>
        /// All the users registered/created
        /// </summary>
        private List<User> AllUsers { get; set; } = new List<User>();
        /// <summary>
        /// int to generate the new id for a new user.
        /// </summary>
        private static int UserIdGenerator = 1;
        public User CheckAndGetUserByName(string userName)
        {
            User user=AllUsers.Find(u=>u.UserName==userName);
            if (user!=null)
            {
                return user; 
            }
            else
            {
                return null;
            }
        }

        public User CreateUser(String userName,String mobileNumber)
        {
            User newUser = new User
            {
                UserId = UserIdGenerator++,
                MobileNumber = mobileNumber,
                UserName = userName,
            };
            AllUsers.Add(newUser);
            return newUser;
        }
    }
}
