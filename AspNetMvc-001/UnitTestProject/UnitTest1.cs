using System;
using System.Linq;
using AspNetMvc_001.Controllers;
using AspNetMvc_001.Models;
using AspNetMvc_001.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Moq;
using System.Web.Mvc;


namespace UnitTestProject
{
    [TestFixture]
    public class UnitTest1
    {

        [Test]
        public void TestMethod1()
        {
            //Arrange
            var Context = new SampleContext();
            var customers = Context.Orders.ToList();

            

            Mock<IQueryable<Order2>> mock = new Mock<IQueryable<Order2>>();
            mock.Setup(m => m).Returns(new Order2[]
            {
                new Order2() {Description = "D1", ProductName = "PN1", Quantity = 1},
                new Order2() {Description = "D2", ProductName = "PN2", Quantity = 2}
            }.AsQueryable());

            ShopController shopController = new ShopController();
            var v = shopController.Index();
            // var viewResult = Assert.IsInstanceOfType<>()
            var d = v;

            Assert.AreEqual(d,v);
        }
    }
}
