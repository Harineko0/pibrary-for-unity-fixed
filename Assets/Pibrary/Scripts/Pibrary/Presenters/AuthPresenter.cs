using System;
using Pibrary.Auth;
using Pibrary.Data;
using Pibrary.Regex;
using Pibrary.UI.Alert;
using TMPro;
using UniRx;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Pibrary.Presenters
{
    public class AuthPresenter : MonoBehaviour
    {
        [SerializeField] private Button googleButton;
        [SerializeField] private Button emailButton;
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;

        [SerializeField] private AlertController alert;

        private ReactiveProperty<bool> loading = new ReactiveProperty<bool>(false);
        private IAuthHandler authHandler;
        
        private void Start()
        {
            var pibrary = Pibrary.DefaultInstance;
            pibrary.Initialize();
            authHandler = pibrary.AuthHandler;
            var dataStore = pibrary.DataStore;
            
            googleButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(1000))
                .Where(_ => loading.Value == false)
                .Subscribe(OnGoogleAuth)
                .AddTo(this);

            emailButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(1000))
                .Where(_ => loading.Value == false)
                .Subscribe(OnEmailAuth)
                .AddTo(this);
        }

        private async void OnEmailAuth(Unit unit)
        {
            loading.Value = true;
            
            string email = emailField.text;
            string password = passwordField.text;
                    
            if (String.IsNullOrEmpty(email))
            {
                alert.Alert("メールアドレスを入力してください", AlertType.warning);
            }
            else if (!RegexUtilities.IsValidEmail(email))
            {
                alert.Alert("メールアドレスが無効です", AlertType.warning);
            }
            else if (String.IsNullOrEmpty(password))
            {
                alert.Alert("パスワードを入力してください", AlertType.warning);
            }
            else
            {
                loading.Value = false;
                var user = await authHandler.CallEmailSignIn(email, password);

                if (user == null)
                {
                    alert.Alert("ログインに失敗しました", AlertType.error);
                }
            }
        }

        private async void OnGoogleAuth(Unit unit)
        {
            loading.Value = true;
            
            var user = await authHandler.CallGoogleSignIn();

            if (user == null)
            {
                alert.Alert("ログインに失敗しました", AlertType.error);
            }
        }
    }
}