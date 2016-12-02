using MyServiceLibrary.Interfaces.Entities;
using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IReplicable<T>: IService<T> where T : IEntity
    {
        ServiceModeEnum ServiceMode { get; }

        event EventHandler<Message<T>> MessageReceived;

        void OnMessageReceived(Message<T> message);
    }
}
