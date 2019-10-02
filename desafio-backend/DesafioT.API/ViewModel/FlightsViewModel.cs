using DesafioT.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DesafioT.API.ViewModel
{
    public class FlightsViewModel
    {
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public List<StopsViewModel> Stops { get; set; }

        public FlightsViewModel()
        {
            Stops = new List<StopsViewModel>();
        }
    }
}
