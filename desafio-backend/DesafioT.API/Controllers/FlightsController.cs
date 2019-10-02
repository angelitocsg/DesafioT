using DesafioT.API.ViewModel;
using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioT.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightsService _flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        [HttpGet]
        [Route("AvailableFlights/{airportFrom}/{airportTo}/{requiredDate}")]
        public IActionResult AvailableFlights(string airportFrom, string airportTo, DateTime requiredDate)
        {
            if (string.IsNullOrEmpty(airportFrom) || string.IsNullOrEmpty(airportTo) || requiredDate == DateTime.MinValue)
                return BadRequest("Something is wrong.");

            var flights = _flightsService.AvailableFlights(airportFrom, airportTo, requiredDate);

            if (flights.Count() == 0)
                return NotFound("Flights not found.");

            return ParseFlightsResults(flights, airportFrom, airportTo);
        }

        private IActionResult ParseFlightsResults(IEnumerable<Flight> flights, string airportFrom, string airportTo)
        {
            return Ok(from a in flights
                      select new FlightsViewModel()
                      {
                          AirportFrom = airportFrom,
                          AirportTo = airportTo,
                          DepartureDate = a.DepartureDate,
                          ArrivalDate = a.Stops.Count() > 0 ? a.Stops.Max(x => x.ArrivalDate) : a.ArrivalDate,
                          Stops = (a.Stops.Count() > 0 ? a.Stops.Select(x => new StopsViewModel()
                          {
                              Id = x.Id,
                              AirportFrom = x.AirportFrom,
                              AirportTo = x.AirportTo,
                              DepartureDate = x.DepartureDate,
                              ArrivalDate = x.ArrivalDate,
                              Company = x.Company,
                              Price = x.Price
                          })
                          .Append(new StopsViewModel()
                          {
                              Id = a.Id,
                              AirportFrom = a.AirportFrom,
                              AirportTo = a.AirportTo,
                              DepartureDate = a.DepartureDate,
                              ArrivalDate = a.ArrivalDate,
                              Company = a.Company,
                              Price = a.Price
                          }) :
                          new List<StopsViewModel>()
                          {
                                 new StopsViewModel()
                                 {
                                     Id = a.Id,
                                     AirportFrom = a.AirportFrom,
                                     AirportTo = a.AirportTo,
                                     DepartureDate = a.DepartureDate,
                                     ArrivalDate = a.ArrivalDate,
                                     Company = a.Company,
                                     Price = a.Price
                                 }
                          })
                          .OrderBy(o => o.DepartureDate).ToList()
                      });
        }

        [HttpGet]
        [Route("Airports")]
        public IActionResult Airports()
        {
            var airports = _flightsService.GetAirports();

            if (airports.Count() == 0)
                return NotFound("Airports list is empty.");

            return Ok(airports);
        }
    }
}
