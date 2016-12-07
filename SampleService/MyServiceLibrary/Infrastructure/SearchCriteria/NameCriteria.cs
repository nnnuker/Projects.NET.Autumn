using System;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    [Serializable]
    public class NameCriteria : ISearchCriteria<User>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsMatch(User data)
        {
            return data?.FirstName == FirstName && data?.LastName == LastName;
        }
    }
}
