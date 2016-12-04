using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IDataSpreader<TData> where TData : EventArgs
    {
        string Name { get; }

        event EventHandler<TData> DataReceived;
        void Send(TData data);

        void Start();
        void Stop();
    }
}