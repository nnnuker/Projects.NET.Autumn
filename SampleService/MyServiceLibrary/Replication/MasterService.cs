using System;
using System.Collections.Generic;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Interfaces.Replication;
using MyServiceLibrary.Replication.Attributes;

namespace MyServiceLibrary.Replication
{
    [Master]
    public class MasterService : IReplicable<User, Message<User>>
    {
        private IService<User> decoratedService;

        public event EventHandler<Message<User>> MessageCreated = delegate { };

        public ServiceModeEnum ServiceMode { get; } = ServiceModeEnum.Master;

        public MasterService(IService<User> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException($"{nameof(service)} argument is null");
            }

            this.decoratedService = service;
        }

        public User Add(User user)
        {
            var result = this.decoratedService.Add(user);

            this.MessageCreated(this, new Message<User>(MessageTypeEnum.Add, result));

            return result;
        }

        public bool Delete(User user)
        {
            if (this.decoratedService.Delete(user))
            {
                this.MessageCreated(this, new Message<User>(MessageTypeEnum.Delete, user));

                return true;
            }

            return false;
        }

        public IList<User> GetAll()
        {
            return this.decoratedService.GetAll();
        }

        public IList<User> GetByPredicate(ISearchCriteria<User> predicate)
        {
            return this.decoratedService.GetByPredicate(predicate);
        }

        public void OnMessageReceived(Message<User> message)
        {
        }

        public bool Save()
        {
            return this.decoratedService.Save();
        }

        public bool Load()
        {
            bool loaded = this.decoratedService.Load();

            if (loaded)
            {
                foreach (var data in this.decoratedService.GetAll())
                {
                    this.MessageCreated(this, new Message<User>(MessageTypeEnum.Add, data));
                }
            }

            return loaded;
        }
    }
}
