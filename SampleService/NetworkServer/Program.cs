using System;
using System.Linq;
using System.Net;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Replication.DataSpreader;
using MyServiceLibrary.Services;

namespace NetworkServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var slave = new DataSpreaderService(new SlaveService<User>(new BasicUserService()));

            var receiver = new NetworkDataReceiver("1", new IPEndPoint(IPAddress.Loopback, 8081));

            slave.AddDataSpreader(receiver);

            while (true)
            {
                try
                {
                    var obj = slave.GetAll().FirstOrDefault();
                    if (obj != null)
                    {
                        Console.WriteLine("Received: " + obj.FirstName);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
