using System;
using System.Runtime.Serialization;
using MyServiceLibrary.Interfaces.Entities;

namespace MyServiceLibrary.Entities
{
    [Serializable]
    [DataContract]
    public class User : IEntity, IEquatable<User>
    {
        #region Properties
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public DateTime DateOfBirth { get; set; }

        [DataMember]
        public string PersonalId { get; set; }

        [DataMember]
        public GenderEnum Gender { get; set; }

        [DataMember]
        public VisaRecord[] Visas { get; set; }
        #endregion

        #region Public methods
        public bool Equals(User other)
        {
            return this.FirstName.Equals(other.FirstName) && this.LastName.Equals(other.LastName)
                && this.DateOfBirth.Equals(other.DateOfBirth)
                && this.PersonalId.Equals(other.PersonalId) && this.Gender.Equals(other.Gender);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            User other = (User)obj;

            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;
                
                hash = (hash * 16777619) ^ (this.PersonalId?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (this.FirstName?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ (this.LastName?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ this.DateOfBirth.GetHashCode();
                hash = (hash * 16777619) ^ this.Gender.GetHashCode();

                return hash;
            }
        }
        #endregion
    }
}
