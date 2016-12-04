using MyServiceLibrary.Interfaces.Entities;
using System;

namespace MyServiceLibrary.Entities
{
    [Serializable]
    public class User: IEntity, IEquatable<User>
    {
        #region Properties
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PersonalId { get; set; }

        public GenderEnum Gender { get; set; }

        public VisaRecord[] Visas { get; set; }
        #endregion

        #region Public methods
        public bool Equals(User other)
        {
            return FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName)
                && DateOfBirth.Equals(other.DateOfBirth)
                && PersonalId.Equals(other.PersonalId) && Gender.Equals(other.Gender);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User other = (User)obj;

            return Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;

                //hash = (hash * 16777619) ^ Id.GetHashCode();
                hash = (hash * 16777619) ^ (PersonalId?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (FirstName?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (LastName?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ DateOfBirth.GetHashCode();
                hash = (hash * 16777619) ^ Gender.GetHashCode();

                return hash;
            }
        }
        #endregion
    }
}
