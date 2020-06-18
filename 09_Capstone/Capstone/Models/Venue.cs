using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Venue
    {
        //---------- PROPERTIES ----------------------------------------------------------------------------------------------------------------------------------

        public string VenueName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public List<string> Category { get; set; } = new List<string>();
        public string Description { get; set; }
        public string Location
        {
            get
            {
                return City + ", " + State;
            }
        }

        //---------- CONSTRUCTORS ------------------------------------------------------------------------------------------------------------------------
        public Venue()
        {
        }
        public Venue(string name)
        {
            VenueName = name;
        }

        //public Venue(string venueName, string city, string state, /*List<string> category,*/ string description)
        //{
        //    VenueName = venueName;
        //    City = city;
        //    State = state;
        //    Category = category;
        //    Description = description;

        //}
    }
}
