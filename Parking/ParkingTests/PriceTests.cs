using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Parking.Tests
{
    [TestFixture]
    public class PriceTests
    {
        private Price price;

        [Test]
        public void AddTariffTest()
        {
            // Arrange
            price = new Price();
            Type t = typeof(Price);

            // Act
            price.AddTariff(new Tariff("3-hour", 180, 12));

            // получить закрытое свойство, используя рефлексию типов 
            IList<Tariff> result = (IList<Tariff>)t.InvokeMember("Tariffs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, null, price, null);

            // Assert
            Assert.AreEqual(result.Count, 1);
        }

        [Test]
        public void RemoveTariffTest()
        {
            // Arrange
            Type t = typeof(Price);
            SetPrice();

            // получить закрытое свойство, используя рефлексию типов 
            IList<Tariff> result = (IList<Tariff>)t.InvokeMember("Tariffs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, null, price, null);

            // Act
            Assert.AreEqual(result.Count, 4);
            price.RemoveTariff(new Tariff("3-hour", 180, 12));
            result = (IList<Tariff>)t.InvokeMember("Tariffs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty, null, price, null);

            // Assert
            Assert.AreEqual(result.Count, 4);
        }

        [Test]
        public void GetTariffByNameTest()
        {
            // Arrange
            price.AddTariff(new Tariff("3-hour", 180, 12));

            // Act
            var tariff = price.GetTariff("3-hour");

            // Assert
            Assert.AreEqual(tariff.Minutes, 180);
        }

        [Test]
        public void GetTariffByMinuteTest()
        {
            // Arrange
            SetPrice();

            // Act
            var tariff1 = price.GetTariff(55);
            var tariff2 = price.GetTariff(110);
            var tariff3 = price.GetTariff(300);
            var tariff4 = price.GetTariff(600);
            var tariff5 = price.GetTariff(1500);


            // Assert
            Assert.AreEqual(tariff1.Minutes, 60);
            Assert.AreEqual(tariff2.Minutes, 180);
            Assert.AreEqual(tariff3.Minutes, 480);
            Assert.AreEqual(tariff4.Minutes, 1440);
            Assert.IsNull(tariff5);

        }

        [Test]
        public void GetTariffByHourMinuteTest()
        {
            // Arrange
            SetPrice();

            // Act
            var tariff0 = price.GetTariff(0, 55);
            var tariff1 = price.GetTariff(2, 55);
            var tariff2 = price.GetTariff(3, 10);
            var tariff3 = price.GetTariff(8, 5);
            

            // Assert
            Assert.AreEqual(tariff0.Minutes, 60);
            Assert.AreEqual(tariff1.Minutes, 180);
            Assert.AreEqual(tariff2.Minutes, 480);
            Assert.AreEqual(tariff3.Minutes, 1440);
            
        }


        [Test]
        public void GetDaylyTariffTest()
        {
            // Arrange
            SetPrice();

            // Act
            var tariff1 = price.GetDailyTariff();

            // Assert
            Assert.AreEqual(tariff1.Minutes, 1440);

        }

        private void SetPrice()
        {
            price = new Price();
            price.AddTariff(new Tariff("1-hour", 60, 5));
            price.AddTariff(new Tariff("3-hour", 180, 12));
            price.AddTariff(new Tariff("8-hour", 480, 25));
            price.AddTariff(new Tariff("24-hour", 1440, 35));
        }

    }
}