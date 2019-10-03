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
            var directFlights = GetDirectFligthsSameDay(airportFrom, airportTo, requiredDate);

            var stopFlights = GetStopFlights(airportFrom, airportTo, requiredDate);

            if (StopFlightsIsValid(stopFlights, airportFrom, airportTo))
            {
                var stopFlightsPossible = GetPossibleStopFlights(stopFlights, airportFrom, airportTo);
                return directFlights.Concat(stopFlightsPossible);
            }

            return directFlights;
        }

        private IEnumerable<Flight> GetDirectFligthsSameDay(string airportFrom, string airportTo, DateTime requiredDate)
        {
            return _flightsRepository
                 .GetFlights()
                 .Where(f => f.AirportFrom.Equals(airportFrom) && f.AirportTo.Equals(airportTo) && f.Date.Equals(requiredDate));
        }

        private IEnumerable<Flight> GetStopFlights(string airportFrom, string airportTo, DateTime requiredDate)
        {
            return _flightsRepository
                .GetFlights()
                // Não são diretos no mesmo dia
                .Where(f => !(f.AirportFrom.Equals(airportFrom) && f.AirportTo.Equals(airportTo) && f.Date.Equals(requiredDate)))
                // Origem e data requeridas
                .Where(f => (f.AirportFrom.Equals(airportFrom) && f.Date.Equals(requiredDate))
                // Destino requerido, origem independente e data maior igual a data requerida
                    || (f.AirportTo.Equals(airportTo) && f.Date >= requiredDate && f.Date <= requiredDate.AddDays(2)));
        }

        private bool StopFlightsIsValid(IEnumerable<Flight> stopFlights, string airportFrom, string airportTo)
        {
            // Deve possuir pelo pelo menos um voo de origem requerida
            return stopFlights.Where(f => f.AirportFrom.Equals(airportFrom)).Count() > 0
                // Deve possuir pelo pelo menos um voo de destino requerido
                && stopFlights.Where(f => f.AirportTo.Equals(airportTo)).Count() > 0 ? true : false;
        }

        private IEnumerable<Flight> GetPossibleStopFlights(IEnumerable<Flight> stopFlights, string airportFrom, string airportTo)
        {
            return FindFlights(stopFlights, airportFrom, airportTo).Where(f => f.Stops.Count() > 0);
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