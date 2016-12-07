namespace MyServiceLibrary.Interfaces.Infrastructure
{
    public interface ISearchCriteria<T>
    {
        bool IsMatch(T data);
    }
}
