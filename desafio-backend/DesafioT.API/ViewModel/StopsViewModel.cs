using System;

namespace DesafioT.API.ViewModel
{
  public class StopsViewModel
  {
    public string Id { get; set; }
    public string Company { get; set; }
    public string AirportFrom { get; set; }
    public string AirportTo { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public decimal Price { get; set; }
  }
}