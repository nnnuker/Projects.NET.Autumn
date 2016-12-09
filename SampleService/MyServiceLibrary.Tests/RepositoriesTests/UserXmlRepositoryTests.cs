using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Repositories;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Repositories.StateSavers;

namespace MyServiceLibrary.Tests.RepositoriesTests
{
    [TestClass]
    public class UserXmlRepositoryTests
    {
        private IRepository<User> repository;
        private User user;

        [TestInitialize]
        public void Initialize()
        {
            this.repository = new UserMemoryRepository(new IdGenerator(), new XmlUserRepositorySaver());

            this.user = new User()
            {
                Id = 1,
                FirstName = "Petr",
                LastName = "The greatest",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
                Gender = GenderEnum.Male
            };

            this.repository.Add(this.user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            this.repository.Add(null);
        }

        [TestMethod]
        public void Add_User_Success()
        {
            var repository = new UserMemoryRepository();

            User u = null;
            if (repository.Add(this.user) != null)
            {
                u = repository.GetByPredicate(us => us.Equals(this.user)).FirstOrDefault();
            }

            Assert.IsNotNull(u);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void Add_UserWhileAlreadyExists_ExceptionThrow()
        {
            this.repository.Add(this.user);
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            Assert.IsTrue(this.repository.Delete(this.user.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Delete_InvalidId_ExceptionThrown()
        {
            this.repository.Delete(-50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByPredicate_NullPredicate_ThrownException()
        {
            this.repository.GetByPredicate(null);
        }

        [TestMethod]
        public void GetByPredicate_RightPredicate_Users()
        {
            Assert.IsTrue(this.repository.GetByPredicate(u => u.FirstName == "Petr").Count > 0);
        }

        [TestMethod]
        public void GetByPredicate_InvalidPredicate_EmptyCollection()
        {
            Assert.IsTrue(this.repository.GetByPredicate(u => u.FirstName == "Vasily").Count == 0);
        }

        [TestMethod]
        public void Save_SaveAll_Success()
        {
            this.repository.Add(new User()
            {
                Id = 2,
                FirstName = "Aliaksandr",
                LastName = "I",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
                Gender = GenderEnum.Male
            });

            this.repository.Add(new User()
            {
                Id = 3,
                FirstName = "Petra",
                LastName = "The greatest",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Europe", Start = DateTime.MinValue, End = DateTime.MaxValue } },
                Gender = GenderEnum.Female
            });

            this.repository.Save();
        }

        [TestMethod]
        public void Load_LoadAll_Success()
        {
            this.repository.Load();

            Assert.IsTrue(this.repository.GetAll().Count == 3);
        }
    }
}
