using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MyServiceLibrary.Configurations;
using MyServiceLibrary.Configurations.SerializableConfiguration;
using MyServiceLibrary.Replication;

namespace MyServiceLibrary.Services.Factories
{
    public class BasicServiceFactory
    {
        public List<DataSpreaderService> RunServices()
        {
            List<ServiceConfiguration> servicesConfigurations = AppConfiguration.GetServices();

            return servicesConfigurations.Select(this.CreateService).ToList();
        }

        private DataSpreaderService CreateService(ServiceConfiguration serviceElement)
        {
            var domain = this.CreateDomain(serviceElement.DomainName);

            var serviceLoader = (DomainServiceLoader)domain.CreateInstanceAndUnwrap(Assembly.GetExecutingAssembly().FullName, typeof(DomainServiceLoader).FullName);

            return serviceLoader.GetService(serviceElement);
        }

        private AppDomain CreateDomain(string domainName)
        {
            var baseSetup = AppDomain.CurrentDomain.SetupInformation;

            var domainSetup = new AppDomainSetup
            {
                ApplicationBase = baseSetup.ApplicationBase,
                PrivateBinPath = baseSetup.ApplicationBase,
                ConfigurationFile = baseSetup.ConfigurationFile
            };

            var privateBinPath = domainSetup.PrivateBinPath;

            domainSetup.PrivateBinPath = Path.Combine(privateBinPath, domainName);

            var domain = AppDomain.CreateDomain(domainName, null, domainSetup);

            domainSetup.PrivateBinPath = privateBinPath;

            domain.Load(Assembly.GetExecutingAssembly().FullName);

            return domain;
        }
    }
}
