using CarPooling.Data.Concerns;
using System;
using System.Collections.Generic;

namespace CarPooling.Data.BookRide.Contracts
{
    public interface IBookRide
    {
        /// <summary>
        /// Get all the Bookings of the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<Booking> GetMyBookings(int userId);

        /// <summary>
        /// get the booking details by the Id
        /// </summary>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        Booking GetBookingById(int bookingId);

        /// <summary>
        /// Get Requests For CurrentUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        List<Booking> GetBookingRequestsForCurrentUser(User user);

        /// <summary>
        /// checks is the booking is valid or not
        /// </summary>
        /// <param name="bookings"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        bool IsBookingIdValid(List<Booking> bookings, int bookingId);
        /// <summary>
        /// Books a ride
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        Booking BookARide(Booking booking);

        /// <summary>
        /// confirm the booking request
        /// </summary>
        /// <param name="booking"></param>
        void ConfirmBooking(Booking booking);
    }
}
