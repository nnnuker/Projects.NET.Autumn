using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Replication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyServiceLibrary.Replication
{
    public class DataSpreaderService : MarshalByRefObject, IReplicable<User, Message<User>>, IDataSpreadersChangeable<Message<User>>
    {
        private readonly IReplicable<User, Message<User>> decoratedService;
        private List<IDataSpreader<Message<User>>> dataSpreaders;

        public ServiceModeEnum ServiceMode { get; }

        public event EventHandler<Message<User>> MessageCreated = delegate { };

        public DataSpreaderService(IReplicable<User, Message<User>> service)
        {
            if (service == null)
                throw new ArgumentNullException($"{nameof(service)} argument is null");

            decoratedService = service;
            ServiceMode = decoratedService.ServiceMode;

            decoratedService.MessageCreated += OnMessageCreated;

            dataSpreaders = new List<IDataSpreader<Message<User>>>();
        }

        public DataSpreaderService(IReplicable<User, Message<User>> service, IEnumerable<IDataSpreader<Message<User>>> dataSpreaders) : this(service)
        {
            if (dataSpreaders == null)
                throw new ArgumentNullException($"{nameof(dataSpreaders)} argument is null");

            foreach (var dataSpreader in dataSpreaders)
            {
                AddDataSpreader(dataSpreader);
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
            decoratedService.OnMessageReceived(message);
        }

        public bool Save()
        {
            return decoratedService.Save();
        }

        public bool Load()
        {
            return decoratedService.Load();
        }

        public void AddDataSpreader(IDataSpreader<Message<User>> dataSpreader)
        {
            if (dataSpreader == null)
                throw new ArgumentNullException($"{nameof(dataSpreader)} argument is null");

            dataSpreaders.Add(dataSpreader);

            dataSpreader.Start();

            foreach (var data in decoratedService.GetAll())
            {
                dataSpreader.Send(new Message<User>(MessageTypeEnum.Add, data));
            }

            dataSpreader.DataReceived += OnMessageReceived; 
        }

        public void RemoveDataSpreader(string spreaderName)
        {
            if (spreaderName == null)
                throw new ArgumentNullException($"{nameof(spreaderName)} argument is null");

            var spreader = dataSpreaders.FirstOrDefault(spr => spr.Name == spreaderName);

            if (spreader == null)
            {
                return;
            }

            spreader.DataReceived -= OnMessageReceived;

            dataSpreaders.Remove(spreader);
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
