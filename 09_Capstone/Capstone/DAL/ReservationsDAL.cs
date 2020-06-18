using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Capstone.DAL
{
    public class ReservationsDAL
    {
        private string connectionString;
        private object reader;

        public ReservationsDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public Reservation AddReservationToDataBase(Reservation reservation)
        {
            string sqlInsertCommand = "INSERT INTO reservation (space_id, number_of_attendees, start_date, end_date, reserved_for)" +
                                      "VALUES(@space_id, @number_of_attendees, @start_date, @end_date, @reserved_for);";

            string sqlQueryReservationID = "SELECT reservation_id FROM reservation WHERE reserved_for = @reserved_for AND start_date = @start_date;";

            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(sqlInsertCommand, conn);
                    cmd.Parameters.AddWithValue("@space_id", reservation.SpaceID);
                    cmd.Parameters.AddWithValue("@number_of_attendees", reservation.NumberOfAttendees);
                    cmd.Parameters.AddWithValue("@start_date", reservation.StartDate);
                    cmd.Parameters.AddWithValue("@end_date", reservation.EndDate);
                    cmd.Parameters.AddWithValue("@reserved_for", reservation.ReservationName);

                    int count = cmd.ExecuteNonQuery();

                    SqlCommand getIDcmd = new SqlCommand(sqlQueryReservationID, conn);
                    getIDcmd.Parameters.AddWithValue("@reserved_for", reservation.ReservationName);
                    getIDcmd.Parameters.AddWithValue("@start_date", reservation.StartDate);

                    SqlDataReader reader = getIDcmd.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        reservation.ReservationID = Convert.ToInt32(reader["reservation_id"]);
                    }
                    
                }
            }
            catch (Exception e)
            {
            }
            
            return reservation;
        }



    }
}
