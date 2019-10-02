using System;
using System.Collections.Generic;

namespace DesafioT.Domain.Entities
{
    public class Flight
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public DateTime Date { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public DateTime ArrivalDate => Convert.ToDateTime(Date.ToString("yyyy-MM-dd ") + ArrivalTime);
        public DateTime DepartureDate => Convert.ToDateTime(Date.ToString("yyyy-MM-dd ") + DepartureTime);
        public List<Flight> Stops { get; set; }

        public Flight()
        {
            Stops = new List<Flight>();
        }
    }
}