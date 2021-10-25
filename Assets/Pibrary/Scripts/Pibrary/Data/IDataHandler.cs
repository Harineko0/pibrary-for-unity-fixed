using System;
using System.Threading.Tasks;
using Pibrary.Config;
using UniRx;

namespace Pibrary.Data
{
    public interface IDataHandler
    {
        public IObservable<LoadingState> OnStateChanged { get;  }
        public IReadOnlyReactiveProperty<UserData> UserData { get; }
        public Task<UserData> FetchUserData(string uid);
    }
}