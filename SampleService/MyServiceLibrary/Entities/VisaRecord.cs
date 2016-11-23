﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServiceLibrary.Entities
{
    public struct VisaRecord
    {
        public string Country { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = (int)2166136261;

                hash = (hash * 16777619) ^ (Country?.GetHashCode() ?? 0);
                hash = (hash * 16777619) ^ Start.GetHashCode();
                hash = (hash * 16777619) ^ End.GetHashCode();
                return hash;
            }
        }
    }
}
