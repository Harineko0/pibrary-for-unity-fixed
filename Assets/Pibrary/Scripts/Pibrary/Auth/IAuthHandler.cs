using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Pibrary.Config;

namespace Pibrary.Auth
{
    public interface IAuthHandler
    {
        public IObservable<LoadingState> OnStateChanged { get; }
        public Task<FirebaseUser> CallGoogleSignIn();
        public Task<FirebaseUser> CallEmailSignIn(string email, string password);
    }
}