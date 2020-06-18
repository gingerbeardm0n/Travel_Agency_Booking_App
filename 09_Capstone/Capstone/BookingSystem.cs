using System;
using System.Collections.Generic;
using System.Text;
using Capstone.DAL;
using Capstone.Models;

namespace Capstone
{
    public class BookingSystem
    {
        private string connectionString;
        VenuesDAL venueIO;
        ReservationsDAL reservationIO;

        public BookingSystem(string dbConnectionString)
        {
            connectionString = dbConnectionString;
            venueIO = new VenuesDAL(connectionString);
            reservationIO = new ReservationsDAL(connectionString);
        }

        public Reservation MakeReservation(List<Space> availableSpaces, int selectedSpaceID, int numberOfGuests, DateTime startDate, int eventLength, string venueName, string reservationName)
        {
            Space selectedSpace = null;
            foreach(Space space in availableSpaces)
            {
                if (space.SpaceID == selectedSpaceID)
                {
                    selectedSpace = space;
                }
            }
            Reservation reservation = new Reservation(selectedSpace.SpaceID, venueName, selectedSpace.SpaceName, reservationName, numberOfGuests, startDate, eventLength, selectedSpace.DailyRate);
            return reservationIO.AddReservationToDataBase(reservation);
        }
    }
}
