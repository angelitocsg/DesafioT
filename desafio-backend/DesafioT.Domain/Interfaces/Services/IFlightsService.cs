using DesafioT.Domain.Entities;
using System;
using System.Collections.Generic;

namespace DesafioT.Domain.Interfaces.Services
{
    public interface IFlightsService
    {
        IEnumerable<Flight> AvailableFlights(string airportFrom, string airportTo, DateTime requiredDate);
        IEnumerable<Airport> GetAirports();
    }
}