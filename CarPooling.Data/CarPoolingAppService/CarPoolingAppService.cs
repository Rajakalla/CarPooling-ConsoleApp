using CarPooling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPooling.Data.CarPoolingAppService
{
    public class CarPoolingAppService : ICarPoolingAppService
    {
        /// <summary>
        /// All the rides created
        /// </summary>
        private List<Ride> AllRides = new List<Ride>();
        /// <summary>
        /// All the bookings created
        /// </summary>
        private List<Booking> AllBookings = new List<Booking>();
        /// <summary>
        /// int to generate the new id for a new ride
        /// </summary>
        private static int RideIdGenerator = 1;
        /// <summary>
        /// int to generate the new id for a new booking
        /// </summary>
        private static int BookingIdGenerator = 1;

        public Booking BookARide(Booking booking)
        {
            booking.BookingId = BookingIdGenerator++;
            AllBookings.Add(booking);
            Ride ride = AllRides.Find(r => r.RideId == booking.RideId);
            ride.SeatsAvailable = ride.SeatsAvailable - booking.NumberOfSeatsBooked;
            return booking;
        }

        public int CreateARide(Ride ride)
        {
            ride.RideId = RideIdGenerator++;
            AllRides.Add(ride);
            return ride.RideId;
        }

        public List<Booking> GetMyBookings(int userId)
        {
            return AllBookings.FindAll(booking => booking.RequestedBy.UserId == userId).ToList<Booking>();
        }

        public List<Ride> GetMyRides(int userId)
        {
            return AllRides.FindAll(ride => ride.RideHost.UserId == userId).ToList<Ride>();
        }

        public List<Ride> GetFilteredRides(string boarding, string destination, int numberOfPassengers)
        {
            return AllRides.FindAll(ride => ride.SeatsAvailable >= numberOfPassengers).ToList<Ride>()
                .FindAll(ride => ride.BoardingPlace == boarding || ride.ViaRoutes.Contains(boarding)).ToList<Ride>()
                .FindAll(ride => ride.DestinationPlace == destination || ride.ViaRoutes.Contains(destination)).ToList<Ride>();
        }

        public bool IsRideIdValid(List<Ride> rides, int rideId)
        {
            Ride ride = rides.Find(r => r.RideId == rideId);
            if (ride!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
