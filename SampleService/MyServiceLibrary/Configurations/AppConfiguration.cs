using MyServiceLibrary.Configurations.CustomServiceSections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using MyServiceLibrary.Configurations.CustomServiceSections.CustomServiceProperties.DataSpreaders;
using MyServiceLibrary.Configurations.SerializableConfiguration;
using MyServiceLibrary.Replication;

namespace MyServiceLibrary.Configurations
{
    public static class AppConfiguration
    {
        public static List<ServiceConfiguration> GetServices()
        {
            var section = ConfigurationManager.GetSection("Replication") as ServicesSection;

            if (section == null)
                throw new ConfigurationErrorsException("Replication section not found");

            var serviceElements = section.Services.OfType<ServiceElement>();

            return (from serviceElement in serviceElements
                    let dataSpreaderElements = serviceElement.DataSpreaders.OfType<DataSpreaderElement>()
                    let dataSpreaders = (from dataSpreaderElement in dataSpreaderElements
                                         let endPointElements = dataSpreaderElement.Ips.OfType<DataSpreaderIpElement>()
                                         select new DataSpreaderConfiguration
                                         {
                                             Name = dataSpreaderElement.Name,
                                             DataSpreaderType = dataSpreaderElement.DataSpreaderType,
                                             EndPoints = endPointElements.Select(end => new EndPointConfiguration
                                             {
                                                 Ip = end.Ip,
                                                 Port = end.Port
                                             }).ToList()
                                         }).ToList()
                    select new ServiceConfiguration
                    {
                        DomainName = serviceElement.DomainName,
                        ServiceType = serviceElement.ServiceType,
                        ValidatorType = serviceElement.Validator.ValidatorType,
                        GeneratorType = serviceElement.Generator.GeneratorType,
                        RepositoryType = serviceElement.Repository.RepositoryType,
                        RepositoryPath = serviceElement.Repository.Path,
                        RepositoryStateSaverType = serviceElement.Repository.StateSaver,
                        IsMaster = serviceElement.IsMaster,
                        DataSpreaders = dataSpreaders
                    }).ToList();
        }
    }
}
