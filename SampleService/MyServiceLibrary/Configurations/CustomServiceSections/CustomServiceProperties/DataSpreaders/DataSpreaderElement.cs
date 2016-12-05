using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders
{
    public class DataSpreaderElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string DataSpreaderType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("", IsRequired = true)]
        [ConfigurationCollection(typeof(DataSpreaderIpElement), AddItemName = "EndPoint")]
        public DataSpreadersIpsCollection Ips 
        {
            get { return (DataSpreadersIpsCollection)base[""]; }
            set { base[""] = value; }
        }
    }
}
