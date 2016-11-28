using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Repositories
{
    public class UserMemoryRepository: IRepository<User>
    {
        private readonly List<User> users;

        public UserMemoryRepository()
        {
            users = new List<User>();
        }

        public bool Add(User user)
        {
            if (user != null && !users.Exists(u => u.Id == user.Id))
            {
                users.Add(user);
            }

            return false;
        }

        public IList<User> GetByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return users.FindAll(predicate);
        }

        public IList<User> GetAll()
        {
            return users;
        }

        public bool Delete(int userId)
        {
            if (userId <= 0)
                throw new ArgumentOutOfRangeException(nameof(userId));

            var findResult = users.Find(user => user.Id == userId);
            if (findResult != null)
            {
                return users.Remove(findResult);
            }

            return false;
        }

        public bool Load()
        {
            return true;
        }

        public bool Save()
        {
            return true;
        }
    }
}
