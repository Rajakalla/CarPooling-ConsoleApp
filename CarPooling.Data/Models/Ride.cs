using System;
using System.Collections.Generic;

namespace CarPooling.Data.Models
{
    public class Ride
    {
        public int RideId { get; set; }
        public User RideHost { get; set; }
        public String BoardingPlace { get; set; }
        public String DestinationPlace { get; set; }
        public List<String> ViaRoutes { get; set; }
        public DateTime StartDateTime { get; set; }
        //public Vehicle Vehicle { get; set; }
        public List<User> UsersJoined { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
