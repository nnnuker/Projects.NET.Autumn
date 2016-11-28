using MyServiceLibrary.Interfaces.Infrastructure;
using System;

namespace MyServiceLibrary.Infrastructure.IdGenerators
{
    public class IdGenerator : IGenerator<int>
    {
        private readonly NumbersIterator iterator;

        public IdGenerator()
        {
            iterator = new NumbersIterator();
        }

        public int Current
        {
            get
            {
                return iterator.Current;
            }
        }

        public int GetNext()
        {
            if (iterator.MoveNext())
            {
                return iterator.Current;
            }

            throw new InvalidOperationException();
        }

        public void Initialize(int start)
        {
            if (start > 0)
            {
                iterator.SetCurrent(start);
            }
        }
    }
}
