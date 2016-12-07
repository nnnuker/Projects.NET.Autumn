using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Interfaces.Replication;
using MyServiceLibrary.Replication.Attributes;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication
{
    [Slave]
    public class SlaveService : IReplicable<User, Message<User>>
    {
        private IService<User> decoratedService;

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Slave;

        public event EventHandler<Message<User>> MessageCreated = delegate { };

        public SlaveService(IService<User> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            this.decoratedService = service;
        }

        public User Add(User user)
        {
            throw new InvalidOperationException();
        }

        public bool Delete(User user)
        {
            throw new InvalidOperationException();
        }

        public IList<User> GetAll()
        {
            return decoratedService.GetAll();
        }

        public IList<User> GetByPredicate(ISearchCriteria<User> predicate)
        {
            return decoratedService.GetByPredicate(predicate);
        }

        public void OnMessageReceived(Message<User> message)
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
