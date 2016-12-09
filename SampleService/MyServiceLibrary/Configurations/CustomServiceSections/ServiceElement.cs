using System.Configuration;
using MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties;
using MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders;

namespace MyServiceLibrary.Configurations.CustomServiceSections
{
    public class ServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string ServiceType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("domainName", IsRequired = true)]
        public string DomainName
        {
            get { return (string)base["domainName"]; }
            set { base["domainName"] = value; }
        }

        [ConfigurationProperty("Repository", IsRequired = false)]
        public RepositoryElement Repository
        {
            get { return (RepositoryElement)base["Repository"]; }
            set { base["Repository"] = value; }
        }

        [ConfigurationProperty("Logger", IsRequired = false)]
        public LoggerElement Logger
        {
            get { return (LoggerElement)base["Logger"]; }
            set { base["Logger"] = value; }
        }

        [ConfigurationProperty("Validator", IsRequired = false)]
        public ValidatorElement Validator
        {
            get { return (ValidatorElement)base["Validator"]; }
            set { base["Validator"] = value; }
        }

        [ConfigurationProperty("Generator", IsRequired = false)]
        public GeneratorElement Generator
        {
            get { return (GeneratorElement)base["Generator"]; }
            set { base["Generator"] = value; }
        }

        [ConfigurationProperty("isMaster", IsRequired = true)]
        public bool IsMaster
        {
            get { return (bool)base["isMaster"]; }
            set { base["isMaster"] = value; }
        }

        [ConfigurationProperty("DataSpreaders", IsRequired = false)]
        [ConfigurationCollection(typeof(DataSpreaderElement), AddItemName = "DataSpreader")]
        public DataSpreadersCollection DataSpreaders
        {
            get { return (DataSpreadersCollection)base["DataSpreaders"]; }
            set { base["DataSpreaders"] = value; }
        }
    }
}
