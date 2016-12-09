using System;
using MyServiceLibrary.Interfaces.Entities;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IReplicable<TEntity, TMessage> : IService<TEntity> where TEntity : IEntity where TMessage : EventArgs
    {
        event EventHandler<TMessage> MessageCreated;

        ServiceModeEnum ServiceMode { get; }

        void OnMessageReceived(TMessage message);
    }
}
