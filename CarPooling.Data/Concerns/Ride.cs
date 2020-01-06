using System;
using System.Collections.Generic;

namespace CarPooling.Data.Concerns
{
    public class Ride
    {
        public int Id { get; set; }
        public User Host { get; set; }
        public String BoardingPlace { get; set; }
        public String DestinationPlace { get; set; }
        public List<String> ViaRoutes { get; set; }
        public DateTime StartDateTime { get; set; }
        public List<User> UsersJoined { get; set; }
        public int SeatsAvailable { get; set; }
    }
}
