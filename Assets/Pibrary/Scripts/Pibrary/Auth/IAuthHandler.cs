using System;
using Pibrary.Config;

namespace Pibrary.Auth
{
    public interface IAuthHandler
    {
        public IObservable<LoadingState> OnStateChanged { get; }
        public void CallGoogleSignIn();
        public void CallEmailSignIn(string email, string password);
    }
}