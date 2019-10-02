using System;
using System.Collections.Generic;
using System.Linq;
using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Repositories;
using DesafioT.Domain.Interfaces.Services;
using Newtonsoft.Json.Linq;

namespace DesafioT.Domain.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly IAirportsRepository _airportsRepository;
        private readonly IFlightsRepository _flightsRepository;

        public FlightsService(IAirportsRepository airportsRepository, IFlightsRepository flightsRepository)
        {
            _airportsRepository = airportsRepository;
            _flightsRepository = flightsRepository;
        }

        public IEnumerable<Flight> AvailableFlights(string airportFrom, string airportTo, DateTime requiredDate)
        {
            var directFlights = _flightsRepository
                .GetFlights()
                // Voos diretos e no mesmo dia
                .Where(f => f.AirportFrom.Equals(airportFrom) && f.AirportTo.Equals(airportTo) && f.Date.Equals(requiredDate));

            var stopFlights = _flightsRepository
                .GetFlights()
                // Não são diretos no mesmo dia
                .Where(f => !(f.AirportFrom.Equals(airportFrom) && f.AirportTo.Equals(airportTo) && f.Date.Equals(requiredDate)))
                // Origem e data requeridas
                .Where(f => (f.AirportFrom.Equals(airportFrom) && f.Date.Equals(requiredDate))
                // Destino requerido, origem independente e data maior igual a data requerida
                    || (f.AirportTo.Equals(airportTo) && f.Date >= requiredDate && f.Date <= requiredDate.AddDays(2)));

            // Deve possuir pelo pelo menos um voo de origem requerida
            stopFlights = stopFlights.Where(f => f.AirportFrom.Equals(airportFrom)).Count() > 0
                // Deve possuir pelo pelo menos um voo de destino requerido
                && stopFlights.Where(f => f.AirportTo.Equals(airportTo)).Count() > 0 ? stopFlights : null;

            var stopFlightsValid = stopFlights;

            if (stopFlights != null)
            {
                var allFlights = stopFlights
                    .Select(f => $"{f.AirportFrom};{f.AirportTo};{f.Company};{f.DepartureTime};{f.ArrivalTime}");

                var csv = string.Join("\r\n", allFlights);

                stopFlightsValid = FindFlights(stopFlights, airportFrom, airportTo).Where(f => f.Stops.Count() > 0);
            }

            var flights = stopFlightsValid != null ? directFlights.Concat(stopFlightsValid) : directFlights;

            return flights;
        }

        private IEnumerable<Flight> FindFlights(IEnumerable<Flight> flights, string airportFrom, string airportTo)
        {
            var fromFlights = flights.Where(f => f.AirportFrom.Equals(airportFrom));
            var validFlights = new List<Flight>();

            foreach (var flight in fromFlights)
            {
                // É voo direto
                if (flight.AirportTo.Equals(airportTo))
                {
                    validFlights.Add(flight);
                    continue;
                }

                // Não é voo direto
                if (!flight.AirportTo.Equals(airportTo))
                {
                    var found = FindFlights(flights, flight.AirportTo, airportTo);

                    foreach (var ff in found)
                    {
                        var priorArrival = flight.ArrivalDate;
                        var nextDeparture = ff.DepartureDate;
                        var limit = priorArrival.AddHours(12);

                        // O horário de saída é antes da chegada do anterior?
                        if (nextDeparture <= priorArrival)
                            continue;

                        // O horário de saída é menos de 12 horas antes do anterior?
                        if (nextDeparture > limit)
                            continue;

                        flight.Stops.Add(ff);
                    }

                    // Voo valido se possuir escala
                    if (flight.Stops.Count() > 0)
                        validFlights.Add(flight);
                }
            }

            return validFlights;
        }

        public IEnumerable<Airport> GetAirports()
        {
            return _airportsRepository.GetAll();
        }
    }
}