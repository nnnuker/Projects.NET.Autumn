using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IDataSpreader<TData> where TData : EventArgs
    {
        event EventHandler<TData> DataReceived;

        string Name { get; }

        void Send(TData data);

        void Start();

        void Stop();
    }
}