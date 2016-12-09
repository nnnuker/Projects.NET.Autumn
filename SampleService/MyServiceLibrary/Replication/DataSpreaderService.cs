using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Interfaces.Replication;

namespace MyServiceLibrary.Replication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DataSpreaderService : MarshalByRefObject, IReplicable<User, Message<User>>, IDataSpreadersChangeable<Message<User>>
    {
        private readonly IReplicable<User, Message<User>> decoratedService;
        private List<IDataSpreader<Message<User>>> dataSpreaders;

        public event EventHandler<Message<User>> MessageCreated = delegate { };

        public ServiceModeEnum ServiceMode { get; }

        public DataSpreaderService(IReplicable<User, Message<User>> service)
        {
            if (service == null)
            {
                throw new ArgumentNullException($"{nameof(service)} argument is null");
            }

            this.decoratedService = service;
            this.ServiceMode = this.decoratedService.ServiceMode;

            this.decoratedService.MessageCreated += this.OnMessageCreated;

            this.dataSpreaders = new List<IDataSpreader<Message<User>>>();
        }

        public DataSpreaderService(IReplicable<User, Message<User>> service, IEnumerable<IDataSpreader<Message<User>>> dataSpreaders) : this(service)
        {
            if (dataSpreaders == null)
            {
                throw new ArgumentNullException($"{nameof(dataSpreaders)} argument is null");
            }

            foreach (var dataSpreader in dataSpreaders)
            {
                this.AddDataSpreader(dataSpreader);
            }
        }

        public User Add(User user)
        {
            return this.decoratedService.Add(user);
        }

        public bool Delete(User user)
        {
            return this.decoratedService.Delete(user);
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
            this.decoratedService.OnMessageReceived(message);
        }

        public bool Save()
        {
            return this.decoratedService.Save();
        }

        public bool Load()
        {
            return this.decoratedService.Load();
        }

        public void AddDataSpreader(IDataSpreader<Message<User>> dataSpreader)
        {
            if (dataSpreader == null)
            {
                throw new ArgumentNullException($"{nameof(dataSpreader)} argument is null");
            }

            this.dataSpreaders.Add(dataSpreader);

            dataSpreader.Start();

            foreach (var data in this.decoratedService.GetAll())
            {
                dataSpreader.Send(new Message<User>(MessageTypeEnum.Add, data));
            }

            dataSpreader.DataReceived += this.OnMessageReceived; 
        }

        public void RemoveDataSpreader(string spreaderName)
        {
            if (spreaderName == null)
            {
                throw new ArgumentNullException($"{nameof(spreaderName)} argument is null");
            }

            var spreader = this.dataSpreaders.FirstOrDefault(spr => spr.Name == spreaderName);

            if (spreader == null)
            {
                return;
            }

            spreader.DataReceived -= this.OnMessageReceived;

            this.dataSpreaders.Remove(spreader);
        }

        private void OnMessageCreated(object sender, Message<User> message)
        {
            this.dataSpreaders.ForEach(spreader => spreader.Send(message));
        }

        private void OnMessageReceived(object sender, Message<User> message)
        {
            this.OnMessageReceived(message);
        }
    }
}
