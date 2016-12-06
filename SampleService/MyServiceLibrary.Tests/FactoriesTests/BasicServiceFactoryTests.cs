using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Services.Factories;

namespace MyServiceLibrary.Tests.FactoriesTests
{
    [TestClass]
    public class BasicServiceFactoryTests
    {
        [TestMethod]
        public void GetServices_All_Success()
        {
            var factory = new BasicServiceFactory();

            var services = factory.RunServices();
            
            Assert.IsTrue(services.Count == 3);
        }
    }
}
