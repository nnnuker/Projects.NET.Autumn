using MyServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyServiceLibrary.Entities;

namespace MyServiceLibrary.Services.Factories
{
    public class BasicServiceFactory : IServiceFactory
    {
        public IService<User> GetService()
        {
            throw new NotImplementedException();
        }
    }
}
