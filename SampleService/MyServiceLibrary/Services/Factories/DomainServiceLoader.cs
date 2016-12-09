using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using MyServiceLibrary.Configurations.SerializableConfiguration;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Interfaces.Replication;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Replication.Attributes;
using MyServiceLibrary.Repositories.RepositoryStates;

namespace MyServiceLibrary.Services.Factories
{
    public class DomainServiceLoader : MarshalByRefObject
    {
        public DataSpreaderService GetService(ServiceConfiguration serviceElement)
        {
            if (serviceElement == null)
            {
                throw new ArgumentNullException(nameof(serviceElement));
            }

            var idGenerator = Activator.CreateInstance(Type.GetType(serviceElement.GeneratorType, true, true)) as IGenerator<int>;
            var saver = Activator.CreateInstance(Type.GetType(serviceElement.RepositoryStateSaverType, true, true)) as IStateSaver<UserRepositorySnapshot>;

            var repository = Activator.CreateInstance(Type.GetType(serviceElement.RepositoryType, true, true), idGenerator, saver) as IRepository<User>;
            var validator = Activator.CreateInstance(Type.GetType(serviceElement.ValidatorType, true, true)) as IValidator<User>;

            var basicService = Activator.CreateInstance(typeof(BasicUserService), repository, validator) as IService<User>;

            var logger = Activator.CreateInstance(Type.GetType(serviceElement.LoggerType, true, true), serviceElement.LoggerName);

            basicService = Activator.CreateInstance(typeof(LoggableUserService), basicService, logger) as IService<User>;

            basicService = Activator.CreateInstance(typeof(ConcurrentUserService), basicService) as IService<User>;

            var serviceType = Type.GetType(serviceElement.ServiceType, true, true);
            var attributes = serviceType.GetCustomAttributesData().ToList();

            IReplicable<User, Message<User>> service = null;

            if (attributes.Exists(attr => attr.AttributeType == typeof(MasterAttribute)))
            {
                service = Activator.CreateInstance(serviceType, basicService) as IReplicable<User, Message<User>>;
            }
            else
            {
                if (attributes.Exists(attr => attr.AttributeType == typeof(SlaveAttribute)))
                {
                    service = Activator.CreateInstance(serviceType, basicService) as IReplicable<User, Message<User>>;
                }
            }

            var spreaderService = (DataSpreaderService)Activator.CreateInstance(typeof(DataSpreaderService), service);

            this.AddDataSpreaders(serviceElement, spreaderService);

            return spreaderService;
        }

        public void AddDataSpreaders(ServiceConfiguration serviceElement, DataSpreaderService service)
        {
            var spreaders = new List<IDataSpreader<Message<User>>>();

            foreach (var spreaderElement in serviceElement.DataSpreaders)
            {
                var endPoints = spreaderElement.EndPoints
                    .Select(end => new IPEndPoint(IPAddress.Parse(end.Ip), int.Parse(end.Port))).ToArray();

                var spreader = Activator.CreateInstance(Type.GetType(spreaderElement.DataSpreaderType, true, true), spreaderElement.Name, endPoints) as IDataSpreader<Message<User>>;

                if (spreader != null)
                {
                    spreaders.Add(spreader);
                }
            }

            foreach (var item in spreaders)
            {
                service.AddDataSpreader(item);
            }
        }
    }
}