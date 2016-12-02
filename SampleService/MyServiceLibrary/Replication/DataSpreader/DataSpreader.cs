using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication.MessageSenders
{
    public class DataSpreader : IDataSpreader<Message<User>>
    {
        private List<string> ips;

        public event EventHandler<Message<User>> DataReceived = delegate { };
        
        public DataSpreader()
        {
            ips = new List<string>();
        }

        public DataSpreader(params string[] ips)
        {
            if (ips == null)
                throw new ArgumentNullException($"{nameof(ips)} argument is null.");
        }

        public void Send(Message<User> data)
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
