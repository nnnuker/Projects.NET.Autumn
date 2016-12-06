using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections
{
    public class ServicesCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ServiceElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServiceElement)element).DomainName;
        }
    }
}
