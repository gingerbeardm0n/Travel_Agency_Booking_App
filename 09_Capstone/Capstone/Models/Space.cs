using System;
using System.Collections.Generic;
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
        public int TotalCost { get; }
        public DateTime OpenFrom { get; }
        public DateTime OpenTo { get; }

        //---------- CONSTRUCTORS ------------------------------------------------------------------------------------------------------------------------

        public Space(int spaceID, string spaceName, int dailyRate, int maxOccupancy, int accessability, int reservationLength, DateTime openFrom, DateTime openTo)
        {
            SpaceID = spaceID;
            SpaceName = spaceName;
            DailyRate = dailyRate;
            MaxOccupancy = maxOccupancy;
            Accessablity = accessability == 1;
            TotalCost = reservationLength * DailyRate;
            OpenFrom = openFrom;
            OpenTo = openTo;

        }

    }


}
