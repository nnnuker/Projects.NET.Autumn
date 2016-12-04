using System;
using System.IO;
using System.Linq;
using System.Net;
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
            master = new DataSpreaderService(
                new MasterService<User>(
                    new BasicUserService(
                        new UserMemoryRepository(new IdGenerator(), new XmlUserRepositorySaver()), 
                        new UserValidator())));

            slave = new DataSpreaderService(new SlaveService<User>(new BasicUserService()));
            slave1 = new DataSpreaderService(new SlaveService<User>(new BasicUserService()));

            var sender = new OneAppDataSender("1", slave, slave1);
            var receiver = new OneAppDataReceiver("2", master);

            master.AddDataSpreader(sender);
            slave.AddDataSpreader(receiver);
            slave1.AddDataSpreader(receiver);
        }

        [TestMethod]
        public void MasterAdd_User_SlavesReceived()
        {
            master.Add(user);

            var result = slave.GetAll();
            var result1 = slave1.GetAll();

            Assert.IsTrue(result[0].Equals(user));
            Assert.IsTrue(result1[0].Equals(user));
        }

        [TestMethod]
        public void MasterLoad_Users_SlavesReceived()
        {
            master.Load();

            var masterCollection = master.GetAll().ToList();

            var result = slave.GetAll().ToList();
            var result1 = slave1.GetAll().ToList();

            CollectionAssert.AreEqual(masterCollection, result);
            CollectionAssert.AreEqual(masterCollection, result1);
        }

        [TestMethod]
        public void MasterRemoveDataSpreader_Spreader_Removed()
        {
            master.RemoveDataSpreader("1");

            master.Add(user);

            var result = slave.GetAll();

            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void MasterAdd_Network_SlavesReceived()
        {
            var endPoint = new IPEndPoint(IPAddress.Loopback, 8081);

            var networkReceiver = new NetworkDataReceiver("3", endPoint);
            var networkSender = new NetworkDataSender("4", endPoint);

            slave.AddDataSpreader(networkReceiver);
            slave1.AddDataSpreader(networkReceiver);
            master.AddDataSpreader(networkSender);

            master.Add(user);

            var result = slave.GetAll();
            var result1 = slave1.GetAll();

            Assert.IsTrue(result[0].Equals(user));
            Assert.IsTrue(result1[0].Equals(user));
        }
    }
}
