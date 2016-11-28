using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Infrastructure.IdGenerators;

namespace MyServiceLibrary.Tests.IdGeneratorsTests
{
    [TestClass]
    public class IdGeneratorTests
    {
        [TestMethod]
        public void GetNext_10Ids_10Ids()
        {
            var generator = new IdGenerator();
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(generator.GetNext() > 0);
            }
        }

        [TestMethod]
        public void GetNext_10IdsFromInitValue_10IdsLargerThenInit()
        {
            var generator = new IdGenerator();
            generator.Initialize(100);
            for (int i = 0; i < 10; i++)
            {
                Assert.IsTrue(generator.GetNext() >= 100);
            }
        }
    }
}
