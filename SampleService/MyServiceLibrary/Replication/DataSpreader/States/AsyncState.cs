using System.Collections.Generic;
using System.Net.Sockets;

namespace MyServiceLibrary.Replication.DataSpreader.States
{
    public class AsyncState<T>
    {
        public int BufferSize { get; set; } = 1024;

        public byte[] Buffer { get; set; } = new byte[1024];

        public List<byte> AllBytes { get; set; } = new List<byte>();

        public Socket WorkSocket { get; set; }

        public T Message { get; set; }
    }
}
