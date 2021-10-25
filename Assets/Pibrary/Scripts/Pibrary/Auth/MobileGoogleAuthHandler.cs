using System.Threading.Tasks;
using Google;
using Pibrary.Config;
using UnityEngine;

namespace Pibrary.Auth
{
    public class MobileGoogleAuthHandler : IGoogleAuthHandler
    {
        private bool initialized;
        
        public async Task<string> getIdToken()
        {
            if (GoogleSignIn.Configuration == null)
            {
                string clientID = "852955764328-acsd9qpovds074rp7q1a53crcd94nsqt.apps.googleusercontent.com";
                Debug.Log(clientID);
                GoogleSignIn.Configuration = new GoogleSignInConfiguration {
                    RequestIdToken = true,
                    WebClientId = clientID
                };
                initialized = true;
            }
            
            Task<GoogleSignInUser> signInTask = GoogleSignIn.DefaultInstance.SignIn();
            
            await signInTask;
            
            if (signInTask.IsCanceled)
            {
                Debug.LogError("GoogleSignIn was canceled.");
                return "";
            }
            if (signInTask.IsFaulted) {
                Debug.LogError("GoogleSignIn was error.");
                return "";
            }
            
            return signInTask.Result.IdToken;
        }
    }
}