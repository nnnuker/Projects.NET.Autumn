using System;

namespace MyServiceLibrary.Replication.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class MasterAttribute : Attribute
    {
        public MasterAttribute()
        {
        }
    }
}
