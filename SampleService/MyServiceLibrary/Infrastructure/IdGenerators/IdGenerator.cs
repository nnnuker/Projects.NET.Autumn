using System;
using MyServiceLibrary.Interfaces.Infrastructure;

namespace MyServiceLibrary.Infrastructure.IdGenerators
{
    public class IdGenerator : IGenerator<int>
    {
        private readonly NumbersIterator iterator;

        public IdGenerator()
        {
            this.iterator = new NumbersIterator();
        }

        public int Current => this.iterator.Current;

        public int GetNext()
        {
            if (this.iterator.MoveNext())
            {
                return this.iterator.Current;
            }

            throw new InvalidOperationException();
        }

        public void Initialize(int start)
        {
            if (start > 0)
            {
                this.iterator.SetCurrent(start);
            }
        }
    }
}
