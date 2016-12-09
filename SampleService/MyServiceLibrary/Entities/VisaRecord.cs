using System;
using System.Runtime.Serialization;

namespace MyServiceLibrary.Entities
{
    [Serializable]
    [DataContract]
    public struct VisaRecord
    {
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;

                hash = (hash * 16777619) ^ (this.Country?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ this.Start.GetHashCode();
                hash = (hash * 16777619) ^ this.End.GetHashCode();
                return hash;
            }
        }
    }
}
