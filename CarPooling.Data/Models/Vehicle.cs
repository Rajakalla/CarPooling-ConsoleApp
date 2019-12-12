using CarPooling.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPooling.Data.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public VehicleType VehicleType { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
