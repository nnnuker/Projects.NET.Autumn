using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public class Message<T> : EventArgs 
    {
        MessageTypeEnum MessageType { get; set; }

        T Data { get; set; }
    }
}
