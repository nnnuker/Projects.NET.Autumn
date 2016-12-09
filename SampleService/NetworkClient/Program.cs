using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Services.Factories;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Infrastructure.SearchCriteria;

namespace NetworkClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new BasicServiceFactory();

            var master = factory.RunServices().FirstOrDefault();

            Console.WriteLine("Enter to send");
            Console.ReadLine();

            try
            {
                Threads(master);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("enter to exit");
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

        private static void Threads(IService<User> master)
        {
            var mres = new ManualResetEventSlim();

            var threads = new List<Thread>();

            threads.AddRange(CreateWorkers(mres, () =>
            {
                var firstName = "first" + DateTime.Now.Ticks;

                try
                {
                    master.Add(new User()
                    {
                        FirstName = firstName,
                        LastName = "First",
                        PersonalId = "PiterSaint",
                        DateOfBirth = DateTime.MinValue,
                        Visas =
                        new VisaRecord[]
                        {
                            new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue }
                        },
                        Gender = GenderEnum.Male
                    });

                    Console.WriteLine(firstName);
                }
                catch (Exception)
                {
                }
            }, 10, 100));

            threads.AddRange(CreateWorkers(mres, () =>
            {
                var firstName = "second" + DateTime.Now.Ticks;
                try
                {
                    master.Add(new User()
                    {
                        FirstName = firstName,
                        LastName = "Second",
                        PersonalId = "PiterSaint",
                        DateOfBirth = DateTime.MinValue,
                        Visas =
                        new VisaRecord[]
                        {
                            new VisaRecord() { Country = "Netherlands", Start = DateTime.MinValue, End = DateTime.MaxValue }
                        },
                        Gender = GenderEnum.Female
                    });

                    Console.WriteLine(firstName);
                }
                catch (Exception)
                {
                }
            }, 10, 100));

            threads.AddRange(CreateWorkers(mres, () =>
            {
                var firstOrDefault = master.GetByPredicate(new GenderCriteria { Gender = GenderEnum.Male }).FirstOrDefault();
                if (firstOrDefault != null)
                {
                    try
                    {
                        var res = master.Delete(firstOrDefault);
                        Console.WriteLine(firstOrDefault.FirstName + " deleted: " + res);
                    }
                    catch (Exception)
                    {
                    }
                }
            }, 10, 100));

            foreach (var thread in threads)
            {
                thread.Start();
            }

            Console.WriteLine("Press any key to run unblock working threads.");
            Console.ReadKey();

            mres.Set();

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }
    }
}
