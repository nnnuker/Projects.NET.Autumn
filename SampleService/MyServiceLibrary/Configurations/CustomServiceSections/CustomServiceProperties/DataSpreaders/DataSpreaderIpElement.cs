using System;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders
{
    public class DataSpreaderIpElement : ConfigurationElement
    {
        [ConfigurationProperty("ip", IsRequired = true)]
        public string Ip
        {
            get { return (string)base["ip"]; }
            set { base["ip"] = value; }
        }

        [ConfigurationProperty("port", IsRequired = true)]
        public string Port
        {
            get { return (string)base["port"]; }
            set { base["port"] = value; }
        }
    }
}
