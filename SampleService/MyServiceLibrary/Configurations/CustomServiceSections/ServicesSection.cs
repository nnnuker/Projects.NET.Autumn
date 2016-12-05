using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections
{
    public class ServicesSection : ConfigurationSection
    {
        [ConfigurationProperty("Services")]
        [ConfigurationCollection(typeof(ServiceElement), AddItemName = "Service")]
        public ServicesCollection Services => (ServicesCollection)base["Services"];
    }
}
