using System;
using System.Linq;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Services.Factories;

namespace NetworkServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new BasicServiceFactory();

            var services = factory.RunServices();

            var slave = services[0];
            var slave1 = services[1];

            Console.WriteLine($"Ready to receive: slave - {slave != null} ; slave1 - {slave1 != null}. Count of all - {services.Count}");

            while (true)
            {
                try
                {
                    if (GetSlaveData(slave, "slave"))
                    {
                        if (GetSlaveData(slave1, "slave1"))
                        {
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }

        private static bool GetSlaveData(DataSpreaderService slave, string args)
        {
            var obj = slave.GetAll().FirstOrDefault();
            if (obj != null)
            {
                Console.WriteLine($"Received from {args}: {obj.FirstName} {obj.LastName}");
                return true;
            }

            return false;
        }
    }
}
