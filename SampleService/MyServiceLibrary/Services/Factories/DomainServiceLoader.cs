using System;
using System.Linq;
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

            object idGenerator = Activator.CreateInstance(Type.GetType(serviceElement.GeneratorType, true, true)) as IGenerator<int>;
            object saver = Activator.CreateInstance(Type.GetType(serviceElement.RepositoryStateSaverType, true, true)) as IStateSaver<UserRepositorySnapshot>;

            object repository = Activator.CreateInstance(Type.GetType(serviceElement.RepositoryType, true, true), idGenerator, saver) as IRepository<User>;
            object validator = Activator.CreateInstance(Type.GetType(serviceElement.ValidatorType, true, true)) as IValidator<User>;

            object basicService = Activator.CreateInstance(typeof(BasicUserService), repository, validator) as IService<User>;

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

            return (DataSpreaderService)Activator.CreateInstance(typeof(DataSpreaderService), service);
        }
    }
}