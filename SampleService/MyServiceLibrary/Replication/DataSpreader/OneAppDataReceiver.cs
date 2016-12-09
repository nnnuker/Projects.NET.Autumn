using System;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class OneAppDataReceiver : IDataSpreader<Message<User>>
    {
        public event EventHandler<Message<User>> DataReceived = delegate { };

        public string Name { get; }

        public OneAppDataReceiver(string name, params IReplicable<User, Message<User>>[] services)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            this.Name = name;

            foreach (var replicable in services)
            {
                replicable.MessageCreated += this.OnMessageCreated;
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
            this.DataReceived(sender, message);
        }
    }
}