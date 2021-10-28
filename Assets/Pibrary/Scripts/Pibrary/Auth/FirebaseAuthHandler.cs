using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Pibrary.Config;
using Pibrary.Regex;
using UniRx;
using UnityEngine;

namespace Pibrary.Auth
{
    public class FirebaseAuthHandler : IAuthHandler
    {
        private Subject<LoadingState> stateSubject = new Subject<LoadingState>();
        public IObservable<LoadingState> OnStateChanged
        {
            get { return stateSubject; }
        }
        
        private IGoogleAuthHandler googleAuth = new MobileGoogleAuthHandler();

        private FirebaseAuth auth = FirebaseAuth.DefaultInstance;

        public async Task<FirebaseUser> CallGoogleSignIn()
        {
            stateSubject.OnNext(LoadingState.Loading);

            string idToken = await googleAuth.getIdToken();
            Credential credential = GoogleAuthProvider.GetCredential(idToken, null);
            Task<FirebaseUser> signInTask = auth.SignInWithCredentialAsync(credential);
            var user = await SignIn(signInTask);
            
            stateSubject.OnNext(LoadingState.Completed);
            return user;
        }

        public async Task<FirebaseUser> CallEmailSignIn(string email, string password)
        {
            stateSubject.OnNext(LoadingState.Loading);

            if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) && RegexUtilities.IsValidEmail(email))
            {
                var signInTask = auth.SignInWithEmailAndPasswordAsync(email, password);
                var user = await SignIn(signInTask);

                return user;
            }

            return null;
        }

        private async Task<FirebaseUser> SignIn(Task<Firebase.Auth.FirebaseUser> signInTask)
        {
            await signInTask;
            
            if (signInTask.IsCanceled)
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return null;
            }
            if (signInTask.IsFaulted) {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + signInTask.Exception);
                return null;
            }
            
            Firebase.Auth.FirebaseUser newUser = signInTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            return newUser;
        }
    }
}