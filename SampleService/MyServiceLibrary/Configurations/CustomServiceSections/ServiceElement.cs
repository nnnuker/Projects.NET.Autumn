using MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties;
using MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections
{
    public class ServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsKey = true, IsRequired = true)]
        public string ServiceType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("domainName", IsKey = false, IsRequired = true)]
        public string DomainName
        {
            get { return (string)base["domainName"]; }
            set { base["domainName"] = value; }
        }

        [ConfigurationProperty("Repository", IsKey = false, IsRequired = false)]
        public RepositoryElement Repository
        {
            get { return (RepositoryElement)base["Repository"]; }
            set { base["Repository"] = value; }
        }

        [ConfigurationProperty("Validator", IsKey = false, IsRequired = false)]
        public ValidatorElement Validator
        {
            get { return (ValidatorElement)base["Validator"]; }
            set { base["Validator"] = value; }
        }

        [ConfigurationProperty("Generator", IsKey = false, IsRequired = false)]
        public GeneratorElement Generator
        {
            get { return (GeneratorElement)base["Generator"]; }
            set { base["Generator"] = value; }
        }

        [ConfigurationProperty("DataSpreaders", IsRequired = true)]
        [ConfigurationCollection(typeof(DataSpreaderElement), AddItemName = "DataSpreader")]
        public DataSpreadersCollection Ips
        {
            get { return (DataSpreadersCollection)base["DataSpreaders"]; }
            set { base["DataSpreaders"] = value; }
        }
    }
}
