using CarPooling.Data.CarPoolingAppService;
using CarPooling.Data.Models;
using CarPooling.Data.UserService;
using CarPoolingConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarPoolingConsoleApp
{
    internal class CarPoolingApp
    {
        /// <summary>
        /// user service
        /// </summary>
        IUserService UserService = new UserService();
        /// <summary>
        /// car pooling app service
        /// </summary>
        ICarPoolingAppService CarPoolingAppService = new CarPoolingAppService();
        User CurrentUser { get; set; } = null;
        public CarPoolingApp()
        {

        }

        /// <summary>
        /// Initializes the App
        /// </summary>
        internal void Initilaze()
        {
            Console.WriteLine("-------------------------------\n");
            Console.WriteLine("Welcome to car pooling service\n");
            Console.WriteLine("-------------------------------\n");
            while (true)
            {
                LoginUser();
            }
        }

        /// <summary>
        /// Login the user
        /// </summary>
        internal void LoginUser()
        {
            Console.Write("\n Enter UserName: ");
            String userName = Utilities.GetUserInputAsString();
            User user = UserService.CheckAndGetUserByName(userName);
            if (user == null)
            {
                Console.Write("\n Enter Mobile Number: ");
                String mobileNumber = Utilities.GetUserInputAsString();
                CurrentUser = UserService.CreateUser(userName, mobileNumber);
            }
            else
            {
                CurrentUser = user;
            }
            Console.WriteLine($"\nWelcome {userName}\n");
            Boolean exitMenu = false;
            if (CurrentUser != null)
            {
                while (!exitMenu)
                {
                    exitMenu = ShowMenu();
                }
            }
        }

        /// <summary>
        /// Show Options Menu
        /// </summary>
        /// <returns></returns>
        internal Boolean ShowMenu()
        {
            Console.WriteLine("\n-------------------------------------\n");
            Console.WriteLine("\nChoose the Option:\n");
            Console.WriteLine("\n-------------------------------------\n");
            Console.WriteLine("1) Offer a Ride\n");
            Console.WriteLine("2) Request a Ride\n");
            Console.WriteLine("3) My Rides\n");
            Console.WriteLine("4) My Bookings\n");
            Console.WriteLine("5) Exit User\n");
            int option = Utilities.GetUserInputAsInt();
            switch ((CarPoolingOptions)option)
            {
                case CarPoolingOptions.OfferRide:
                    Console.Clear();
                    OfferARide();
                    break;
                case CarPoolingOptions.RequestRide:
                    Console.Clear();
                    BookARide();
                    break;
                case CarPoolingOptions.MyRides:
                    Console.Clear();
                    List<Ride> myRides = CarPoolingAppService.GetMyRides(CurrentUser.UserId);
                    ShowRides(myRides);
                    break;
                case CarPoolingOptions.MyBookings:
                    Console.Clear();
                    List<Booking> myBookings = CarPoolingAppService.GetMyBookings(CurrentUser.UserId);
                    ShowBookings(myBookings);
                    break;
                case CarPoolingOptions.ExitUser:
                    Console.Clear();
                    CurrentUser = null;
                    return true;
                default: return false;
            }
            return false;
        }

        /// <summary>
        /// Offer a ride
        /// </summary>
        internal void OfferARide()
        {
            Ride ride;
            Console.WriteLine("Enter Boarding Place:");
            String boardingPlace = Utilities.GetUserInputAsString();
            Console.WriteLine("Enter Destination Place:");
            String destinationPlace = Utilities.GetUserInputAsString();
            Console.WriteLine("Enter Via Routes:");
            List<String> viaRoutes = Utilities.GetUserInputAsString().Split(',').ToList<String>();
            Console.WriteLine("Enter Number of seats:");
            int numberOfSeats = Utilities.GetUserInputAsInt();
            Console.WriteLine("Enter Date and time of starting the ride");
            DateTime dateTime = Utilities.GetUserInputAsDateTime();
            ride = new Ride
            {
                RideHost = CurrentUser,
                BoardingPlace = boardingPlace,
                DestinationPlace = destinationPlace,
                ViaRoutes = viaRoutes,
                SeatsAvailable = numberOfSeats,
                StartDateTime = dateTime
            };
            CarPoolingAppService.CreateARide(ride);
            Console.WriteLine($"\nYou ride has been created\n");
        }

        /// <summary>
        /// Book a ride
        /// </summary>
        internal void BookARide()
        {
            Booking booking;
            Console.WriteLine("Enter Boarding Place:");
            String boardingPlace = Utilities.GetUserInputAsString();
            Console.WriteLine("Enter Destination Place:");
            String destinationPlace = Utilities.GetUserInputAsString();
            Console.WriteLine("Enter Number of Passengers:");
            int numberOfPassengers = Utilities.GetUserInputAsInt();
            Console.WriteLine("Enter Date and time of starting the ride");
            DateTime dateTime = Utilities.GetUserInputAsDateTime();
            List<Ride> rides = CarPoolingAppService.GetFilteredRides(boardingPlace, destinationPlace, numberOfPassengers);
            if (rides.Count > 0)
            {
                ShowRides(rides);
                Boolean isValid;
                do
                {
                    Console.Write("\nChoose Ride Id (-1 to exit booking): ");
                    int rideId = Utilities.GetUserInputAsInt();
                    isValid = CarPoolingAppService.IsRideIdValid(rides, rideId);
                    if (rideId == -1)
                    {
                        isValid = true;
                    }
                    else if (isValid)
                    {
                        booking = new Booking
                        {
                            RideId = rideId,
                            RequestedBy = CurrentUser,
                            BoardingPlace = boardingPlace,
                            DestinationPlace = destinationPlace,
                            NumberOfSeatsBooked = numberOfPassengers,
                            DateOfRide = dateTime
                        };
                        CarPoolingAppService.BookARide(booking);
                        Console.WriteLine("Your ride has been booked");
                    }
                    else
                    {
                        Console.WriteLine("\n Selected Ride id is not valid. \n");
                    }
                } while (!isValid);
            }
            else
            {
                Console.WriteLine("\n There are no rides in your route. \n");
            }
        }

        /// <summary>
        /// Display all the rides
        /// </summary>
        internal void ShowRides(List<Ride> rides)
        {
            if (rides.Count > 0)
            {
                foreach (Ride ride in rides)
                {
                    Console.WriteLine($"\n Ride Id : {ride.RideId} ");
                    Console.WriteLine($" Route : {ride.BoardingPlace} --> {String.Join(" --> ", ride.ViaRoutes)} --> {ride.DestinationPlace}");
                    Console.WriteLine($" Number of seats available : {ride.SeatsAvailable}");
                    Console.WriteLine($" Date and Time : {ride.StartDateTime.ToString()}");
                }
            }
            else
            {
                Console.WriteLine("\n There are no rides. \n");
            }
        }

        /// <summary>
        /// Display all the bookings
        /// </summary>
        internal void ShowBookings(List<Booking> bookings)
        {
            if (bookings.Count > 0)
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine($"\n Ride Id : {booking.RideId} ");
                    Console.WriteLine($" Route : {booking.BoardingPlace} --> {booking.DestinationPlace}");
                    Console.WriteLine($" Seats Booked : {booking.NumberOfSeatsBooked}");
                    Console.WriteLine($" Date and Time : {booking.DateOfRide.ToString()}");
                }
            }
            else
            {
                Console.WriteLine("\n There are no bookings. \n");
            }
        }
    }
}
