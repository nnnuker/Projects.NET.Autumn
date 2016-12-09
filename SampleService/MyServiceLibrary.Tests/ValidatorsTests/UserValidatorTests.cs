using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Infrastructure.UserValidators;

namespace MyServiceLibrary.Tests.ValidatorsTests
{
    [TestClass]
    public class UserValidatorTests
    {
        private User user;
        private UserValidator validator = new UserValidator();

        [TestInitialize]
        public void Initialize()
        {
            this.user = new User()
            {
                FirstName = "Matt",
                LastName = "Murdock",
                PersonalId = "Hell",
                DateOfBirth = DateTime.MinValue,
                Visas = new VisaRecord[0],
                Gender = GenderEnum.Female
            };
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsValid_NUllUser_Exception()
        {
            this.validator.IsValid(null);
        }

        [TestMethod]
        public void IsValid_VisasNull_False()
        {
            this.user.Visas = null;

            Assert.IsFalse(this.validator.IsValid(this.user));
        }

        [TestMethod]
        public void IsValid_GenderNotDefined_False()
        {
            this.user.Gender = (GenderEnum)10;

            Assert.IsFalse(this.validator.IsValid(this.user));
        }
    }
}
