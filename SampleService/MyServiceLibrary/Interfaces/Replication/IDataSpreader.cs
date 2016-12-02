using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IDataSpreader<TData>
    {
        event EventHandler<TData> DataReceived;
        void Send(TData data);

        void Start();
        void Stop();
    }
}