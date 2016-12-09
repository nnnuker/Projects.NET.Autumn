using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Infrastructure.UserValidators;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Replication.DataSpreader;
using MyServiceLibrary.Repositories;
using MyServiceLibrary.Repositories.StateSavers;
using MyServiceLibrary.Services;

namespace MyServiceLibrary.Tests.ServicesTests
{
    [TestClass]
    public class DataSpreaderServiceTests
    {
        private DataSpreaderService master;
        private DataSpreaderService slave;
        private DataSpreaderService slave1;

        private User user = new User()
        {
            FirstName = "Petr",
            LastName = "The greatest",
            PersonalId = "PiterSaint",
            DateOfBirth = DateTime.MinValue,
            Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
            Gender = GenderEnum.Male
        };

        [TestInitialize]
        public void Initialize()
        {
            this.master = new DataSpreaderService(
                new MasterService(
                    new BasicUserService(
                        new UserMemoryRepository(new IdGenerator(), new XmlUserRepositorySaver()), 
                        new UserValidator())));

            this.slave = new DataSpreaderService(new SlaveService(new BasicUserService()));
            this.slave1 = new DataSpreaderService(new SlaveService(new BasicUserService()));

            var sender = new OneAppDataSender("1", this.slave, this.slave1);
            var receiver = new OneAppDataReceiver("2", this.master);

            this.master.AddDataSpreader(sender);
            this.slave.AddDataSpreader(receiver);
            this.slave1.AddDataSpreader(receiver);
        }

        [TestMethod]
        public void MasterAdd_User_SlavesReceived()
        {
            this.master.Add(this.user);

            var result = this.slave.GetAll();
            var result1 = this.slave1.GetAll();

            Assert.IsTrue(result[0].Equals(this.user));
            Assert.IsTrue(result1[0].Equals(this.user));
        }

        [TestMethod]
        public void MasterLoad_Users_SlavesReceived()
        {
            this.master.Load();

            var masterCollection = this.master.GetAll().ToList();

            var result = this.slave.GetAll().ToList();
            var result1 = this.slave1.GetAll().ToList();

            CollectionAssert.AreEqual(masterCollection, result);
            CollectionAssert.AreEqual(masterCollection, result1);
        }

        [TestMethod]
        public void MasterRemoveDataSpreader_Spreader_Removed()
        {
            this.master.RemoveDataSpreader("1");

            this.master.Add(this.user);

            var result = this.slave.GetAll();

            Assert.IsTrue(result.Count == 0);
        }
    }
}
