using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using MyServiceLibrary.Replication.DataSpreader.States;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class NetworkDataSender : IDataSpreader<Message<User>>
    {
        private readonly List<IPEndPoint> ips;

        public string Name { get; }
        public event EventHandler<Message<User>> DataReceived = delegate { };

        public NetworkDataSender()
        {
            ips = new List<IPEndPoint>();
            Name = GetType().FullName;
        }

        public NetworkDataSender(string name, params IPEndPoint[] ips)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (ips == null)
                throw new ArgumentNullException($"{nameof(ips)} argument is null.");

            Name = name;

            this.ips = ips.ToList();
        }

        public void Send(Message<User> data)
        {
            Parallel.ForEach(ips, iPEndPoint => Send(iPEndPoint, data));
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            var clientState = (AsyncState<Message<User>>)ar.AsyncState;

            clientState.WorkSocket.EndConnect(ar);

            Send(clientState);
        }

        private void Send(IPEndPoint ipEndPoint, Message<User> message)
        {
            try
            {
                var client = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                var clientState = new AsyncState<Message<User>> { Message = message, WorkSocket = client};

                client.BeginConnect(ipEndPoint, ConnectCallback, clientState);
            }
            catch
            {
            }
        }

        private void Send(AsyncState<Message<User>> clientState)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, clientState.Message);
                byte[] buffer = stream.ToArray();

                clientState.WorkSocket.BeginSend(buffer, 0, buffer.Length, 0, SendCallback, clientState);
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            var clientState = (AsyncState<Message<User>>)ar.AsyncState;

            clientState.WorkSocket.EndSend(ar);

            clientState.WorkSocket.Shutdown(SocketShutdown.Both);
            clientState.WorkSocket.Close();
        }
    }
}
