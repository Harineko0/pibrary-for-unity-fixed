using System;
using Cysharp.Threading.Tasks;
using Firebase.Auth;
using Pibrary.Config;
using Pibrary.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Pibrary.Presenters
{
    public class TitlePresenter : MonoBehaviour
    {
        [SerializeField] private GameObject title;
        [SerializeField] private Button startButton;
        [SerializeField] private SceneLoader sceneLoader;

        [SerializeField] private double titleDelay;
        [SerializeField] private double titleAnimationTime;

        private FirebaseAuth auth;
        
        private void Start()
        {
            auth = FirebaseAuth.DefaultInstance;
            startButton
                .OnClickAsObservable()
                .Subscribe(OnClickStart)
                .AddTo(this);
            
            PlayTitle();
        }

        private void OnClickStart(Unit _)
        {
            SceneConfig config = ConfigProvider.SceneConfig;
            bool isSignIn = auth.CurrentUser != null;
            sceneLoader.LoadScene(isSignIn ? config.homeScene : config.authScene);
        }

        private async void PlayTitle()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(titleDelay));
            title.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(titleAnimationTime));
            startButton.transform.parent.gameObject.SetActive(true);
        }
    }
}