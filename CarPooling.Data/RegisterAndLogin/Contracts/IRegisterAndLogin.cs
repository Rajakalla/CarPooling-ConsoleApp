using CarPooling.Data.Concerns;
using System;

namespace CarPooling.Data.RegisterAndLogin.Contracts
{
    public interface IRegisterAndLogin
    {
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="mobileNumber">mobileNumber</param>
        /// <returns>User</returns>
        User CreateUser(User user);
        /// <summary>
        /// login the user if credentials are correct
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        User UserLogin(string userName, string password);
        /// <summary>
        /// Checks if the user is already there in all Users and will return User
        /// </summary>
        /// <param name="userName">userName</param>
        /// <returns>User</returns>
        User CheckAvailabilityOfUserName(String userName);
    }
}
