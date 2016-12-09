using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders
{
    public class DataSpreadersCollection : ConfigurationElementCollection
    {
        protected override string ElementName { get; } = "DataSpreader";

        protected override ConfigurationElement CreateNewElement()
        {
            return new DataSpreaderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DataSpreaderElement)element).Name;
        }
    }
}
