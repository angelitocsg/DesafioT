using DesafioT.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DesafioT.Tests.Domain.Entities
{
    [TestClass]
    public class FlightTest
    {
        [TestMethod]
        public void ShouldReturnFalseIfAFieldIsNotFilled()
        {
            Flight flight = new Flight()
            {
                Id = "P21V63",
                Company = "",
                AirportFrom = "PMW",
                AirportTo = "VCP",
                Date = new DateTime(2019, 02, 11),
                DepartureTime = "06:00",
                ArrivalTime = "17:00",
                Price = 876.79m
            };

            bool Valid = flight.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldBeEqualIfAFieldIsNotFilledAndMessageIsCorrect()
        {
            var message = "Incorrect fill";

            Flight flight = new Flight()
            {
                Id = "P21V63",
                Company = "",
                AirportFrom = "PMW",
                AirportTo = "VCP",
                Date = new DateTime(2019, 02, 11),
                DepartureTime = "06:00",
                ArrivalTime = "17:00",
                Price = 876.79m
            };

            bool Valid = flight.IsValid();

            Assert.AreEqual(message, flight.ValidateMessage);
        }

        [TestMethod]
        public void ShouldReturnFalseIfAirportFromHasNot3Characters()
        {
            Flight flight = new Flight()
            {
                Id = "P21V63",
                Company = "UberAir",
                AirportFrom = "PMWW",
                AirportTo = "VCP",
                Date = new DateTime(2019, 02, 11),
                DepartureTime = "06:00",
                ArrivalTime = "17:00",
                Price = 876.79m
            };

            bool Valid = flight.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnFalseIfPriceIsNotGreaterThanZero()
        {
            Flight flight = new Flight()
            {
                Id = "P21V63",
                Company = "UberAir",
                AirportFrom = "PWW",
                AirportTo = "VCP",
                Date = new DateTime(2019, 02, 11),
                DepartureTime = "06:00",
                ArrivalTime = "17:00",
                Price = 0
            };

            bool Valid = flight.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnFalseIfArrivalDateIsLessThanDepartureDate()
        {
            Flight flight = new Flight()
            {
                Id = "P21V63",
                Company = "UberAir",
                AirportFrom = "PWW",
                AirportTo = "VCP",
                Date = new DateTime(2019, 02, 11),
                DepartureTime = "12:00",
                ArrivalTime = "08:00",
                Price = 876.79m
            };

            bool Valid = flight.IsValid();

            Assert.IsFalse(Valid);
        }
    }
}