using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication
{
    public class SlaveService<T> : IService<T>, IReplicable<T> where T:IEntity
    {
        private IService<T> decoratedService;

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Slave;

        public event EventHandler<Message<T>> EntityAdded = delegate { };
        public event EventHandler<Message<int>> EntityDeleted = delegate { };

        public SlaveService(IService<T> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            this.decoratedService = service;
        }

        public T Add(T user)
        {
            throw new InvalidOperationException();
        }

        public bool Delete(int id)
        {
            throw new InvalidOperationException();
        }

        public IList<T> GetAll()
        {
            return decoratedService.GetAll();
        }

        public IList<T> GetByPredicate(Predicate<T> predicate)
        {
            return decoratedService.GetByPredicate(predicate);
        }

        public void OnEntityAdded(T entity)
        {
            decoratedService.Add(entity);
        }

        public void OnEntityDeleted(int id)
        {
            decoratedService.Delete(id);
        }

        public void Save()
        {
            decoratedService.Save();
        }

        public void Load()
        {
            decoratedService.Load();
        }
    }
}
