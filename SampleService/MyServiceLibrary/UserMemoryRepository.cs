using MyServiceLibrary.Exceptions;
using System;
using System.Collections.Generic;

namespace MyServiceLibrary
{
    public class UserMemoryRepository
    {
        private List<User> users = new List<User>();

        public User Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User argument is null when add user");
            }

            if (this.users.Contains(user))
            {
                throw new UserAlreadyExistsException("User already exists");
            }

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                throw new UserValidationException("User is invalid");
            }

            this.users.Add(user);

            return user;
        }

        public bool Delete(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User argument is null when delete user");
            }

            return users.Remove(user);
        }

        public IList<User> GetByPredicate(Predicate<User> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("Predicate argument null");
            }

            return this.users.FindAll(predicate);
        }
    }
}
