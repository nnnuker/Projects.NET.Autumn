using MyServiceLibrary.Entities;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Repositories.RepositoryStates
{
    public class UserRepositorySnapshot
    {
        public int LastId { get; set; }
        public List<User> Users { get; set; }

        public UserRepositorySnapshot()
        {
            Users = new List<User>();
            LastId = 0;
        }

        public UserRepositorySnapshot(List<User> users, int lastId)
        {
            if (users == null)
            {
                throw new ArgumentNullException($"{nameof(users)} argument is null");
            }

            if (lastId < 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(lastId)} argument is out of range");
            }

            Users = users;
            LastId = lastId;
        }
    }
}
