using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bll.Interfaces
{
    public interface IAsyncService<T>: IDisposable where T : IBllEntity
    {
        Task<IList<T>> GetAll();

        Task<T> Get(int id);

        Task<T> Add(T item);

        Task<T> Remove(int id);

        Task<bool> Update(T item);
    }
}
