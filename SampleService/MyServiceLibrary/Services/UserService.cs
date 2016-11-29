using MyServiceLibrary.Entities;
using MyServiceLibrary.Exceptions;
using MyServiceLibrary.Infrastructure.IdGenerators;
using MyServiceLibrary.Infrastructure.UserValidators;
using MyServiceLibrary.Interfaces;
using MyServiceLibrary.Interfaces.Infrastructure;
using MyServiceLibrary.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyServiceLibrary.Services
{
    public class UserService : IService<User>
    {
        private readonly IRepository<User> repository;
        private readonly IValidator<User> validator;

        public UserService()
        {
            repository = new UserMemoryRepository();
            validator = new UserValidator();
        }

        public UserService(IRepository<User> repository, IValidator<User> validator)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("Repository is null");
            }

            if (validator == null)
            {
                throw new ArgumentNullException("Validator is null");
            }

            this.repository = repository;
            repository.Load();

            this.validator = validator;
        }

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User argument is null when add user");
            }

            if (!validator.IsValid(user))
            {
                throw new UserValidationException("User is invalid");
            }
            
            repository.Add(user);

            return user;
        }

        public bool Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User argument is null when delete user");
            }

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
                throw new ArgumentNullException("Predicate argument null");
            }

            return repository.GetByPredicate(predicate);
        }
    }
}
