using System;
using System.Collections.Generic;
using System.Linq;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class OneAppDataSender : IDataSpreader<Message<User>>
    {
        private readonly IReplicable<User, Message<User>>[] services;

        public string Name { get; }
        public event EventHandler<Message<User>> DataReceived = delegate { };

        public OneAppDataSender(string name, params IReplicable<User, Message<User>>[] services)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (services == null) throw new ArgumentNullException(nameof(services));

            Name = name;

            this.services = services;
        }

        public void Send(Message<User> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            foreach (var service in services)
            {
                service.OnMessageReceived(data);
            }
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }
}
