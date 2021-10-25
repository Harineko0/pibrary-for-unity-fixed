using Google;
using Pibrary.Auth;
using Pibrary.Config;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Pibrary.Presenters
{
    public class AuthPresenter : MonoBehaviour
    {
        [SerializeField] private Button googleButton;
        [SerializeField] private Button emailButton;
        [SerializeField] private InputField emailField;
        [SerializeField] private InputField passwordField;
        [SerializeField] private Button signOutButton;
        [SerializeField] private Text log;
        
        private void Start()
        {
            Pibrary.DefaultInstance.Initialize();
            
            IAuthHandler authHandler = Pibrary.DefaultInstance.AuthHandler;
            
            googleButton
                .OnClickAsObservable()
                .Subscribe(_ => authHandler.CallGoogleSignIn())
                .AddTo(this);

            emailButton
                .OnClickAsObservable()
                .Subscribe(_ => authHandler.CallEmailSignIn(emailField.text, passwordField.text))
                .AddTo(this);
            
            signOutButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    log.text = "Sign Out";
                    GoogleSignIn.DefaultInstance.SignOut();
                })
                .AddTo(this);

            authHandler.OnStateChanged
                .Subscribe(state =>
                {
                    if (state == LoadingState.Loading)
                    {
                        Debug.Log("Loading");
                        log.text = "Loading";
                    }
                    if (state == LoadingState.Completed)
                    {
                        Debug.Log("Complete");
                        
                        log.text = "Complete";
                    }
                });
        }
        
        
        Firebase.Auth.FirebaseAuth auth;
        Firebase.Auth.FirebaseUser user;

// Handle initialization of the necessary firebase modules:
        void InitializeFirebase() {
            Debug.Log("Setting up Firebase Auth");
            auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }

// Track state changes of the auth object.
        void AuthStateChanged(object sender, System.EventArgs eventArgs) {
            if (auth.CurrentUser != user) {
                bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
                if (!signedIn && user != null) {
                    Debug.Log("Signed out " + user.UserId);
                    log.text = "Sign out";
                }
                user = auth.CurrentUser;
                if (signedIn) {
                    Debug.Log("Signed in " + user.UserId);
                    log.text = "Signed in " + user.UserId;
                }
            }
        }

        void OnDestroy() {
            auth.StateChanged -= AuthStateChanged;
            auth = null;
        }
    }
}