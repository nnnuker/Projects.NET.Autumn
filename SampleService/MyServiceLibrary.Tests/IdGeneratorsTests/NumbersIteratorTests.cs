using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MyServiceLibrary.Infrastructure.IdGenerators;

namespace MyServiceLibrary.Tests.IdGeneratorsTests
{
    [TestClass]
    public class NumbersIteratorTests
    {
        [TestMethod]
        public void Current_10Nums_AllNumsIsSimple()
        {
            var expected = new List<int>() { 1, 2, 3, 5, 7, 11, 13, 17, 19, 23};
            var iterator = new NumbersIterator();

            foreach (var item in expected)
            {
                iterator.MoveNext();
                Assert.AreEqual(item, iterator.Current);
            }
        }
    }
}
