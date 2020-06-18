using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Venue
    {
        //---------- PROPERTIES ----------------------------------------------------------------------------------------------------------------------------------

        public string VenueName { get; }
        public string City { get; }
        public string State { get; }
        public List<string> Category { get; set; }
        public string Description { get; }
        public string Location
        {
            get
            {
                return City + ", " + State;
            }
        }

        //---------- CONSTRUCTORS ------------------------------------------------------------------------------------------------------------------------

        public Venue(string venueName, string city, string state, List<string> category, string description)
        {
            VenueName = venueName;
            City = city;
            State = state;
            Category = category;
            Description = description;

        }
    }
}
