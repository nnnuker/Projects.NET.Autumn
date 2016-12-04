using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyServiceLibrary.Replication.DataSpreader.States;

namespace MyServiceLibrary.Replication.DataSpreader
{
    public class NetworkDataReceiver : IDataSpreader<Message<User>>
    {
        private List<IPEndPoint> ips;
        private CancellationTokenSource cancellationToken;

        public string Name { get; }
        public event EventHandler<Message<User>> DataReceived = delegate { };

        public NetworkDataReceiver()
        {
            ips = new List<IPEndPoint>();
            Name = GetType().FullName;
        }

        public NetworkDataReceiver(string name, params IPEndPoint[] ips)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (ips == null)
                throw new ArgumentNullException($"{nameof(ips)} argument is null.");

            this.ips = ips.ToList();
        }

        public void Send(Message<User> data)
        {
        }

        public void Start()
        {
            cancellationToken = new CancellationTokenSource();
            foreach (var ipEndPoint in ips)
            {
                Task.Run(() => StartListening(ipEndPoint, cancellationToken.Token), cancellationToken.Token);
            }
        }

        public void Stop()
        {
            cancellationToken?.Cancel();
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

                    listener.BeginAccept(AcceptCallback, listener);
                }
            }
            catch (Exception e)
            {
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            var socket = (Socket)ar.AsyncState;
            Socket handler = socket.EndAccept(ar);

            var state = new AsyncState<Message<User>> { WorkSocket = handler };

            handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReadCallback, state);
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
                handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReadCallback, state);
            }
            else
            {
                var binaryFormatter = new BinaryFormatter();

                using (var stream = new MemoryStream(state.AllBytes.ToArray()))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var message = (Message<User>)binaryFormatter.Deserialize(stream);

                    Task.Run(() => DataReceived(this, message));
                }
            }
        }
    }
}
