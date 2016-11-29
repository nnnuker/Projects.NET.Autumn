using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Repositories;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using System.Linq;

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
            repository = new UserXmlRepository();

            user = new User()
            {
                Id = 1,
                FirstName = "Petr",
                LastName = "The greatest",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue} },
                Gender = GenderEnum.Male
            };

            repository.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            repository.Add(null);
        }

        [TestMethod]
        public void Add_User_Success()
        {
            var repository = new UserXmlRepository();

            User u = null;
            if (repository.Add(user) != null)
            {
                u = repository.GetByPredicate(us => us.Equals(user)).FirstOrDefault();
            }

            Assert.IsNotNull(u);
        }

        [TestMethod]
        public void Add_UserWhileAlreadyExists_False()
        {
            Assert.IsFalse(repository.Add(user) != null);
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            Assert.IsTrue(repository.Delete(user.Id));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Delete_InvalidId_ExceptionThrown()
        {
            repository.Delete(-50);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByPredicate_NullPredicate_ThrownException()
        {
            repository.GetByPredicate(null);
        }

        [TestMethod]
        public void GetByPredicate_RightPredicate_Users()
        {
            Assert.IsTrue(repository.GetByPredicate(u => u.FirstName == "Petr").Count > 0);
        }

        [TestMethod]
        public void GetByPredicate_InvalidPredicate_EmptyCollection()
        {
            Assert.IsTrue(repository.GetByPredicate(u => u.FirstName == "Vasily").Count == 0);
        }

        [TestMethod]
        public void Save_SaveAll_Success()
        {
            repository.Add(new User()
            {
                Id = 2,
                FirstName = "Petr",
                LastName = "The greatest",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
                Gender = GenderEnum.Male
            });

            repository.Add(new User()
            {
                Id = 3,
                FirstName = "Petr",
                LastName = "The greatest",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
                Gender = GenderEnum.Male
            });

            repository.Save();
        }

        [TestMethod]
        public void Load_LoadAll_Success()
        {
            repository.Load();

            Assert.IsTrue(repository.GetAll().Count == 3);
        }
    }
}
