using CarPooling.Data.Concerns;
using System;
using System.Collections.Generic;

namespace CarPooling.Data.OfferRide.Contracts
{
    public interface IOfferRide
    {
        /// <summary>
        /// Creates a ride
        /// </summary>
        /// <param name="ride">ride</param>
        /// <returns>Ride</returns>
        int CreateARide(Ride ride);

        /// <summary>
        /// Get all the Rides of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Ride> GetMyRides(int userId);

        /// <summary>
        /// Get the ride details by id
        /// </summary>
        /// <param name="rideId"></param>
        /// <returns></returns>
        Ride GetRideById(int rideId);

        /// <summary>
        /// check if the given id is valid or not
        /// </summary>
        /// <param name="rides"></param>
        /// <param name="rideId"></param>
        /// <returns></returns>
        Boolean IsRideIdValid(List<Ride> rides, int rideId);

        /// <summary>
        /// filter all the bookings with boarding, destination and numberOfPassengers
        /// </summary>
        /// <param name="boarding"></param>
        /// <param name="destination"></param>
        /// <param name="numberOfPassengers"></param>
        /// <returns></returns>
        List<Ride> GetFilteredRides(String boarding, String destination, int numberOfPassengers);

        /// <summary>
        /// book the seats in that ride
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        void BookSeatsOfARide(Booking booking);
    }
}
