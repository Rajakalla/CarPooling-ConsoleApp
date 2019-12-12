using System;
using System.Collections.Generic;
using System.Text;

namespace CarPooling.Data.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RideId { get; set; }
        public User RequestedBy { get; set; }
        public String BoardingPlace { get; set; }
        public String DestinationPlace { get; set; }
        public DateTime DateOfRide { get; set; }
        public int NumberOfSeatsBooked { get; set; }
    }
}
