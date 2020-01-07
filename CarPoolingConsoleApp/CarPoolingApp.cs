using CarPooling.Data.BookRide.Contracts;
using CarPooling.Data.BookRide.Providers;
using CarPooling.Data.Concerns;
using CarPooling.Data.OfferRide.Contracts;
using CarPooling.Data.OfferRide.Providers;
using CarPooling.Data.RegisterAndLogin.Contracts;
using CarPooling.Data.RegisterAndLogin.Providers;
using CarPoolingConsoleApp.Concerns;
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
        IRegisterAndLogin RegisterAndLogin = new RegisterAndLogin();
        /// <summary>
        /// provider for offering a ride
        /// </summary>
        IOfferRide OfferRide = new OfferRide();
        /// <summary>
        /// provider for booking a ride
        /// </summary>
        IBookRide BookRide = new BookRide();
        User CurrentUser { get; set; } = null;
        public CarPoolingApp()
        {

        }

        /// <summary>
        /// Initializes the App
        /// </summary>
        internal void Initilaze()
        {
            Console.WriteLine("\n-------------------------------");
            Console.WriteLine("\nWelcome to car pooling service");
            Console.WriteLine("\n-------------------------------\n");
            Boolean exitApp = false;
            while (!exitApp)
            {
                Boolean exitMenu = false;
                if (CurrentUser != null)
                {
                    Console.WriteLine($"\nWelcome {CurrentUser.Name}\n");
                    while (!exitMenu)
                    {
                        exitMenu = ShowMenu();
                    }
                }
                else
                {
                    exitApp = RegisterOrLogin();
                }
            }
        }

        internal Boolean RegisterOrLogin()
        {
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("\nChoose the Option:");
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("\n1) Register");
            Console.WriteLine("\n2) Login");
            Console.WriteLine("\n3) Exit\n");
            int option = Extensions.GetUserInputAsInt();
            switch (option)
            {
                case 1:
                    Console.Clear();
                    RegisterUser();
                    break;
                case 2:
                    Console.Clear();
                    LoginUser();
                    break;
                case 3:
                    return true;
                default: return false;
            }
            return false;
        }

        /// <summary>
        /// Register the user
        /// </summary>
        internal void RegisterUser()
        {
            User newUser;
            String userName;
            do
            {
                Console.Write("\n Enter UserName: ");
                userName = Extensions.GetUserInputAsString();
                newUser = RegisterAndLogin.CheckAvailabilityOfUserName(userName);
                if (newUser != null)
                {
                    Console.Write("\n User already available, choose a different user name.\n");
                }
            } while (newUser != null);
            Console.Write("\n Enter password: ");
            String password = Extensions.GetUserInputAsString();
            Console.Write("\n Enter Mobile Number: ");
            String mobileNumber = Extensions.GetUserInputAsString();
            newUser = new User
            {
                Name = userName,
                MobileNumber = mobileNumber,
                Password = password
            };
            CurrentUser = RegisterAndLogin.CreateUser(newUser);
        }

        /// <summary>
        /// Login the user
        /// </summary>
        internal void LoginUser()
        {
            Console.Write("\n Enter UserName: ");
            String userName = Extensions.GetUserInputAsString();
            Console.Write("\n Enter Password: ");
            String password = Extensions.GetUserInputAsString();
            User user = RegisterAndLogin.UserLogin(userName, password);
            if (user == null)
            {
                Console.Write("\n Username or password is incorrect. \n");
            }
            else
            {
                CurrentUser = user;
            }
        }

        /// <summary>
        /// Show Options Menu
        /// </summary>
        /// <returns></returns>
        internal Boolean ShowMenu()
        {
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("\nChoose the Option:");
            Console.WriteLine("\n-------------------------------------");
            Console.WriteLine("\n1) Offer a Ride");
            Console.WriteLine("\n2) Request a Ride");
            Console.WriteLine("\n3) My Rides");
            Console.WriteLine("\n4) My Bookings");
            Console.WriteLine("\n5) Exit User\n");
            int option = Extensions.GetUserInputAsInt();
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
                    List<Ride> myRides = OfferRide.GetMyRides(CurrentUser.Id);
                    List<Booking> bookings = BookRide.GetBookingRequestsForCurrentUser(CurrentUser);
                    Console.Write("\n__________________________________________________________________");
                    Console.Write("\nMy Offered Rides:");
                    Console.Write("\n__________________________________________________________________\n");
                    ShowRides(myRides);
                    if (bookings.Count > 0)
                    {
                        Boolean isValid;
                        do
                        {
                            Console.Write("\n___________________________________________________");
                            Console.Write("\n Requests for your rides:");
                            Console.Write("\n___________________________________________________\n");
                            ShowBookings(bookings);
                            Console.Write("\n Enter the request Id to approve (-1 to exit): ");
                            int requestId = Extensions.GetUserInputAsInt();
                            isValid = BookRide.IsBookingIdValid(bookings, requestId);
                            if (requestId == -1)
                            {
                                Console.Write("***Enter valid input.***");
                                isValid = true;
                            }
                            else if (isValid)
                            {
                                Booking booking = BookRide.GetBookingById(requestId);
                                BookRide.ConfirmBooking(booking);
                                OfferRide.BookSeatsOfARide(booking);
                                isValid = false;
                                Console.WriteLine($"Booking request with id {booking.Id} is confirmed.");
                            }
                        } while (isValid);
                    }
                    break;
                case CarPoolingOptions.MyBookings:
                    Console.Clear();
                    List<Booking> myBookings = BookRide.GetMyBookings(CurrentUser.Id);
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
            String boardingPlace = Extensions.GetUserInputAsString();
            Console.WriteLine("Enter Destination Place:");
            String destinationPlace = Extensions.GetUserInputAsString();
            Console.WriteLine("Enter Via Routes:");
            List<String> viaRoutes = Extensions.GetUserInputAsString().Split(',').ToList<String>();
            Console.WriteLine("Enter Number of seats:");
            int numberOfSeats = Extensions.GetUserInputAsInt();
            Console.WriteLine("Enter Date and time of starting the ride");
            DateTime dateTime = Extensions.GetUserInputAsDateTime();
            ride = new Ride
            {
                Host = CurrentUser,
                BoardingPlace = boardingPlace,
                DestinationPlace = destinationPlace,
                ViaRoutes = viaRoutes,
                SeatsAvailable = numberOfSeats,
                StartDateTime = dateTime
            };
            OfferRide.CreateARide(ride);
            Console.WriteLine($"\nYou ride has been created\n");
        }

        /// <summary>
        /// Book a ride
        /// </summary>
        internal void BookARide()
        {
            Booking booking;
            Console.WriteLine("Enter Boarding Place:");
            String boardingPlace = Extensions.GetUserInputAsString();
            Console.WriteLine("Enter Destination Place:");
            String destinationPlace = Extensions.GetUserInputAsString();
            Console.WriteLine("Enter Number of Passengers:");
            int numberOfPassengers = Extensions.GetUserInputAsInt();
            Console.WriteLine("Enter Date and time of starting the ride");
            DateTime dateTime = Extensions.GetUserInputAsDateTime();
            List<Ride> rides = OfferRide.GetFilteredRides(boardingPlace, destinationPlace, numberOfPassengers);
            if (rides.Count > 0)
            {
                Console.Write("\n___________________________________________________");
                Console.Write("\n Available rides for your request:");
                Console.Write("\n___________________________________________________\n");
                ShowRides(rides);
                Boolean isValid;
                do
                {
                    Console.Write("\nChoose Ride Id (-1 to exit booking): ");
                    int rideId = Extensions.GetUserInputAsInt();
                    isValid = OfferRide.IsRideIdValid(rides, rideId);
                    if (rideId == -1)
                    {
                        isValid = true;
                    }
                    else if (isValid)
                    {
                        booking = new Booking
                        {
                            RideId = rideId,
                            Host = OfferRide.GetRideById(rideId).Host,
                            RequestedBy = CurrentUser,
                            BoardingPlace = boardingPlace,
                            DestinationPlace = destinationPlace,
                            NumberOfSeatsBooked = numberOfPassengers,
                            DateOfRide = dateTime,
                            Status = false,
                        };
                        BookRide.BookARide(booking);
                        Console.WriteLine("\nYour booking request has been sent.");
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
                    Console.WriteLine($"\n Ride Id : {ride.Id} ");
                    Console.WriteLine($" Host : {ride.Host.Name.ToUpper()} ( Ph.No: {ride.Host.MobileNumber})");
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
                    String status = booking.Status ? "Booked" : "Pending";
                    Console.WriteLine($"\n Booking Id : {booking.RideId} ");
                    Console.WriteLine($" Ride Host : {booking.Host.Name.ToUpper()} ( Ph.No: {booking.Host.MobileNumber})");
                    Console.WriteLine($" Requested By: {booking.RequestedBy.Name.ToUpper()} ( Ph.No: {booking.RequestedBy.MobileNumber})");
                    Console.WriteLine($" Route : {booking.BoardingPlace} --> {booking.DestinationPlace}");
                    Console.WriteLine($" Seats Booked : {booking.NumberOfSeatsBooked}");
                    Console.WriteLine($" Date and Time : {booking.DateOfRide.ToString()}");
                    Console.WriteLine($" Status : {status}");
                }
            }
            else
            {
                Console.WriteLine("\n There are no bookings. \n");
            }
        }
    }
}
