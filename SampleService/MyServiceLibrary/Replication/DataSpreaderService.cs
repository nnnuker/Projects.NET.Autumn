using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyServiceLibrary.Replication
{
    public class DataSpreaderService : IReplicable<User, Message<User>>
    {
        private IReplicable<User, Message<User>> decoratedService;
        private List<IDataSpreader<Message<User>>> dataSpreaders;

        public ServiceModeEnum ServiceMode { get; }

        public event EventHandler<Message<User>> MessageCreated = delegate { };

        public DataSpreaderService(IReplicable<User, Message<User>> service, params IDataSpreader<Message<User>>[] dataSpreaders)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            if (dataSpreaders == null)
                throw new ArgumentNullException($"{nameof(dataSpreaders)} argument is null");

            decoratedService = service;
            ServiceMode = decoratedService.ServiceMode;

            decoratedService.MessageCreated += OnMessageCreated;

            this.dataSpreaders = dataSpreaders.ToList();
            foreach (var spreader in this.dataSpreaders)
            {
                spreader.DataReceived += OnMessageReceived;
                spreader.Start();
            }
        }

        public User Add(User user)
        {
            return decoratedService.Add(user);
        }

        public bool Delete(User user)
        {
            return decoratedService.Delete(user);
        }

        public IList<User> GetAll()
        {
            return decoratedService.GetAll();
        }

        public IList<User> GetByPredicate(Predicate<User> predicate)
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

        public void Save()
        {
            decoratedService.Save();
        }

        public void Load()
        {
            decoratedService.Load();
        }

        private void OnMessageCreated(object sender, Message<User> message)
        {
            dataSpreaders.ForEach(spreader => spreader.Send(message));
        }

        private void OnMessageReceived(object sender, Message<User> message)
        {
            OnMessageReceived(message);
        }
    }
}
