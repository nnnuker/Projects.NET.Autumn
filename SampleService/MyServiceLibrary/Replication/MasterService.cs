using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Replication;
using MyServiceLibrary.Replication.Attributes;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication
{
    [Master]
    public class MasterService<T> : IReplicable<T, Message<T>> where T : IEntity
    {
        private IService<T> decoratedService;

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Master;

        public event EventHandler<Message<T>> MessageCreated = delegate { };

        public MasterService(IService<T> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            this.decoratedService = service;
        }

        public T Add(T user)
        {
            var result = decoratedService.Add(user);

            MessageCreated(this, new Message<T>(MessageTypeEnum.Add, result));

            return result;
        }

        public bool Delete(T user)
        {
            if (decoratedService.Delete(user))
            {
                MessageCreated(this, new Message<T>(MessageTypeEnum.Delete, user));
            }

            return false;
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

        public bool Save()
        {
            return decoratedService.Save();
        }

        public bool Load()
        {
            bool loaded  = decoratedService.Load();

            if (loaded)
            {
                foreach (var data in decoratedService.GetAll())
                {
                    MessageCreated(this, new Message<T>(MessageTypeEnum.Add, data));
                }
            }

            return loaded;
        }
    }
}
