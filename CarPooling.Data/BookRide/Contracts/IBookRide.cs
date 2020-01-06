using CarPooling.Data.Concerns;
using System;
using System.Collections.Generic;

namespace CarPooling.Data.BookRide.Contracts
{
    public interface IBookARide
    {
        /// <summary>
        /// Get all the Bookings of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Booking> GetMyBookings(int userId);

        /// <summary>
        /// Get Requests For CurrentUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<Booking> GetRequestsForCurrentUser(User user);
        
        /// <summary>
        /// Books a ride
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        Booking BookRide(Booking booking);
    }
}
