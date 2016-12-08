using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace MyServiceLibrary.Interfaces
{
    [ServiceContract]
    public interface IService<T> where T:IEntity
    {
        [OperationContract]
        T Add(T user);

        [OperationContract]
        bool Delete(T user);

        [OperationContract]
        IList<T> GetAll();

        [OperationContract]
        IList<T> GetByPredicate(ISearchCriteria<T> predicate);

        [OperationContract]
        bool Save();

        [OperationContract]
        bool Load();
    }
}
