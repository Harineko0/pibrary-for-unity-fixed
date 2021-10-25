using System;

namespace Pibrary.Config
{
    public enum LoadingState
    {
        WaitingToLoad,
        Loading,
        Completed,
    }

    interface IConfigLoader
    {
        public IObservable<LoadingState> OnStateChanged { get; }
        public PibraryConfig Config { get; }
        
    }
}