using System;
using System.Collections.Generic;
using System.Threading;
using MyServiceLibrary.Replication;
using MyServiceLibrary.Services.Factories;
using MyServiceLibrary.Infrastructure.SearchCriteria;
using MyServiceLibrary.Entities;

namespace NetworkServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new BasicServiceFactory();

            var services = factory.RunServices();

            var slave = services[0];
            var slave1 = services[1];

            Console.WriteLine($"Ready to receive: slave - {slave != null} ; slave1 - {slave1 != null}. Count of all - {services.Count}");

            var threads = new List<Thread>();

            var mres = new ManualResetEventSlim();

            threads.AddRange(Threads(mres, slave, "slave 1"));
            threads.AddRange(Threads(mres, slave1, "slave 2"));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            Console.WriteLine("Enter to start workers");
            Console.ReadLine();

            mres.Set();

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("Enter to exit");
            Console.ReadLine();
        }

        private static IList<Thread> CreateWorkers(ManualResetEventSlim mres, Action action, int threadsNum, int cycles)
        {
            var threads = new Thread[threadsNum];

            for (int i = 0; i < threadsNum; i++)
            {
                Action d = () =>
                {
                    mres.Wait();

                    for (int j = 0; j < cycles; j++)
                    {
                        action();
                    }
                };

                Thread thread = new Thread(new ThreadStart(d));

                threads[i] = thread;
            }

            return threads;
        }

        private static List<Thread> Threads(ManualResetEventSlim mres, DataSpreaderService service, string name)
        {
            var threads = new List<Thread>();

            threads.AddRange(CreateWorkers(mres, () =>
            {
                try
                {
                    var result = service.GetAll();

                    Console.WriteLine($"Service {name}: has {result.Count} users");
                }
                catch (Exception)
                {
                }
            }, 5, 20));

            threads.AddRange(CreateWorkers(mres, () =>
            {
                try
                {
                    var result = service.GetByPredicate(new GenderCriteria { Gender = GenderEnum.Male });

                    Console.WriteLine($"Service {name}: has {result?.Count} males");
                }
                catch (Exception)
                {
                }
            }, 5, 20));

            threads.AddRange(CreateWorkers(mres, () =>
            {
                try
                {
                    var result = service.GetByPredicate(new PersonalIdCriteria { PersonalId = "PiterSaint" });

                    Console.WriteLine($"Service {name}: has {result?.Count} personal ids");
                }
                catch (Exception)
                {
                }
            }, 5, 20));

            return threads;
        }
    }
}
