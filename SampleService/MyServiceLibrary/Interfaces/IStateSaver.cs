namespace MyServiceLibrary.Interfaces
{
    public interface IStateSaver<T>
    {
        T Load(string filePath);

        void Save(T state, string filePath);
    }
}
