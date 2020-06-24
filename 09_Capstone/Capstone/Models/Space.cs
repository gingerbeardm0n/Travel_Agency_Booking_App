using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Capstone.Models
{
    public class Space
    {
        //---------- PROPERTIES ----------------------------------------------------------------------------------------------------------------------------------

        public int SpaceID { get; }
        public string SpaceName { get; }
        public int DailyRate { get; }
        public int MaxOccupancy { get; }
        public bool Accessablity { get; }
        public int ReservationLength { get; set; }//JNB removed set, trying to get eventLength
        public int TotalCost
        {
            get
            {
               return DailyRate * ReservationLength;
            }
        }
        public string OpenFrom { get; }
        public string OpenTo { get; }

        //---------- CONSTRUCTORS ------------------------------------------------------------------------------------------------------------------------

        public Space(int spaceID, string spaceName, Decimal dailyRate, int maxOccupancy, bool accessability, int openFrom, int openTo)
        {
            SpaceID = spaceID;
            SpaceName = spaceName;
            DailyRate = (int)dailyRate;
            MaxOccupancy = maxOccupancy;
            Accessablity = accessability;
            if (openFrom != 0 && openTo != 0)
            {
                OpenFrom = new DateTime(2020, openFrom, 1).ToString("MMM", CultureInfo.InvariantCulture);
                OpenTo = new DateTime(2020, openTo, 1).ToString("MMM", CultureInfo.InvariantCulture);
            }
            else
            {
                OpenFrom = "";
                OpenTo = "";
            }
        }

        public Space(int spaceID, string spaceName, Decimal dailyRate, int maxOccupancy, bool accessability)
        {
            SpaceID = spaceID;
            SpaceName = spaceName;
            DailyRate = (int)dailyRate;
            MaxOccupancy = maxOccupancy;
            Accessablity = accessability;
        }


    }


}
