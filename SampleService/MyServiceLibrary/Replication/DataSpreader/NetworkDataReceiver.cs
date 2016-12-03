using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class NetworkDataReceiver : IDataSpreader<Message<User>>
    {
        private List<string> ips;

        public event EventHandler<Message<User>> DataReceived = delegate { };

        public NetworkDataReceiver()
        {
            ips = new List<string>();
        }

        public NetworkDataReceiver(params string[] ips)
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
