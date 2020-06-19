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

        SpacesDAL spaceIO;
        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueIO = new VenuesDAL(connectionString);
            spaceIO = new SpacesDAL(connectionString);
        }


        public void Run()
        {
            Console.WriteLine("Reached the User Interface.");
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
                        ViewVenuesMenu();  //Method call for view venues menu
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
            bool previousMenu = false;
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
                        previousMenu = true;
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
            if (previousMenu)
            {
                PrintMainMenu();
            }
            else if (nextMenu)
            {
                venueInfoMenu(venueNameForNextMenu);
            }

        }
        // VenuesDAL venueIO;
        private void venueInfoMenu(string venueName)
        {
            Venue venueInfo = venueIO.GetSpecificVenue(venueName);
            List<string> categories = venueInfo.Category;

            bool viewSpaces = false;
            bool searchSpaces = false;
            
            bool done = false;
            while (!done)
            {

                Console.WriteLine(venueName);
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
                //Viewspace method call
            }
            else if (searchSpaces)
            {
                // Serach for reservation method call
            }
            else
            {
                ViewVenuesMenu();
            }
        }
        public void ViewSpaces(Venue venueInfo)
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
        }
        private void ViewSpacesHeader()
        {
            Console.WriteLine(" No. Name                                    Open  Close   Daily Rate    Max Occupancy");
            Console.WriteLine(" -------------------------------------------------------------------------------------");
            Console.WriteLine();
        }
        
    }
}
