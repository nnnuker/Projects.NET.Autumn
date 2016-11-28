namespace MyServiceLibrary.Interfaces.Infrastructure
{
    public interface IGenerator<T>
    {
        T Current { get; }

        T GetNext();

        void Initialize(T start);
    }
}
