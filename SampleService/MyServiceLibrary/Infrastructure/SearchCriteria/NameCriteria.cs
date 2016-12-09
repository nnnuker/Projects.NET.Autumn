using System;
using System.Runtime.Serialization;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    [Serializable]
    [DataContract]
    public class NameCriteria : ISearchCriteria<User>
    {
        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        public bool IsMatch(User data)
        {
            return data?.FirstName == this.FirstName && data?.LastName == this.LastName;
        }
    }
}
