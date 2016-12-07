using System;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties
{
    public class ValidatorElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, DefaultValue = "MyServiceLibrary.Infrastructure.UserValidators.UserValidator, MyServiceLibrary")]
        public string ValidatorType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }
    }
}
