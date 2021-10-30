using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Pibrary.Auth;
using Pibrary.Config;
using Pibrary.Data;
using Pibrary.Regex;
using Pibrary.UI;
using Pibrary.UI.Alert;
using Pibrary.UI.LoadingScreen;
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
        [SerializeField] private LoadingScreenController loadingScreen;

        [SerializeField] private AlertController alert;
        [SerializeField] private SceneLoader sceneLoader;

        private IAuthHandler authHandler;
        private IDataStore<SaveData> dataStore;
        private IDataHandler dataHandler;
        
        private void Start()
        {
            var pibrary = Pibrary.DefaultInstance;
            pibrary.Initialize();
            authHandler = pibrary.AuthHandler;
            dataStore = pibrary.DataStore;
            dataHandler = pibrary.DataHandler;
            
            googleButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(1000))
                .Subscribe(OnGoogleAuth)
                .AddTo(this);

            emailButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromMilliseconds(1000))
                .Subscribe(OnEmailAuth)
                .AddTo(this);
        }

        private void OnEmailAuth(Unit unit)
        {
            loadingScreen.SetEnable(true);
            
            string email = emailField.text;
            string password = passwordField.text;
                    
            if (String.IsNullOrEmpty(email))
            {
                alert.Alert("メールアドレスを入力してください", AlertType.warning);
                loadingScreen.SetEnable(false);
            }
            else if (!RegexUtilities.IsValidEmail(email))
            {
                alert.Alert("メールアドレスが無効です", AlertType.warning);
                loadingScreen.SetEnable(false);
            }
            else if (String.IsNullOrEmpty(password))
            {
                alert.Alert("パスワードを入力してください", AlertType.warning);
                loadingScreen.SetEnable(false);
            }
            else
            {
                var task = authHandler.CallEmailSignIn(email, password);
                OnAuthTask(task);
            }
        }

        private void OnGoogleAuth(Unit unit)
        {
            loadingScreen.SetEnable(true);
            
            var task = authHandler.CallGoogleSignIn();
            OnAuthTask(task);
        }

        private async void OnAuthTask(Task<FirebaseUser> task)
        {
            Debug.Log("認証中");
            loadingScreen.SetEnable("認証中");
            var user = await task;

            if (user == null)
            {
                alert.Alert("ログインに失敗しました", AlertType.error);
                loadingScreen.SetEnable(false);
                return;
            }

            loadingScreen.SetEnable("ユーザーデータを取得しています");
            await UniTask.WaitUntilValueChanged(dataHandler.UserData, x => x.Value);

            SaveData data = dataStore.SaveData.Value;
                
            if (data == null)
            {
                alert.Alert("ユーザーデータの取得に失敗しました", AlertType.error);
                loadingScreen.SetEnable(false);
            }
            else
            {
                alert.Alert("ようこそ、" + user.DisplayName + "さん", AlertType.success);
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                loadingScreen.SetEnable(false);
                sceneLoader.LoadScene(ConfigProvider.SceneConfig.homeScene);
            }
        }
    }
}