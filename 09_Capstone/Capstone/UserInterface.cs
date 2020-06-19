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
        public UserInterface(string connectionString)
        {
            this.connectionString = connectionString;
            venueIO = new VenuesDAL(connectionString);
        }

        public void Run()
        {
            Console.WriteLine("Reached the User Interface.");
            Console.ReadLine();
        }

        //Main Menu Method
        public void PrintMainMenu()
        {
            bool done = false;
            while (!done)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("\t1) List Venues");
                Console.WriteLine("\t Quit");
                string menuInput = Console.ReadLine();
                switch (menuInput)
                {
                    case "1":
                        //Method call for view venues menu
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

        public void ViewVenuesMenu()
        {
            Console.WriteLine("Which Venue would you like to view?");
            List<Venue> listOfVenueNames = venueIO.GetAllVenues();
            bool done = false;
            bool previousMenu = false;
            bool nextMenu = false;
            string venueNameForNextMenu = "";
            int maxMenuItems = 0;
            while (!done) {
                int menuItemCounter = 1;
                foreach (Venue venue in listOfVenueNames)
                {
                    Console.WriteLine("\t" + menuItemCounter + ") " + venue.VenueName);
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

        public void venueInfoMenu (string venueName)
        {
            Venue venueInfo = venueIO.GetSpecificVenue(venueName);
            List<string> categories = venueInfo.Category;
        }
    }
}
