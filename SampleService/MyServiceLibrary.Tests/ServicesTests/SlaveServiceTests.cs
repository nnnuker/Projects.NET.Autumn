using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Services;

namespace MyServiceLibrary.Tests.ServicesTests
{
    [TestClass]
    public class SlaveServiceTests
    {
        private SlaveService slaveService;

        [TestInitialize]
        public void Initialize()
        {
            slaveService = new SlaveService(new BasicUserService());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_User_ExceptionThrown()
        {
            slaveService.Add(new User());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_UserId_ExceptionThrown()
        {
            slaveService.Delete(new User());
        }
    }
}
