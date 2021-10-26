using Google;
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
            var pibrary = Pibrary.DefaultInstance;
            pibrary.Initialize();
            var authHandler = pibrary.AuthHandler;
            var dataStore = pibrary.DataStore;
            
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

            dataStore.SaveData.Subscribe(data =>
            {
                if (data.purchased)
                {
                    log.text = "purchased";
                }
            });
        }
    }
}