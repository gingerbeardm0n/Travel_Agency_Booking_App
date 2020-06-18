using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Reservation
    {
        //This class contains the definition of a single reservation

        //---------- PROPERTIES ----------------------------------------------------------------------------------------------------------------------------------

        public int ReservationID { get; }
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

        public Reservation(int reservationID, string venueName, string spaceName, string reservationName,
            int numberOfAttendees, DateTime startDate, int reservationLength, int dailyRate)
        {
            ReservationID = reservationID;
            VenueName = venueName;
            SpaceName = spaceName;
            ReservationName = reservationName;
            NumberOfAttendees = numberOfAttendees;
            StartDate = startDate;
            ReservationLength = reservationLength;
            DailyRate = dailyRate;
            EndDate = startDate.AddDays(ReservationLength);
            
        }
    }
}
