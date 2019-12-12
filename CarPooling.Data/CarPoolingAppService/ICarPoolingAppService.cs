using CarPooling.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPooling.Data.CarPoolingAppService
{
    public interface ICarPoolingAppService
    {
        /// <summary>
        /// Creates a ride
        /// </summary>
        /// <param name="ride">ride</param>
        /// <returns>Ride</returns>
        int CreateARide(Ride ride);
        
        /// <summary>
        /// Books a ride
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        Booking BookARide(Booking booking);
        
        /// <summary>
        /// Get all the Rides of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Ride> GetMyRides(int userId);
        
        /// <summary>
        /// Get all the Bookings of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Booking> GetMyBookings(int userId);

        /// <summary>
        /// filter all the bookings with boarding, destination and numberOfPassengers
        /// </summary>
        /// <param name="boarding"></param>
        /// <param name="destination"></param>
        /// <param name="numberOfPassengers"></param>
        /// <returns></returns>
        List<Ride> GetFilteredRides(String boarding, String destination, int numberOfPassengers);
        
        /// <summary>
        /// check if the given id is valid or not
        /// </summary>
        /// <param name="rides"></param>
        /// <param name="rideId"></param>
        /// <returns></returns>
        Boolean IsRideIdValid(List<Ride> rides,int rideId);
    }
}
