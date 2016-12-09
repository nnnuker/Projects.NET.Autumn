using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Services.Factories;

namespace WcfServiceHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string baseAddress = "http://localhost:8085/Services/";

            DataSpreaderService service = new BasicServiceFactory().RunServices().First();
            
            using (var host = new ServiceHost(service, new Uri($"{baseAddress}Service{service.ServiceMode}1")))
            {
                var smb = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true,
                    MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                };
                host.Description.Behaviors.Add(smb);

                host.Open();
                
                Console.WriteLine($"Service hosted {host.BaseAddresses[0]}");
            }

            Console.WriteLine("Enter to stop the service.");
            Console.ReadLine();
        }
    }
}
