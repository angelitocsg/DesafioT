﻿using DesafioT.Domain.Entities;
using System.Collections.Generic;

namespace DesafioT.Domain.Interfaces.Repositories
{
    public interface IAirportsRepository
    {
        IEnumerable<Airport> GetAll();
    }
}
