using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Repositories
{
    public class UserMemoryRepository: IRepository<User>
    {
        private readonly List<User> users = new List<User>();
        private readonly IGenerator<int> idGenerator;

        public UserMemoryRepository()
        {
            idGenerator = new IdGenerator();
        }

        public UserMemoryRepository(IGenerator<int> idGenerator)
        {
            if (idGenerator == null)
            {
                throw new ArgumentNullException($"{nameof(idGenerator)} argument is null");
            }
            
            this.idGenerator = idGenerator;
        }

        public User Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException($"{nameof(user)} argument is null");

            if (users.Exists(u => u.Equals(user)))
            {
                throw new UserAlreadyExistsException($"User {user.FirstName} {user.LastName} already exists");
            }

            user.Id = idGenerator.GetNext();
            users.Add(user);
            return user;
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
