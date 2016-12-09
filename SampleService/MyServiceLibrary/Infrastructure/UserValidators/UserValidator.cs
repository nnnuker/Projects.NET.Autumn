using System;
using MyServiceLibrary.Entities;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.UserValidators
{
    public class UserValidator : IValidator<User>
    {
        public bool IsValid(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User is null when validate");
            }

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName)
                || string.IsNullOrEmpty(user.PersonalId) || user.Visas == null
                || user.DateOfBirth > DateTime.Now || !Enum.IsDefined(typeof(GenderEnum), user.Gender))
            {
                return false;
            }

            return true;
        }
    }
}
