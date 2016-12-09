using System;
using System.Collections.Generic;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.UserValidators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Repositories;

namespace MyServiceLibrary.Services
{
    public class BasicUserService : IService<User>
    {
        private readonly IRepository<User> repository;
        private readonly IValidator<User> validator;

        public BasicUserService()
        {
            this.repository = new UserMemoryRepository();
            this.validator = new UserValidator();
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

            this.validator = validator;
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException($"{nameof(user)} argument is null when add user");
            }

            if (!this.validator.IsValid(user))
            {
                throw new UserValidationException($"User is invalid");
            }

            user = this.repository.Add(user);

            return user;
        }

        public bool Delete(User user)
        {
            return this.repository.Delete(user.Id);
        }

        public IList<User> GetAll()
        {
            return this.repository.GetAll();
        }

        public IList<User> GetByPredicate(ISearchCriteria<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException($"{nameof(predicate)} argument null");
            }

            return this.repository.GetByPredicate(predicate.IsMatch);
        }

        public bool Load()
        {
            return this.repository.Load();
        }

        public bool Save()
        {
            return this.repository.Save();
        }
    }
}
