using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            this.slaveService = new SlaveService(new BasicUserService());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Add_User_ExceptionThrown()
        {
            this.slaveService.Add(new User());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Delete_UserId_ExceptionThrown()
        {
            this.slaveService.Delete(new User());
        }
    }
}
