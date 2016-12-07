using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Interfaces.Replication;
using MyServiceLibrary.Replication.Attributes;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Replication
{
    [Master]
    public class MasterService : IReplicable<User, Message<User>>
    {
        private IService<User> decoratedService;

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Master;

        public event EventHandler<Message<User>> MessageCreated = delegate { };

        public MasterService(IService<User> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            this.decoratedService = service;
        }

        public User Add(User user)
        {
            var result = decoratedService.Add(user);

            MessageCreated(this, new Message<User>(MessageTypeEnum.Add, result));

            return result;
        }

        public bool Delete(User user)
        {
            if (decoratedService.Delete(user))
            {
                MessageCreated(this, new Message<User>(MessageTypeEnum.Delete, user));

                return true;
            }

            return false;
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
        }

        public bool Save()
        {
            return decoratedService.Save();
        }

        public bool Load()
        {
            bool loaded = decoratedService.Load();

            if (loaded)
            {
                foreach (var data in decoratedService.GetAll())
                {
                    MessageCreated(this, new Message<User>(MessageTypeEnum.Add, data));
                }
            }

            return loaded;
        }
    }
}
