using CarPooling.Data.BookRide.Contracts;
using CarPooling.Data.BookRide.Providers;
using CarPooling.Data.Concerns;
using CarPooling.Data.OfferRide.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CarPooling.Data.OfferRide.Providers
{
    public class OfferARide : IOfferARide
    {
        /// <summary>
        /// All the rides created
        /// </summary>
        private List<Ride> OfferedRides = new List<Ride>();
        /// <summary>
        /// int to generate the new id for a new ride
        /// </summary>
        private static int RideIdGenerator = 1;
        //private IBookARide BookRide = new BookARide();

        public int CreateARide(Ride ride)
        {
            ride.Id = RideIdGenerator++;
            OfferedRides.Add(ride);
            return ride.Id;
        }

        public List<Ride> GetMyRides(int userId)
        {
            return OfferedRides.FindAll(ride => ride.Host.Id == userId).ToList<Ride>();
        }

        public bool IsRideIdValid(List<Ride> rides, int rideId)
        {
            Ride ride = rides.Find(r => r.Id == rideId);
            if (ride!=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public List<Booking> GetAllRequests(User user)
        //{
        //    return BookRide.GetRequestsForCurrentUser(user);
        //}

        public List<Ride> GetFilteredRides(string boarding, string destination, int numberOfPassengers)
        {
            return OfferedRides.FindAll(ride => ride.SeatsAvailable >= numberOfPassengers).ToList<Ride>()
                .FindAll(ride => ride.BoardingPlace == boarding || ride.ViaRoutes.Contains(boarding)).ToList<Ride>()
                .FindAll(ride => ride.DestinationPlace == destination || ride.ViaRoutes.Contains(destination)).ToList<Ride>();
        }

        public void BookSeatsOfARide(Booking booking)
        {
            Ride ride = OfferedRides.Find(r => r.Id == booking.RideId);
            ride.SeatsAvailable = ride.SeatsAvailable - booking.NumberOfSeatsBooked;
        }
    }
}
