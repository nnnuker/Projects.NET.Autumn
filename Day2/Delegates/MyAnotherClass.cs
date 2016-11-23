namespace Delegates
{
    public class MyAnotherClass
    {
        public event SimpleDelegate Delegate1 = delegate { return 0; };

        public event AdvancedDelegate Delegate2;
    }
}
