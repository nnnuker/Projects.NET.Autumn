using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Replication.DataSpreader;
using MyServiceLibrary.Services;
using System.Threading;

namespace NetworkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new DataSpreaderService(new MasterService<User>(new BasicUserService()));

            var sender = new NetworkDataSender("1", new IPEndPoint(IPAddress.Loopback, 8081), new IPEndPoint(IPAddress.Loopback, 8082));

            client.AddDataSpreader(sender);

            Console.WriteLine("Enter to send");
            Console.ReadLine();

            try
            {
                client.Add(new User()
                {
                    FirstName = "Petr",
                    LastName = "The greatest",
                    PersonalId = "PiterSaint",
                    DateOfBirth = DateTime.MinValue,
                    Visas =
                        new VisaRecord[]
                            {new VisaRecord() {Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue}},
                    Gender = GenderEnum.Male
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("enter to exit");
            Console.ReadLine();
        }
    }
}
