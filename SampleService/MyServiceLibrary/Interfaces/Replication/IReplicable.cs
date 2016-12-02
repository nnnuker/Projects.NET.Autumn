using MyServiceLibrary.Interfaces.Entities;
using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IReplicable<T> where T : IEntity
    {
        ServiceModeEnum ServiceMode { get; }

        event EventHandler<Message<T>> EntityAdded;
        event EventHandler<Message<int>> EntityDeleted;

        void OnEntityAdded(T entity);
        void OnEntityDeleted(int id);
    }
}
