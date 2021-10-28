using System;
using System.Threading.Tasks;
using Firebase.Auth;
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
        [SerializeField] private GameObject loadingScreen;

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

            loading.Subscribe(loading =>
            {
                loadingScreen.SetActive(loading);
            });
        }

        private void OnEmailAuth(Unit unit)
        {
            loading.Value = true;
            
            string email = emailField.text;
            string password = passwordField.text;
                    
            if (String.IsNullOrEmpty(email))
            {
                alert.Alert("メールアドレスを入力してください", AlertType.warning);
                loading.Value = false;
            }
            else if (!RegexUtilities.IsValidEmail(email))
            {
                alert.Alert("メールアドレスが無効です", AlertType.warning);
                loading.Value = false;
            }
            else if (String.IsNullOrEmpty(password))
            {
                alert.Alert("パスワードを入力してください", AlertType.warning);
                loading.Value = false;
            }
            else
            {
                var task = authHandler.CallEmailSignIn(email, password);
                OnAuthTask(task);
            }
        }

        private void OnGoogleAuth(Unit unit)
        {
            loading.Value = true;
            
            var task = authHandler.CallGoogleSignIn();
            OnAuthTask(task);
        }

        private async void OnAuthTask(Task<FirebaseUser> task)
        {
            var user = await task;

            if (user == null)
            {
                alert.Alert("ログインに失敗しました", AlertType.error);
                return;
            }
            
            alert.Alert("ようこそ、" + user.DisplayName + "さん", AlertType.success);

            loading.Value = false;
        }
    }
}