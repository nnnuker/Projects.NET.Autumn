using MyServiceLibrary.Configurations.CustomServiceSections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace MyServiceLibrary.Configurations
{
    public static class AppConfiguration
    {
        public static List<ServiceElement> GetServices()
        {
            var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = conf.Sections["Replication"] as ServicesSection;//ConfigurationManager.GetSection("Replication") as ServicesSection;

            if (section == null)
                throw new ConfigurationErrorsException("Replication section not found");

            return section.Services.OfType<ServiceElement>().ToList();
        }
    }
}
