using CarPooling.Data.BookRide.Contracts;
using CarPooling.Data.Concerns;
using System.Collections.Generic;
using System.Linq;

namespace CarPooling.Data.BookRide.Providers
{
    public class BookRide : IBookRide
    {
        /// <summary>
        /// All the bookings created
        /// </summary>
        private List<Booking> AllBookings = new List<Booking>();

        /// <summary>
        /// int to generate the new id for a new booking
        /// </summary>
        private static int BookingIdGenerator = 1;

        public List<Booking> GetMyBookings(int userId)
        {
            return AllBookings.FindAll(booking => booking.RequestedBy.Id == userId).ToList<Booking>();
        }

        public Booking GetBookingById(int bookingId)
        {
            return AllBookings.Find(b => b.Id == bookingId);
        }

        public bool IsBookingIdValid(List<Booking> bookings, int bookingId)
        {
            Booking booking = bookings.Find(b => b.Id == bookingId);
            if (booking != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Booking> GetBookingRequestsForCurrentUser(User user)
        {
            return AllBookings.FindAll(booking => booking.Status == false && booking.Host.Id == user.Id);
        }

        public Booking BookARide(Booking booking)
        {
            booking.Id = BookingIdGenerator++;
            AllBookings.Add(booking);
            return booking;
        }

        public void ConfirmBooking(Booking booking)
        {
            booking.Status = true;
        }
    }
}
