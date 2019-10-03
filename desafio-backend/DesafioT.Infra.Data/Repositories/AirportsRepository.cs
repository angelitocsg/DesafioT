using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Repositories;
using DesafioT.Infra.Data.Data;
using DesafioT.Infra.Data.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Resources;

namespace DesafioT.Infra.Data.Repositories
{
    public class AirportsRepository : IAirportsRepository
    {
        private IEnumerable<Airport> Airports;

        public AirportsRepository()
        {
            ParseAndLoad();
        }

        public IEnumerable<Airport> GetAll()
        {
            return Airports;
        }

        private void ParseAndLoad()
        {
            var DataSource = Utils.ReadFile("data/airports.json");
            var DataJson = JArray.Parse(DataSource);
            var ParsedData = new List<Airport>();

            foreach (var line in DataJson)
            {
                var model = line.ToObject<Aeroporto>();

                var airport = new Airport()
                {
                    Id = model.aeroporto,
                    Name = model.nome,
                    City = model.cidade
                };

                ParsedData.Add(airport);
            }

            Airports = ParsedData;
        }
    }
}
