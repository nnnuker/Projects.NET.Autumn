using System;
using System.Runtime.Serialization;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    [Serializable]
    [DataContract]
    public class GenderCriteria : ISearchCriteria<User>
    {
        [DataMember]
        public GenderEnum Gender { get; set; }
        
        public bool IsMatch(User data)
        {
            return data?.Gender == this.Gender;
        }
    }
}
