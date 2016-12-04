using System;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class OneAppDataReceiver : IDataSpreader<Message<User>>
    {
        public string Name { get; }
        public event EventHandler<Message<User>> DataReceived = delegate { };

        public OneAppDataReceiver(string name, params IReplicable<User, Message<User>>[] services)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (services == null) throw new ArgumentNullException(nameof(services));

            Name = name;

            foreach (var replicable in services)
            {
                replicable.MessageCreated += OnMessageCreated;
            }
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

        private void OnMessageCreated(object sender, Message<User> message)
        {
            DataReceived(sender, message);
        }
    }
}