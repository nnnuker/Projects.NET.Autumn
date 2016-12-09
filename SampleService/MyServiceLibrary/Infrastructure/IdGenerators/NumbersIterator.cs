using System;
using System.Collections;
using System.Collections.Generic;

namespace MyServiceLibrary.Infrastructure.IdGenerators
{
    public class NumbersIterator : IEnumerator<int>
    {
        private int current;

        public int Current => this.current;

        object IEnumerator.Current => this.Current;

        public NumbersIterator()
        {
        }

        public void SetCurrent(int current)
        {
            this.current = current;
        }

        public bool MoveNext()
        {
            try
            {
                checked
                {
                    while (this.current < int.MaxValue)
                    {
                        this.current++;
                        if (this.IsSimple(this.current))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (OverflowException)
            {
                this.Reset();
                return false;
            }

            return true;
        }

        public void Reset()
        {
            this.current = 0;
        }

        public void Dispose()
        {
        }

        private bool IsSimple(int num)
        {
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
