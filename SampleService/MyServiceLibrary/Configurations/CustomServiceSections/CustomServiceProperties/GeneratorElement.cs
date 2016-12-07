using System;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties
{
    public class GeneratorElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, DefaultValue = "MyServiceLibrary.Infrastructure.IdGenerators.IdGenerator, MyServiceLibrary")]
        public string GeneratorType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
