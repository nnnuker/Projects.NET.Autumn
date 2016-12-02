using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.UserValidators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Repositories;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary.Services
{
    public class BasicUserService : IService<User>
    {
        private readonly IRepository<User> repository;
        private readonly IValidator<User> validator;

        public BasicUserService()
        {
            repository = new UserMemoryRepository();
            validator = new UserValidator();
        }

        public BasicUserService(IRepository<User> repository, IValidator<User> validator)
        {
            if (repository == null)
            {
                throw new ArgumentNullException($"{nameof(repository)} argument is null");
            }

            if (validator == null)
            {
                throw new ArgumentNullException($"{nameof(validator)} argument is null");
            }

            this.repository = repository;
            repository.Load();

            this.validator = validator;
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)} argument is null when add user");
            }

            if (!validator.IsValid(user))
            {
                throw new UserValidationException($"User is invalid");
            }

            user = repository.Add(user);

            return user;
        }

        public bool Delete(User user)
        {
            return repository.Delete(user.Id);
        }

        public IList<User> GetAll()
        {
            return repository.GetAll();
        }

        public IList<User> GetByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} argument null");
            }

            return repository.GetByPredicate(predicate);
        }

        public void Load()
        {
            repository.Load();
        }

        public void Save()
        {
            repository.Save();
        }
    }
}
