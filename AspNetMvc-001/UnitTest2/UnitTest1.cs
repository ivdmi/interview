using System;
using System.Linq;
using AspNetMvc_001.Controllers;
using AspNetMvc_001.Models;
using AspNetMvc_001.Data;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace UnitTest2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var Context = new SampleContext();
            var customers = Context.Orders.ToList();

            ShopController shopController = new ShopController();
            var v = shopController.Index();
            // var viewResult = Assert.IsInstanceOfType<>()
            var d = v;

            Assert.AreEqual(d, v);

        }
    }
}
