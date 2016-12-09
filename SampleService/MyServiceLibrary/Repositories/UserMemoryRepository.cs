using System;
using System.Collections.Generic;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Repositories.RepositoryStates;

namespace MyServiceLibrary.Repositories
{
    public class UserMemoryRepository : IRepository<User>
    {
        private readonly IGenerator<int> idGenerator;
        private readonly IStateSaver<UserRepositorySnapshot> stateSaver;
        private List<User> users = new List<User>();

        public UserMemoryRepository()
        {
            this.idGenerator = new IdGenerator();
        }

        public UserMemoryRepository(IGenerator<int> idGenerator, IStateSaver<UserRepositorySnapshot> stateSaver)
        {
            if (idGenerator == null)
            {
                throw new ArgumentNullException($"{nameof(idGenerator)} argument is null");
            }

            this.idGenerator = idGenerator;
            this.stateSaver = stateSaver;
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)} argument is null");
            }

            if (this.users.Exists(u => u.Equals(user)))
            {
                throw new UserAlreadyExistsException($"User {user.FirstName} {user.LastName} already exists");
            }

            user.Id = this.idGenerator.GetNext();
            this.users.Add(user);
            return user;
        }

        public IList<User> GetByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return this.users.FindAll(predicate);
        }

        public IList<User> GetAll()
        {
            return this.users;
        }

        public bool Delete(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var findResult = this.users.Find(user => user.Id == userId);

            if (findResult != null)
            {
                return this.users.Remove(findResult);
            }

            return false;
        }

        public bool Save()
        {
            if (this.stateSaver == null)
            {
                return false;
            }

            var snapshot = new UserRepositorySnapshot(this.users, this.idGenerator.Current);

            this.stateSaver.Save(snapshot);

            return true;
        }

        public bool Load()
        {
            if (this.stateSaver == null)
            {
                return false;
            }

            var snapshot = this.stateSaver.Load();

            this.idGenerator.Initialize(snapshot.LastId);
            this.users = snapshot.Users;

            return true;
        }
    }
}
