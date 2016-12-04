using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication
{
    public class SlaveService<T> : IReplicable<T, Message<T>> where T:IEntity
    {
        private IService<T> decoratedService;

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Slave;

        public event EventHandler<Message<T>> MessageCreated = delegate { };

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

        public bool Delete(T user)
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

        public void OnMessageReceived(Message<T> message)
        {
            if (message == null)
                throw new ArgumentNullException($"{nameof(message)} argument is null");

            switch (message.MessageType)
            {
                case MessageTypeEnum.Add:
                    {
                        decoratedService.Add(message.Data);
                        break;
                    }

                case MessageTypeEnum.Delete:
                    {
                        decoratedService.Delete(message.Data);
                        break;
                    }

                default:
                    {
                        throw new NotSupportedException($"{message.MessageType} message is not supported");
                    }
            }
        }

        public bool Save()
        {
            return decoratedService.Save();
        }

        public bool Load()
        {
            return decoratedService.Load();
        }
    }
}
