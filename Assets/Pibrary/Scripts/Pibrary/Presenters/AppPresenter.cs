using System;
using System.Collections.Generic;
using Pibrary.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.Presenters
{
    [Serializable]
    public class GameApp
    {
        public Button button;
        public string scene;
    }

    [Serializable]
    public class URIApp
    {
        public Button button;
        public string uri;
    }
    
    public class AppPresenter : MonoBehaviour
    {
        [SerializeField] private List<GameApp> gameApps;
        [SerializeField] private List<URIApp> uriApps;

        [SerializeField] private SceneLoader sceneLoader;

        private void Start()
        {
            foreach (var app in gameApps)
            {
                app.button.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        sceneLoader.LoadScene(app.scene);
                    }).AddTo(this);
            }

            foreach (var app in uriApps)
            {
                
                app.button.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        Application.OpenURL(app.uri);
                    }).AddTo(this);

            }
        }
    }
}