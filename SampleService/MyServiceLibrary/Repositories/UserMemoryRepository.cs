using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Repositories.RepositoryStates;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Repositories
{
    public class UserMemoryRepository : IRepository<User>
    {
        private List<User> users = new List<User>();
        private readonly IGenerator<int> idGenerator;
        private readonly IStateSaver<UserRepositorySnapshot> stateSaver;

        public UserMemoryRepository()
        {
            idGenerator = new IdGenerator();
        }

        public UserMemoryRepository(IGenerator<int> idGenerator, IStateSaver<UserRepositorySnapshot> stateSaver)
        {
            if (idGenerator == null)
                throw new ArgumentNullException($"{nameof(idGenerator)} argument is null");

            this.idGenerator = idGenerator;
            this.stateSaver = stateSaver;
        }

        public User Add(User user)
        {
            if (user == null)
                throw new ArgumentNullException($"{nameof(user)} argument is null");

            if (users.Exists(u => u.Equals(user)))
                throw new UserAlreadyExistsException($"User {user.FirstName} {user.LastName} already exists");

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

        public bool Save()
        {
            if (stateSaver == null)
                return false;

            var snapshot = new UserRepositorySnapshot(users, idGenerator.Current);

            stateSaver.Save(snapshot);

            return true;
        }

        public bool Load()
        {
            if (stateSaver == null)
                return false;

            var snapshot = stateSaver.Load();

            idGenerator.Initialize(snapshot.LastId);
            users = snapshot.Users;

            return true;
        }
    }
}
