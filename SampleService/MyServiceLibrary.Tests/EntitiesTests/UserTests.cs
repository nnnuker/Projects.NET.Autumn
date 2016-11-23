using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Entities;

namespace MyServiceLibrary.Tests.EntitiesTests
{
    [TestClass]
    public class UserTests
    {
        private User user1 = new User()
        {
            FirstName = "Vasiliy",
            LastName = "Ivanov",
            Visas = new VisaRecord[] { },
            PersonalId = "ABCD"
        };

        private User user2 = new User()
        {
            FirstName = "Vasiliy",
            LastName = "Ivanov",
            Visas = new VisaRecord[] { },
            PersonalId = "ABCD"
        };

        [TestMethod]
        public void Equals_DiffNamesUsers_False()
        {
            user1.FirstName = "Vasiliy";
            user2.FirstName = "Ivan";

            Assert.IsFalse(user1.Equals(user2));
        }

        [TestMethod]
        public void Equals_SameNamesUsers_True()
        {
            user1.FirstName = "Vasiliy";
            user2.FirstName = "Vasiliy";

            Assert.IsTrue(user1.Equals(user2));
        }

        [TestMethod]
        public void GetHashCode_SameUsers_True()
        {
            user1.FirstName = "Vasiliy";
            user2.FirstName = "Vasiliy";

            Assert.IsTrue(user1.GetHashCode() == user2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_DiffUsers_False()
        {
            user1.FirstName = "Vasiliy";
            user2.FirstName = "Petr";

            Assert.IsFalse(user1.GetHashCode() == user2.GetHashCode());
        }
    }
}
