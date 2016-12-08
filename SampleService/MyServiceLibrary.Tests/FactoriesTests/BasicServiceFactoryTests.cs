using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Services.Factories;
using System.Collections.Generic;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Infrastructure.SearchCriteria;

namespace MyServiceLibrary.Tests.FactoriesTests
{
    [TestClass]
    public class BasicServiceFactoryTests
    {
        //private BasicServiceFactory factory;
        //private DataSpreaderService master;

        //private User user = new User()
        //{
        //    FirstName = "Petr",
        //    LastName = "The greatest",
        //    PersonalId = "PiterSaint",
        //    DateOfBirth = DateTime.MinValue,
        //    Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
        //    Gender = GenderEnum.Male
        //};

        //[TestInitialize]
        //public void Initialize()
        //{
        //    factory = new BasicServiceFactory();

        //    var services = factory.RunServices();

        //    master = services.FirstOrDefault(s => s.ServiceMode == Interfaces.Replication.ServiceModeEnum.Master);
        //}

        //[TestMethod]
        //public void RunServices_All_Success()
        //{
        //    List<DataSpreaderService> services = factory.RunServices();

        //    Assert.IsTrue(services.Count == 1);
        //}

        //[TestMethod]
        //public void Add_User_UserAdded()
        //{
        //    master.Add(user);
        //}

        //[TestMethod]
        //public void GetAll_User_Success()
        //{
        //    master.Add(user);

        //    Assert.AreEqual(user, master.GetAll().First());
        //}

        //[TestMethod]
        //public void Remove_User_UserRemoved()
        //{
        //    var result = master.Add(user);

        //    Assert.AreEqual(user, master.GetAll().First());

        //    master.Delete(result);

        //    Assert.AreEqual(0, master.GetAll().Count);
        //}

        //[TestMethod]
        //public void GetByPredicate()
        //{
        //    master.Add(user);

        //    var result = master.GetByPredicate(new NameCriteria { FirstName = user.FirstName, LastName = user.LastName });

        //    Assert.AreEqual(user, result.First());
        //}
    }
}
