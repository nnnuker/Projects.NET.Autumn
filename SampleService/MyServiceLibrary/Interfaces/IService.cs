using MyServiceLibrary.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Interfaces
{
    public interface IService<T> where T:IEntity
    {
        T Add(T user);

        bool Delete(int id);

        IList<T> GetAll();

        IList<T> GetByPredicate(Predicate<T> predicate);

        void Save();

        void Load();
    }
}
