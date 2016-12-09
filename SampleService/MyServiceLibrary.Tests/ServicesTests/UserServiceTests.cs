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
            this.userService = new BasicUserService();
            this.user = new User()
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
            this.userService.Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UserAlreadyExistsException))]
        public void Add_SameUser_UserAlreadyExists()
        {
            this.userService.Add(this.user);
            this.userService.Add(this.user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void Add_NullFirstName_ExceptionThrown()
        {
            this.user.FirstName = null;
            this.userService.Add(this.user);
        }

        [TestMethod]
        [ExpectedException(typeof(UserValidationException))]
        public void Add_EmptyLastName_ExceptionThrown()
        {
            this.user.LastName = string.Empty;
            this.userService.Add(this.user);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Delete_NullUser_ExceptionThrown()
        {
            this.userService.Add(null);
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            this.user = this.userService.Add(this.user);
            this.userService.Delete(this.user);

            Assert.IsTrue(this.userService.GetAll().Count == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetByPredicate_NullPredicate_ExceptionThrown()
        {
            this.userService.GetByPredicate(null);
        }

        [TestMethod]
        public void GetByPredicate_RightPredicate_UserReturns()
        {
            this.userService.Add(this.user);
            Assert.IsTrue(this.userService.GetByPredicate(new GenderCriteria { Gender = this.user.Gender }).Count == 1);
        }
    }
}
