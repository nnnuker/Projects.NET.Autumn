using System;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders
{
    public class DataSpreadersIpsCollection : ConfigurationElementCollection
    {
        protected override string ElementName { get; } = "EndPoint";

        protected override ConfigurationElement CreateNewElement()
        {
            return new DataSpreaderIpElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataSpreaderIpElement)element).Ip + ":" + ((DataSpreaderIpElement)element).Port;
        }
    }
}
