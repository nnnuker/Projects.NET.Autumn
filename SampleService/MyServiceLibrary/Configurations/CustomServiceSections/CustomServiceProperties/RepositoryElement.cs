using System;
using System.Configuration;

namespace MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties
{
    public class RepositoryElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true, DefaultValue = "MyServiceLibrary.Repositories.UserMemoryRepository, MyServiceLibrary")]
        public string RepositoryType
        {
            get { return (string)base["type"]; }
            set { base["type"] = value; }
        }

        [ConfigurationProperty("saver", IsRequired = false, DefaultValue = "MyServiceLibrary.Repositories.StateSavers.XmlUserRepositorySaver, MyServiceLibrary")]
        public string StateSaver
        {
            get { return (string)base["saver"]; }
            set { base["saver"] = value; }
        }

        [ConfigurationProperty("path", IsRequired = false)]
        public string Path
        {
            get { return (string)base["path"]; }
            set { base["path"] = value; }
        }
    }
}
