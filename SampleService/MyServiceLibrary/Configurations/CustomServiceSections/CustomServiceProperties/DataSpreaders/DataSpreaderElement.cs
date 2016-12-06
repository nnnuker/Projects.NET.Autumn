using System;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders
{
    public class DataSpreaderElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
        public string DataSpreaderType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(DataSpreaderIpElement), AddItemName = "EndPoint")]
        public DataSpreadersIpsCollection Ips
        {
            get { return (DataSpreadersIpsCollection)base[""]; }
            set { base[""] = value; }
        }
    }
}
