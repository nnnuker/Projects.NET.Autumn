using MyServiceLibrary.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Interfaces
{
    public interface IService<T> where T:IEntity
    {
        T Add(T user);

        bool Delete(T user);

        IList<T> GetAll();

        IList<T> GetByPredicate(Predicate<T> predicate);
    }
}
