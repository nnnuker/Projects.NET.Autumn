using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Infrastructure.SearchCriteria
{
    public class GenderCriteria : ISearchCriteria<User>
    {
        public GenderEnum Gender { get; set; }

        public bool IsMatch(User data)
        {
            return data?.Gender == Gender;
        }
    }
}
