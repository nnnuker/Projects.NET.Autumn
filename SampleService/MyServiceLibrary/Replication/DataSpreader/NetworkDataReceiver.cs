using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using MyServiceLibrary.Replication.DataSpreader.States;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class NetworkDataReceiver : IDataSpreader<Message<User>>
    {
        private List<IPEndPoint> ips;

        private CancellationTokenSource cancellationToken;

        public event EventHandler<Message<User>> DataReceived = delegate { };

        public string Name { get; }

        public NetworkDataReceiver()
        {
            this.ips = new List<IPEndPoint>();
            this.Name = GetType().FullName;
        }

        public NetworkDataReceiver(string name, params IPEndPoint[] ips)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (ips == null)
            {
                throw new ArgumentNullException($"{nameof(ips)} argument is null.");
            }

            this.ips = ips.ToList();
        }

        public void Send(Message<User> data)
        {
        }

        public void Start()
        {
            this.cancellationToken = new CancellationTokenSource();
            foreach (var ipEndPoint in this.ips)
            {
                Task.Run(() => this.StartListening(ipEndPoint, this.cancellationToken.Token), this.cancellationToken.Token);
            }
        }

        public void Stop()
        {
            this.cancellationToken?.Cancel();
        }

        public void StartListening(IPEndPoint ipEndPoint, CancellationToken cancellationToken)
        {
            try
            {
                var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(ipEndPoint);
                listener.Listen(100);

                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    listener.BeginAccept(this.AcceptCallback, listener);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            Socket handler = socket.EndAccept(ar);

            var state = new AsyncState<Message<User>> { WorkSocket = handler };

            handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, this.ReadCallback, state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            var state = (AsyncState<Message<User>>)ar.AsyncState;
            Socket handler = state.WorkSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.AllBytes.AddRange(state.Buffer);

                Array.Clear(state.Buffer, 0, state.Buffer.Length);
                handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, this.ReadCallback, state);
            }
            else
            {
                var binaryFormatter = new BinaryFormatter();

                using (var stream = new MemoryStream(state.AllBytes.ToArray()))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var message = (Message<User>)binaryFormatter.Deserialize(stream);

                    this.DataReceived(this, message);
                }
            }
        }
    }
}
