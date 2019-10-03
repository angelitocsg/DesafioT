using DesafioT.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesafioT.Tests.Domain.Entities
{
    [TestClass]
    public class AirportTest
    {
        [TestMethod]
        public void ShouldReturnFalseIfIdIsNotFilled()
        {
            Airport airport = new Airport()
            {
                City = "São Paulo",
                Name = "Congonhas"
            };

            bool Valid = airport.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnFalseIfCityIsNotFilled()
        {
            Airport airport = new Airport()
            {
                Id = "CGH",
                Name = "Congonhas"
            };

            bool Valid = airport.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnFalseIfNameIsNotFilled()
        {
            Airport airport = new Airport()
            {
                Id = "CGH",
                City = "São Paulo"
            };

            bool Valid = airport.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnFalseIfIdHasMoreThan3Characters()
        {
            Airport airport = new Airport()
            {
                Id = "CGHH",
                City = "São Paulo",
                Name = "Congonhas"
            };

            bool Valid = airport.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnFalseIfIdHasLessThan3Characters()
        {
            Airport airport = new Airport()
            {
                Id = "CG",
                City = "São Paulo",
                Name = "Congonhas"
            };

            bool Valid = airport.IsValid();

            Assert.IsFalse(Valid);
        }

        [TestMethod]
        public void ShouldReturnTrueIfAllFieldsAreFilledAndIdHas3Characters()
        {
            Airport airport = new Airport()
            {
                Id = "CGH",
                City = "São Paulo",
                Name = "Congonhas"
            };

            bool Valid = airport.IsValid();

            Assert.IsTrue(Valid);
        }
    }
}
