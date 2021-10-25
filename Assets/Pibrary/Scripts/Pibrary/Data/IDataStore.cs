using UniRx;

namespace Pibrary.Data
{
    public interface IDataStore<T>
    {
        public T SaveData { get; }
        public void Save(T data);
        public T Load();
    }
}