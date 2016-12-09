using System;

namespace MyServiceLibrary.Interfaces.Replication
{
    public interface IDataSpreadersChangeable<TData> where TData : EventArgs
    {
        void AddDataSpreader(IDataSpreader<TData> dataSpreader);

        void RemoveDataSpreader(string name);
    }
}