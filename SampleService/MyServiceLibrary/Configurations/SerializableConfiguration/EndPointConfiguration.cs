using System;

namespace MyServiceLibrary.Configurations.SerializableConfiguration
{
    [Serializable]
    public class EndPointConfiguration
    {
        public string Ip { get; set; }
        public string Port { get; set; }
    }
}
