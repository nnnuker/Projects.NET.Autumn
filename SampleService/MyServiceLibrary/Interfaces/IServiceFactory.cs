using MyServiceLibrary.Entities;

namespace MyServiceLibrary.Interfaces
{
    public interface IServiceFactory
    {
        IService<User> GetService();
    }
}
