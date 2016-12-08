using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Configurations.SerializableConfiguration
{
    [Serializable]
    public class ServiceConfiguration
    {
        public string ServiceType { get; set; }
        public string DomainName { get; set; }
        public string RepositoryType { get; set; }
        public string RepositoryStateSaverType { get; set; }
        public string RepositoryPath { get; set; }
        public string ValidatorType { get; set; }
        public string GeneratorType { get; set; }
        public bool IsMaster { get; set; }
        public List<DataSpreaderConfiguration> DataSpreaders { get; set; }
        public string LoggerType { get; set; }
        public string LoggerName { get; set; }
    }
}
