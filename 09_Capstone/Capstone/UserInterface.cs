using Capstone.DAL;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class UserInterface
    {
        //ALL Console.ReadLine and WriteLine in this class
        //NONE in any other class

        private string connectionString;
        VenuesDAL venueIO;
        ReservationsDAL reservationIO;
        SpacesDAL spaceIO;
        BookingSystem bookingSystem;
        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueIO = new VenuesDAL(connectionString);
            spaceIO = new SpacesDAL(connectionString);
            reservationIO = new ReservationsDAL(connectionString);
            bookingSystem = new BookingSystem(connectionString);
        }


        public void Run()
        {
            Console.WriteLine("Reached the User Interface. \nPlease press enter to continue."); //Inserted new line with additonal message JB
            Console.ReadLine();

            PrintMainMenu();
        }

        //Main Menu Method
        public void PrintMainMenu()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\t1) List Venues");
                Console.WriteLine("\tQ) Quit");
                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":
                        ViewVenuesMenu();
                        break;
                    case "Q":
                        done = true;
                        Console.WriteLine("Program has ended, press return to close.");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Invalid Entry, please enter 1 or Q");
                        break;
                }
            }
        }
        //Switch Case 1.1 View entire list of Venues
        public void ViewVenuesMenu()
        {
            Console.WriteLine("Which Venue would you like to view?");
            List<Venue> listOfVenueNames = venueIO.GetAllVenues();
            bool done = false;
            bool viewMainMenu = false;
            bool nextMenu = false;
            string venueNameForNextMenu = "";
            int maxMenuItems = 0;
            while (!done)
            {
                int menuItemCounter = 1;

                foreach (Venue venue in listOfVenueNames)
                {
                    Console.WriteLine("\t" + menuItemCounter + ") " + venue.VenueName);
                    menuItemCounter++;
                }
                maxMenuItems = menuItemCounter;

                Console.WriteLine("\tR) Return to Previous Screen");
                string menuInput = Console.ReadLine();
                try
                {
                    if (menuInput == "R")
                    {
                        done = true;
                        viewMainMenu = true;
                    }
                    else if (int.Parse(menuInput) > 0 && int.Parse(menuInput) <= maxMenuItems)
                    {
                        done = true;
                        nextMenu = true;
                        venueNameForNextMenu = listOfVenueNames[int.Parse(menuInput) - 1].VenueName;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Entry, please enter a venue number or R");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid Entry, please enter a venue number or R");
                }

            }
            if (viewMainMenu)
            {
                PrintMainMenu();
            }
            else if (nextMenu)
            {
                VenueInfoMenu(venueNameForNextMenu);
            }

        }
        // VenuesDAL venueIO;
        private void VenueInfoMenu(string venueName)
        {
            Venue venueInfo = venueIO.GetSpecificVenue(venueName);
            List<string> categories = venueInfo.Category;

            bool viewSpaces = false;
            bool searchSpaces = false;

            bool done = false;
            while (!done)
            {

                Console.WriteLine("\n" + venueName);//inserted new line here JB
                Console.WriteLine("Location: " + venueInfo.Location);

                Console.Write("Categories: ");

                int commaCounter = 0;

                foreach (string uniqueCat in categories)
                {
                    if (commaCounter == 0)
                    {
                        Console.Write(uniqueCat);
                    }
                    else
                    {
                        Console.Write(", " + uniqueCat);
                    }
                    commaCounter++;
                }
                Console.WriteLine("\n");

                Console.WriteLine(venueInfo.Description);
                Console.WriteLine("\n");


                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("\t 1) View Spaces");
                Console.WriteLine("\t 2) Search for Reservation");
                Console.WriteLine("\t R) Return to Previous Screen");

                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":
                        viewSpaces = true;
                        done = true;
                        break;

                    case "2":
                        searchSpaces = true;
                        done = true;
                        break;

                    case "R":
                        done = true;
                        break;

                    default:
                        break;
                }

            }
            if (viewSpaces)
            {
                ViewSpaces(venueInfo);
            }
            else if (searchSpaces)
            {
                SearchSpacesMenu(venueInfo);
            }
            else
            {
                Console.WriteLine("Invalid Entry, please enter 1, 2, or R"); //inseted error message JB
                ViewVenuesMenu();
            }
        }
        public void ViewSpaces(Venue venueInfo)
        {
            bool searchSpaceMenu = false;
            bool done = false;

            while (!done)
            {
                Console.WriteLine(venueInfo.VenueName);
                Console.WriteLine("\n");

                ViewSpacesHeader();

                List<Space> listOfSpaces = spaceIO.GetSpacesForVenue(venueInfo.VenueID);

                int counter = 1;

                foreach (Space individulaSpace in listOfSpaces)
                {
                    Console.WriteLine(" #" + counter.ToString().PadRight(3) + individulaSpace.SpaceName.PadRight(40) +
                                        individulaSpace.OpenTo.PadRight(6) + individulaSpace.OpenFrom.PadRight(8) +
                                        "$" + individulaSpace.DailyRate.ToString().PadRight(13) +
                                        individulaSpace.MaxOccupancy.ToString().PadRight(10));

                    counter++;
                }
                Console.WriteLine();
                Console.WriteLine("What would you like to do next?");
                Console.WriteLine("\t1) Reserve a Space");
                Console.WriteLine("\tR) Return to Previous Screen");
                string menuInput = Console.ReadLine();
                if (menuInput == "1")
                {
                    done = true;
                    searchSpaceMenu = true;
                }
                else if (menuInput == "R")
                {
                    done = true;
                }
                else
                {
                    Console.WriteLine("Invalid Entry, please enter 1 or R");
                }
            }
            if (searchSpaceMenu)
            {
                SearchSpacesMenu(venueInfo);
            }
            else
            {
                VenueInfoMenu(venueInfo.VenueName);
            }
        }

        private void ViewSpacesHeader()
        {
            Console.WriteLine(" No. Name                                    Open  Close   Daily Rate    Max Occupancy");
            Console.WriteLine(" -------------------------------------------------------------------------------------");
            Console.WriteLine();
        }

        public void SearchSpacesMenu(Venue venueInfo)
        {
            bool done = false;
            while (!done)
            {
                List<Space> searchResults;
                Console.Write("When do you need the space? Format is MM/DD/YYYY: ");
                try
                {
                    string dateToBeSplit = Console.ReadLine();
                    string[] splitDates = dateToBeSplit.Split("/");
                    DateTime startDate = new DateTime(int.Parse(splitDates[2]), int.Parse(splitDates[0]), int.Parse(splitDates[1]));
                    Console.Write("How many days will you need the space? ");
                    int daysNeeded = int.Parse(Console.ReadLine());
                    Console.Write("How many people will be in attendance? ");
                    int attendeeCount = int.Parse(Console.ReadLine());

                    searchResults = spaceIO.SearchForAvailableSpaces(venueInfo.VenueID, startDate, daysNeeded, attendeeCount);

                    if (searchResults.Count > 0)
                    {
                        Console.WriteLine("\nThe following spaces are available based on your needs:\n");
                        Console.WriteLine("Space #".PadRight(10) + "Name".PadRight(30) + "Daily Rate".PadRight(13) + "Max Occup.".PadRight(13) + "Accessible?".PadRight(14) + "Total Cost");
                        foreach (Space searchResult in searchResults)
                        {
                            string accessible = "Yes";
                            if (!searchResult.Accessablity)
                            {
                                accessible = "No";
                            }
                            Console.WriteLine(searchResult.SpaceID.ToString().PadRight(10) + searchResult.SpaceName.PadRight(30) + searchResult.DailyRate.ToString().PadRight(13) + searchResult.MaxOccupancy.ToString().PadRight(13) + accessible.PadRight(14) + "$" + searchResult.TotalCost);
                        }
                        ReservationOnSearchMenu(searchResults, attendeeCount, startDate, venueInfo, daysNeeded);
                        done = true;
                    }
                    else
                    {
                        Console.WriteLine("\nNo space availability matching available constraints, would you like to make another search? Y or N? \n");
                        string failureResponse = Console.ReadLine();
                        if (failureResponse == "N")
                        {
                            done = true;
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid entry format, Please try again.");
                }
            }
            PrintMainMenu();
        }
        public void ReservationOnSearchMenu(List<Space> searchResults, int attendeeCount, DateTime startDate, Venue venueInfo, int daysNeeded)
        {
            Console.WriteLine("\n");
            Console.Write("Which space would you like to reserve? ");
            int spaceChosen = int.Parse(Console.ReadLine());
            Console.Write("Who is the reservation for? ");
            string reservationName = Console.ReadLine();

            Reservation newReservation = bookingSystem.MakeReservation(searchResults, spaceChosen, attendeeCount, startDate, daysNeeded, venueInfo.VenueName, reservationName);

            Console.WriteLine("\nThanks for submitting your reservation! The details for your event are listed below:");
            Console.WriteLine(newReservation.ToString());
        }

    }
}
