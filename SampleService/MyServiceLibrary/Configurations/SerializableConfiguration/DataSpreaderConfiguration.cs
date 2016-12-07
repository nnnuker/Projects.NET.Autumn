using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Configurations.SerializableConfiguration

{
    [Serializable]
    public class DataSpreaderConfiguration
    {
        public string Name { get; set; }
        public string DataSpreaderType { get; set; }
        public List<EndPointConfiguration> EndPoints { get; set; }
    }
}
