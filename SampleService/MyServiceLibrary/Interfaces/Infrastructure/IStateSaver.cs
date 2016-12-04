namespace MyServiceLibrary.Interfaces.Infrastructure
{
    public interface IStateSaver<T>
    {
        T Load();

        void Save(T state);
    }
}
