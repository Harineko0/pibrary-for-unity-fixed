using System;
using Cysharp.Threading.Tasks;

using Firebase.Auth;
using Pibrary.Config;
using Pibrary.UI;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pibrary.Presenters
{
    public class TitlePresenter : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private SceneLoader sceneLoader;
        
        private FirebaseAuth auth;
        
        private void Start()
        {
            auth = FirebaseAuth.DefaultInstance;
            startButton
                .OnClickAsObservable()
                .Subscribe(OnClickStart)
                .AddTo(this);
        }

        private void OnClickStart(Unit _)
        {
            SceneConfig config = ConfigProvider.SceneConfig;
            bool isSignIn = auth.CurrentUser != null;
            sceneLoader.LoadScene(isSignIn ? config.homeScene : config.authScene);
        }
    }
}