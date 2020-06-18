using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    class Venue
    {
        //This class contains the definition of a single reservation

        //---------- PROPERTIES ----------------------------------------------------------------------------------------------------------------------------------

        public int ReservationID { get; }
        public int SpaceID { get; }
        public int NumberOfAttendees { get; }
        public DateTime StartDate { get; } // This is where we need to figure out what the heck format we want to use for our dates
        public DateTime EndDate { get; }   // I just put datetime in for now, and we'll most likely change later
        public string ReservationName { get; }

    }
}
