using System;
using System.Collections.Generic;

namespace DesafioT.Domain.Entities
{
    public class Flight
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string AirportFrom { get; set; }
        public string AirportTo { get; set; }
        public DateTime Date { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
        public decimal Price { get; set; }
        public DateTime ArrivalDate => Convert.ToDateTime(Date.ToString("yyyy-MM-dd ") + ArrivalTime);
        public DateTime DepartureDate => Convert.ToDateTime(Date.ToString("yyyy-MM-dd ") + DepartureTime);
        public List<Flight> Stops { get; set; }

        public Flight()
        {
            Stops = new List<Flight>();
        }

        public string ValidateMessage = string.Empty;
        public bool IsValid()
        {
            bool valid = false;

            // Valida preenchimento
            valid = !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(Company)
                && !string.IsNullOrEmpty(AirportFrom) && !string.IsNullOrEmpty(AirportTo)
                && !string.IsNullOrEmpty(DepartureTime) && !string.IsNullOrEmpty(ArrivalTime);
            if (!valid) { ValidateMessage = "Incorrect fill"; return valid; }

            // Valida tamanha do campo
            if (valid) valid = Id.Length == 3;
            if (!valid) { ValidateMessage = "Id length must be equal to 3"; return valid; }

            // Valida preco > 0
            if (valid) valid = Price > 0;
            if (!valid) { ValidateMessage = "Price must be greater than 0"; return valid; }

            // Valida datas
            if (valid) valid = ArrivalDate > DepartureDate;
            if (!valid) { ValidateMessage = "Arrival data must be greater than Departure date"; return valid; }

            return valid;
        }
    }
}