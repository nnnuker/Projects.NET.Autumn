using MyServiceLibrary.Interfaces.Entities;

namespace MyServiceLibrary.Interfaces.Infrastructure
{
    public interface IValidator<T> where T : IEntity
    {
        bool IsValid(T entity);
    }
}
