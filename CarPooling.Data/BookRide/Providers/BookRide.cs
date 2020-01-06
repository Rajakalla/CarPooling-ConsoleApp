using CarPooling.Data.BookRide.Contracts;
using CarPooling.Data.OfferRide.Providers;
using CarPooling.Data.Concerns;
using CarPooling.Data.OfferRide.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CarPooling.Data.BookRide.Providers
{
    public class BookARide : IBookARide
    {
        /// <summary>
        /// All the bookings created
        /// </summary>
        private List<Booking> AllBookings = new List<Booking>();
     
        /// <summary>
        /// int to generate the new id for a new booking
        /// </summary>
        private static int BookingIdGenerator = 1;

        private IOfferARide OfferRide = new OfferARide();

        public List<Booking> GetMyBookings(int userId)
        {
            return AllBookings.FindAll(booking => booking.RequestedBy.Id == userId).ToList<Booking>();
        }

        public List<Booking> GetRequestsForCurrentUser(User user)
        {
            return AllBookings.FindAll(booking => booking.Status == false && booking.Host.Id == user.Id);
        }

        public Booking BookRide(Booking booking)
        {
            booking.Id = BookingIdGenerator++;
            AllBookings.Add(booking);
            OfferRide.BookSeatsOfARide(booking);
            return booking;
        }
    }
}
