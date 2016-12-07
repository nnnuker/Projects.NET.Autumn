using System;
using System.Linq;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Services.Factories;

namespace NetworkClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new BasicServiceFactory();

            var client = factory.RunServices().FirstOrDefault();
           
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
