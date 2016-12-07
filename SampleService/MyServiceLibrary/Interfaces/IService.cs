using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Interfaces
{
    public interface IService<T> where T:IEntity
    {
        T Add(T user);

        bool Delete(T user);

        IList<T> GetAll();

        IList<T> GetByPredicate(ISearchCriteria<T> predicate);

        bool Save();

        bool Load();
    }
}
