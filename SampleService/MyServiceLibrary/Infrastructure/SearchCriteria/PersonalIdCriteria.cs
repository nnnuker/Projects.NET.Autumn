using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    public class PersonalIdCriteria:ISearchCriteria<User>
    {
        public string PersonalId { get; set; }

        public bool IsMatch(User data)
        {
            return data?.PersonalId == PersonalId;
        }
    }
}
