using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication.MessageSenders
{
    public class NetworkDataSender : IDataSpreader<Message<User>>
    {
        private List<string> ips;

        public event EventHandler<Message<User>> DataReceived = delegate { };
        
        public NetworkDataSender()
        {
            ips = new List<string>();
        }

        public NetworkDataSender(params string[] ips)
        {
            if (ips == null)
                throw new ArgumentNullException($"{nameof(ips)} argument is null.");
        }

        public void Send(Message<User> data)
        {
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
