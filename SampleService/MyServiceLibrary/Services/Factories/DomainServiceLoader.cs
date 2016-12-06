using System;
using System.Linq;
using MyServiceLibrary.Configurations.CustomServiceSections;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Replication.Attributes;

namespace MyServiceLibrary.Services.Factories
{
    public class DomainServiceLoader : MarshalByRefObject
    {
        //public DataSpreaderService GetService(ServiceElement serviceElement)
        //{
        //    if (serviceElement == null)
        //    {
        //        throw new ArgumentNullException(nameof(serviceElement));
        //    }

        //    object idGenerator = Activator.CreateInstance(Type.GetType(serviceElement.Generator.GeneratorType, true, true));
        //    object saver = Activator.CreateInstance(Type.GetType(serviceElement.Repository.StateSaver, true, true));
        //    object repository = Activator.CreateInstance(Type.GetType(serviceElement.Repository.RepositoryType, true, true), idGenerator, saver);
        //    object validator = Activator.CreateInstance(Type.GetType(serviceElement.Validator.ValidatorType, true, true));

        //    object basicService = Activator.CreateInstance(typeof(BasicUserService), repository, validator);

        //    var serviceType = Type.GetType(serviceElement.ServiceType, true, true);
        //    var attributes = serviceType.GetCustomAttributesData().ToList();

        //    object service = null;

        //    if (attributes.Exists(attr => attr.AttributeType == typeof(MasterAttribute)))
        //    {
        //        service = Activator.CreateInstance(serviceType, basicService);
        //    }
        //    else
        //    {
        //        if (attributes.Exists(attr => attr.AttributeType == typeof(SlaveAttribute)))
        //        {
        //            service = Activator.CreateInstance(serviceType, basicService);
        //        }
        //    }

        //    return (DataSpreaderService)Activator.CreateInstance(typeof(DataSpreaderService), service);
        //}

        public DataSpreaderService GetService()
        {
            return new DataSpreaderService(new MasterService<User>(new BasicUserService()));
        }
    }
}