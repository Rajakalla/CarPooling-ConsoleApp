using System;

namespace CarPooling.Data.Concerns
{
    public class Booking
    {
        public int Id { get; set; }
        public int RideId { get; set; }
        public User Host { get; set; }
        public User RequestedBy { get; set; }
        public String BoardingPlace { get; set; }
        public String DestinationPlace { get; set; }
        public DateTime DateOfRide { get; set; }
        public int NumberOfSeatsBooked { get; set; }
        public Boolean Status { get; set; }
    }
}
