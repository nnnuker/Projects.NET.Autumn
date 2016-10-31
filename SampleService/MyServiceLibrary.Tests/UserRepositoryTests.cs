using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Exceptions;

namespace MyServiceLibrary.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UserMemoryRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new UserMemoryRepository();

            repository.Add(new User
            {
                FirstName = "Petr",
                LastName = "The greatest",
                DateOfBirth = DateTime.Now
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            repository.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void Add_NullFirstName_ExceptionThrown()
        {
            repository.Add(new User
            {
                FirstName = null,
                LastName = "The greatest",
                DateOfBirth = DateTime.Now
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void Add_EmptyLastName_ExceptionThrown()
        {
            repository.Add(new User
            {
                FirstName = "Petr",
                LastName = "",
                DateOfBirth = DateTime.Now
            });
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            var user = repository.Add(new User
            {
                FirstName = "Ivan",
                LastName = "The Terrible",
                DateOfBirth = DateTime.Now
            });

            Assert.IsTrue(repository.Delete(user));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ExceptionThrown()
        {
            repository.Delete(null);
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
        public void GetByPredicate_BadPredicate_EmptyCollection()
        {
            Assert.IsTrue(repository.GetByPredicate(u => u.FirstName == "Vasily").Count == 0);
        }
    }
}
