using CarPooling.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPooling.Data.UserService
{
    public interface IUserService
    {
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="mobileNumber">mobileNumber</param>
        /// <returns>User</returns>
        User CreateUser(String userName, String mobileNumber);
        /// <summary>
        /// Checks if the user is already there in all Users and will return User
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>User</returns>
        User CheckAndGetUserByName(String userName);
    }
}
