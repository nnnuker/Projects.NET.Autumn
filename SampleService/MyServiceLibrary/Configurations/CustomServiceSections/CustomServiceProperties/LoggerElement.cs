using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties
{
    public class LoggerElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, DefaultValue = "default")]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true, DefaultValue = "MyServiceLibrary.Infrastructure.Loggers.NlogLogger, MyServiceLibrary")]
        public string LoggerType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
