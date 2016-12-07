using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyServiceLibrary.Configurations;

namespace MyServiceLibrary.Tests.ConfigurationsTests
{
    [TestClass]
    public class AppConfigurationTests
    {
        [TestMethod]
        public void GetServices_All_Success()
        {
            var services = AppConfiguration.GetServices();

            Assert.IsNotNull(services);
            Assert.IsTrue(services.Count == 1);
        }
    }
}
