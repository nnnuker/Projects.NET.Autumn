using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication
{
    public class MasterService<T> : IReplicable<T> where T:IEntity
    {
        private IService<T> decoratedService;

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Master;

        public event EventHandler<Message<T>> MessageReceived = delegate { };

        public MasterService(IService<T> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            this.decoratedService = service;
        }

        public T Add(T user)
        {
            return decoratedService.Add(user);
        }

        public bool Delete(int id)
        {
            return decoratedService.Delete(id);
        }

        public IList<T> GetAll()
        {
            return decoratedService.GetAll();
        }

        public IList<T> GetByPredicate(Predicate<T> predicate)
        {
            return decoratedService.GetByPredicate(predicate);
        }

        public void OnMessageReceived(Message<T> message)
        {
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
