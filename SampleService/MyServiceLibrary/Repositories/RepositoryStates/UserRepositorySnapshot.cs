using System;
using System.Collections.Generic;
using MyServiceLibrary.Entities;

namespace MyServiceLibrary.Repositories.RepositoryStates
{
    public class UserRepositorySnapshot
    {
        public int LastId { get; set; }

        public List<User> Users { get; set; }

        public UserRepositorySnapshot()
        {
            this.Users = new List<User>();
            this.LastId = 0;
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

            this.Users = users;
            this.LastId = lastId;
        }
    }
}
