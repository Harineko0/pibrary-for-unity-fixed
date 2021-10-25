using System;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Pibrary.Config
{
    public enum Environment
    {
        Development,
        Production,
    }
    
    public class AddressableConfigLoader : SingletonMonoBehaviour<AddressableConfigLoader>, IConfigLoader
    {
        [SerializeField]
        private Environment environment = Environment.Development;
        private PibraryConfig config;
        private Subject<LoadingState> stateSubject = new Subject<LoadingState>();
        public IObservable<LoadingState> OnStateChanged
        {
            get { return stateSubject; }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            LoadConfig();
            stateSubject.OnNext(LoadingState.WaitingToLoad);
        }

        /// <summary>
        /// Conf値
        /// </summary>
        public PibraryConfig Config
        {
            //configがnullならロードしてキャッシュする
            get
            {
                if (config == null)
                {
                    Debug.Log("PibraryConfig is not loaded yet.");
                    LoadConfig();
                }
                return config;
            }
        }

        /// <summary>
        /// 環境別設定値読み込み
        /// </summary>
        /// <returns></returns>
        private async void LoadConfig()
        {
            stateSubject.OnNext(LoadingState.Loading);
            AsyncOperationHandle<PibraryConfig> op;
            // 愚直にswitchで
            // 他にもっといい方法あるかも
            switch (environment)
            {
                case Environment.Development:
                    op = Addressables.LoadAssetAsync<PibraryConfig>(Constant.getAssetPath("DevelopmentConfig"));
                    break;
                case Environment.Production:
                    op = Addressables.LoadAssetAsync<PibraryConfig>(Constant.getAssetPath("ProductionConfig"));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            PibraryConfig result = await op.Task;
            this.config = result;
            Addressables.Release(op);
            stateSubject.OnNext(LoadingState.Completed);
        }
    }
}