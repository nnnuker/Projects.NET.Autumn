using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Services;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Infrastructure.SearchCriteria;

namespace MyServiceLibrary.Tests.ServicesTests
{
    [TestClass]
    public class UserServiceTests
    {
        private BasicUserService userService;
        private User user;

        [TestInitialize]
        public void Initialize()
        {
            userService = new BasicUserService();
            user = new User()
            {
                FirstName = "Petr",
                LastName = "The greatest",
                PersonalId = "PiterSaint",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[] { new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue } },
                Gender = GenderEnum.Male
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_NullUser_ExceptionThrown()
        {
            userService.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void Add_SameUser_UserAlreadyExists()
        {
            userService.Add(user);
            userService.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void Add_NullFirstName_ExceptionThrown()
        {
            user.FirstName = null;
            userService.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void Add_EmptyLastName_ExceptionThrown()
        {
            user.LastName = "";
            userService.Add(user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ExceptionThrown()
        {
            userService.Add(null);
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            user = userService.Add(user);
            userService.Delete(user);

            Assert.IsTrue(userService.GetAll().Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByPredicate_NullPredicate_ExceptionThrown()
        {
            userService.GetByPredicate(null);
        }

        [TestMethod]
        public void GetByPredicate_RightPredicate_UserReturns()
        {
            userService.Add(user);
            Assert.IsTrue(userService.GetByPredicate(new GenderCriteria { Gender = user.Gender}).Count == 1);
        }
    }
}
