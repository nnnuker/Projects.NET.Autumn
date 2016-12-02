using MyServiceLibrary.Interfaces.Entities;
using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IReplicable<TEntity, TMessage> : IService<TEntity> where TEntity : IEntity where TMessage : EventArgs
    {
        ServiceModeEnum ServiceMode { get; }

        event EventHandler<TMessage> MessageCreated;

        void OnMessageReceived(TMessage message);
    }
}
