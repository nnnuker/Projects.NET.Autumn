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

        public event EventHandler<Message<User>> DataReceived = delegate { };

        public string Name { get; }

        public NetworkDataSender()
        {
            this.ips = new List<IPEndPoint>();
            this.Name = GetType().FullName;
        }

        public NetworkDataSender(string name, params IPEndPoint[] ips)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (ips == null)
            {
                throw new ArgumentNullException($"{nameof(ips)} argument is null.");
            }

            this.Name = name;

            this.ips = ips.ToList();
        }

        public void Send(Message<User> data)
        {
            Parallel.ForEach(this.ips, iPEndPoint => this.Send(iPEndPoint, data));
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

            try
            {
                clientState.WorkSocket.EndConnect(ar);
            }
            catch (SocketException)
            {
                return;
            }

            this.Send(clientState);
        }

        private void Send(IPEndPoint ipEndPoint, Message<User> message)
        {
            var client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            var clientState = new AsyncState<Message<User>> { Message = message, WorkSocket = client };

            client.BeginConnect(ipEndPoint, this.ConnectCallback, clientState);
        }

        private void Send(AsyncState<Message<User>> clientState)
        {
            var binaryFormatter = new BinaryFormatter();

            using (var stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, clientState.Message);
                byte[] buffer = stream.ToArray();

                clientState.WorkSocket.BeginSend(buffer, 0, buffer.Length, 0, this.SendCallback, clientState);
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
