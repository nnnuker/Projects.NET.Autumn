using System;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    [Serializable]
    public class GenderCriteria : ISearchCriteria<User>
    {
        public GenderEnum Gender { get; set; }

        public bool IsMatch(User data)
        {
            return data?.Gender == Gender;
        }
    }
}
