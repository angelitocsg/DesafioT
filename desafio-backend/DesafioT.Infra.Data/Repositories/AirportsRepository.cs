using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Repositories;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace DesafioT.Infra.Data.Repositories
{
    public class AirportsRepository : IAirportsRepository
    {
        private IEnumerable<Airport> Airports;

        private struct Aeroporto
        {
            public string nome { get; set; }
            public string aeroporto { get; set; }
            public string cidade { get; set; }
        }

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
            var DataSource = Properties.Resources.airports;
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
