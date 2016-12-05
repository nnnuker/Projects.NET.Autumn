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
            var slave1 = new DataSpreaderService(new SlaveService<User>(new BasicUserService()));

            var receiver = new NetworkDataReceiver("1", new IPEndPoint(IPAddress.Loopback, 8081));
            var receiver1 = new NetworkDataReceiver("1", new IPEndPoint(IPAddress.Loopback, 8082));

            slave.AddDataSpreader(receiver);
            slave1.AddDataSpreader(receiver1);

            while (true)
            {
                try
                {
                    if (GetSlaveData(slave))
                    {
                        GetSlaveData(slave1);
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static bool GetSlaveData(DataSpreaderService slave)
        {
            var obj = slave.GetAll().FirstOrDefault();
            if (obj != null)
            {
                Console.WriteLine("Received: " + obj.FirstName);
                return true;
            }

            return false;
        }
    }
}
