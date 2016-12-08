using MyServiceLibrary.Configurations;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Services.Factories;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8085/Services/";
            
            List<DataSpreaderService> services = new BasicServiceFactory().RunServices();

            var serviceHosts = new List<ServiceHost>();

            int i = 0;
            foreach (DataSpreaderService service in services)
            {
                i++;
                var host = new ServiceHost(service, new Uri($"{baseAddress}Service{service.ServiceMode}{i}"));

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                };
                host.Description.Behaviors.Add(smb);

                host.Open();

                serviceHosts.Add(host);

                Console.WriteLine($"Service hosted {host.BaseAddresses[0]}");
            }
            Console.WriteLine("Press <Enter> to stop the services.");
            Console.ReadLine();

            serviceHosts.ForEach(s => s.Close());
        }
    }
}
