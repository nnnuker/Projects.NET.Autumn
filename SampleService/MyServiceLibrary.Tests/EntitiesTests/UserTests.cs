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
            this.user1.FirstName = "Vasiliy";
            this.user2.FirstName = "Ivan";

            Assert.IsFalse(this.user1.Equals(this.user2));
        }

        [TestMethod]
        public void Equals_SameNamesUsers_True()
        {
            this.user1.FirstName = "Vasiliy";
            this.user2.FirstName = "Vasiliy";

            Assert.IsTrue(this.user1.Equals(this.user2));
        }

        [TestMethod]
        public void GetHashCode_SameUsers_True()
        {
            this.user1.FirstName = "Vasiliy";
            this.user2.FirstName = "Vasiliy";

            Assert.IsTrue(this.user1.GetHashCode() == this.user2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_DiffUsers_False()
        {
            this.user1.FirstName = "Vasiliy";
            this.user2.FirstName = "Petr";

            Assert.IsFalse(this.user1.GetHashCode() == this.user2.GetHashCode());
        }
    }
}
