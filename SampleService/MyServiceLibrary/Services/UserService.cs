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
        private readonly IGenerator<int> idGenerator;
        private readonly IValidator<User> validator;

        public UserService()
        {
            repository = new UserXmlRepository();
            idGenerator = new IdGenerator();
            validator = new UserValidator();
        }

        public UserService(IRepository<User> repository, IGenerator<int> idGenerator, IValidator<User> validator)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("Repository is null");
            }

            if (idGenerator == null)
            {
                throw new ArgumentNullException("Id generator is null");
            }

            if (validator == null)
            {
                throw new ArgumentNullException("Validator is null");
            }

            this.repository = repository;
            repository.Load();

            this.validator = validator;

            this.idGenerator = idGenerator;
            idGenerator.Initialize(repository.GetAll().Max(u => u.Id));
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

            if (repository.GetByPredicate(u => u.Equals(user)).Count > 0)
            {
                throw new UserAlreadyExistsException("User already exists");
            }

            user.Id = idGenerator.GetNext();
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
