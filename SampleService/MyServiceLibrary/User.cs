using System;

namespace MyServiceLibrary
{
    public class User: IEquatable<User>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public bool Equals(User other)
        {
            return other != null && FirstName.Equals(other.FirstName) &&
                LastName.Equals(other.LastName) && DateOfBirth.Equals(other.DateOfBirth) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals(obj as User);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ FirstName.GetHashCode() ^ LastName.GetHashCode() ^ DateOfBirth.GetHashCode();
        }
    }
}
