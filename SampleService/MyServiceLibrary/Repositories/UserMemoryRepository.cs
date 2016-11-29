using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Repositories.RepositoryStates;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyServiceLibrary.Repositories
{
    public class UserMemoryRepository : IRepository<User>
    {
        private readonly string filePath;
        private List<User> users = new List<User>();
        private readonly IGenerator<int> idGenerator;
        private readonly IStateSaver<UserRepositorySnapshot> stateSaver;

        public UserMemoryRepository()
        {
            filePath = Directory.GetCurrentDirectory() + @"\RepositoryStateSnapshot.xml";
            idGenerator = new IdGenerator();
        }

        public UserMemoryRepository(string filePath, IGenerator<int> idGenerator, IStateSaver<UserRepositorySnapshot> stateSaver)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException($"{nameof(filePath)} argument is null or empty string");

            if (idGenerator == null)
                throw new ArgumentNullException($"{nameof(idGenerator)} argument is null");

            this.filePath = filePath;
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

            stateSaver.Save(snapshot, filePath);

            return true;
        }

        public bool Load()
        {
            if (stateSaver == null)
                return false;

            var snapshot = stateSaver.Load(filePath);

            idGenerator.Initialize(snapshot.LastId);
            users = snapshot.Users;

            return true;
        }
    }
}
