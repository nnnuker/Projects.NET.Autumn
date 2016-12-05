using System;

namespace MyServiceLibrary.Replication.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class SlaveAttribute : Attribute
    {
        public SlaveAttribute()
        {
        }
    }
}
