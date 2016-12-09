using System;
using System.Runtime.Serialization;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    [Serializable]
    [DataContract]
    public class PersonalIdCriteria : ISearchCriteria<User>
    {
        [DataMember]
        public string PersonalId { get; set; }

        public bool IsMatch(User data)
        {
            return data?.PersonalId == this.PersonalId;
        }
    }
}
