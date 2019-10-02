using DesafioT.Domain.Entities;
using DesafioT.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace DesafioT.Infra.Data.Repositories
{
    public class FlightsRepository : IFlightsRepository
    {
        private IEnumerable<Flight> Flights;

        public FlightsRepository()
        {
            var UberAir = new UberAirRepository();
            var _99Planes = new NineNinePlanesRepository();

            Flights = UberAir.GetFlights().Concat(_99Planes.GetFlights());
        }

        public IEnumerable<Flight> GetFlights()
        {
            return Flights;
        }
    }
}
