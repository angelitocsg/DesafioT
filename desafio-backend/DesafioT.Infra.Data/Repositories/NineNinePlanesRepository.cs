using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Repositories;
using DesafioT.Infra.Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DesafioT.Infra.Data.Repositories
{
    public class NineNinePlanesRepository : IFlightsRepository
    {
        private IEnumerable<Flight> Flights;
        
        public NineNinePlanesRepository()
        {
            ParseAndLoad();
        }

        public IEnumerable<Flight> GetFlights()
        {
            return Flights;
        }

        private void ParseAndLoad()
        {
            var DataSource = Properties.Resources._99planes;
            var DataJson = JArray.Parse(DataSource);
            var ParsedData = new List<Flight>();

            foreach (var line in DataJson)
            {
                var model = line.ToObject<Voo>();

                var flight = new Flight()
                {
                    Id = model.voo,
                    Company = "99 Planes",
                    AirportFrom = model.origem,
                    AirportTo = model.destino,
                    Date = Convert.ToDateTime(model.data_saida),
                    DepartureTime = model.saida,
                    ArrivalTime = model.chegada,
                    Price = Convert.ToDecimal(model.valor)
                };

                ParsedData.Add(flight);
            }

            Flights = ParsedData;
        }
    }
}
