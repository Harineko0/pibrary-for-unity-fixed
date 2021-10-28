using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase;
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

        private bool isMobile = Application.platform == RuntimePlatform.IPhonePlayer ||
                                Application.platform == RuntimePlatform.Android;
        
        public async Task<FirebaseUser> CallGoogleSignIn()
        {
            if (!isMobile)
            {
                return null;
            }

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

        private async Task<FirebaseUser> SignIn(Task<FirebaseUser> signInTask)
        {
            try
            {
                FirebaseUser newUser = await signInTask;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                return newUser;
            }
            catch
            {
                return null;
            }
        }
    }
}