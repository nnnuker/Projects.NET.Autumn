using System.Collections.Generic;
using System.ServiceModel;
using MyServiceLibrary.Infrastructure.SearchCriteria;
using MyServiceLibrary.Interfaces.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Interfaces
{
    [ServiceContract]
    [ServiceKnownType(typeof(GenderCriteria))]
    [ServiceKnownType(typeof(NameCriteria))]
    [ServiceKnownType(typeof(PersonalIdCriteria))]
    public interface IService<T> where T : IEntity
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
