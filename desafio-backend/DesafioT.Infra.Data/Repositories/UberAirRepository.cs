using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DesafioT.Infra.Data.Repositories
{
    public class UberAirRepository : IFlightsRepository
    {
        private IEnumerable<Flight> Flights;

        public UberAirRepository()
        {
            ParseAndLoad();
        }

        public IEnumerable<Flight> GetFlights()
        {
            return Flights;
        }

        private enum COLUMNS
        {
            NumeroVoo = 0,
            AeroportoOrigem = 1,
            AeroportoDestino = 2,
            Data = 3,
            HorarioSaida = 4,
            HorarioChegada = 5,
            Preco = 6
        }

        private void ParseAndLoad()
        {
            var DataSource = Properties.Resources.uberair;
            var Lines = DataSource.Split("\r\n");
            var ParsedData = new List<Flight>();

            for (int i = 0; i < Lines.Count(); i++)
            {
                if (i == 0) { continue; }

                var columns = Lines[i].Split(',');

                var flight = new Flight()
                {
                    Id = columns[(int)COLUMNS.NumeroVoo],
                    Company = "UberAir",
                    AirportFrom = columns[(int)COLUMNS.AeroportoOrigem],
                    AirportTo = columns[(int)COLUMNS.AeroportoDestino],
                    Date = Convert.ToDateTime(columns[(int)COLUMNS.Data]),
                    DepartureTime = columns[(int)COLUMNS.HorarioSaida],
                    ArrivalTime = columns[(int)COLUMNS.HorarioChegada],
                    Price = Convert.ToDecimal(columns[(int)COLUMNS.Preco])
                };

                ParsedData.Add(flight);
            }

            Flights = ParsedData;
        }
    }
}
