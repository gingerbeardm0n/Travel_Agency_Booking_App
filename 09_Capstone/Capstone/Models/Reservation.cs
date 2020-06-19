using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        //This class contains the definition of a single reservation

        //---------- PROPERTIES ----------------------------------------------------------------------------------------------------------------------------------

        public int ReservationID { get; set; }
        public int SpaceID { get; }
        public string VenueName { get; }
        public string SpaceName { get; }
        public string ReservationName { get; }
        public int NumberOfAttendees { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int ReservationLength { get; }
        public int DailyRate { get; }

        public int TotalReservationCost
        {
            get
            {
                return ReservationLength * DailyRate;
            }
        }

        //---------- CONSTRUCTORS ------------------------------------------------------------------------------------------------------------------------

        public Reservation(int spaceID, string venueName, string spaceName, string reservationName,
            int numberOfAttendees, DateTime startDate, int reservationLength, int dailyRate)
        {
            SpaceID = spaceID;
            VenueName = venueName;
            SpaceName = spaceName;
            ReservationName = reservationName;
            NumberOfAttendees = numberOfAttendees;
            StartDate = startDate;
            ReservationLength = reservationLength;
            DailyRate = dailyRate;
            EndDate = startDate.AddDays(ReservationLength);
            
        }

        //---------- METHODS -----------------------------------------------------------------------------------------------------------------------------
        public override string ToString()
        {
            return "\nConfirmation #:".PadRight(17) + ReservationID.ToString()
                + "\nVenue: ".PadRight(17) + VenueName
                + "\nSpace: ".PadRight(17) + SpaceName
                + "\nReserved For: ".PadRight(17) + ReservationName
                + "\nAttendees: ".PadRight(17) + NumberOfAttendees.ToString()
                + "\nArrival Date: ".PadRight(17) + StartDate.Month.ToString() + "/" + StartDate.Day.ToString() + "/" + StartDate.Year.ToString()
                + "\nDepart Date: ".PadRight(17) + EndDate.Month.ToString() + "/" + EndDate.Day.ToString() + "/" + EndDate.Year.ToString()
                + "\nTotal Cost: ".PadRight(17) + "$" + TotalReservationCost.ToString() +"\n";
        }
    }
}
