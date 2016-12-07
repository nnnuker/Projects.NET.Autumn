using System;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    [Serializable]
    public class PersonalIdCriteria:ISearchCriteria<User>
    {
        public string PersonalId { get; set; }

        public bool IsMatch(User data)
        {
            return data?.PersonalId == PersonalId;
        }
    }
}
