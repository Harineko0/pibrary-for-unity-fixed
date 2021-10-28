using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Google;
using Pibrary.Config;
using UniRx;
using Unity;
using UnityEngine;

namespace Pibrary.Auth
{
    public class TestAuthHandler : IAuthHandler
    {
        private Subject<LoadingState> stateSubject = new Subject<LoadingState>();
        public IObservable<LoadingState> OnStateChanged
        {
            get { return stateSubject; }
        }

        public Task<FirebaseUser> CallGoogleSignIn()
        {
            if (GoogleSignIn.Configuration == null)
            {
                string clientID = "852955764328-acsd9qpovds074rp7q1a53crcd94nsqt.apps.googleusercontent.com";
                GoogleSignIn.Configuration = new GoogleSignInConfiguration {
                    RequestIdToken = true,
                    // Copy this value from the google-service.json file.
                    // oauth_client with type == 3
                    WebClientId = clientID
                };
            }
            
            stateSubject.OnNext(LoadingState.Loading);
            Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

            signIn.ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.Log("GoogleSignIn was canceled.");
                } else if (task.IsFaulted) {
                    Debug.Log("GoogleSignIn was error.");
                } else {
                    // Credential credential = Firebase.Auth.GoogleAuthProvider.GetCredential(((Task<GoogleSignInUser>)task).Result.IdToken, null);
                }
            });

            return null;
        }

        public Task<FirebaseUser> CallEmailSignIn(string email, string password)
        {
            stateSubject.OnNext(LoadingState.Loading);
            return null;
        }
    }
}