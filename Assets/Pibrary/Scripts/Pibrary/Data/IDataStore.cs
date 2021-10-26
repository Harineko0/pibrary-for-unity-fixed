using System;
using UniRx;

namespace Pibrary.Data
{
    public interface IDataStore<T>
    {
        public IObservable<T> SaveData { get; }
        public void Save(T data);
    }
}