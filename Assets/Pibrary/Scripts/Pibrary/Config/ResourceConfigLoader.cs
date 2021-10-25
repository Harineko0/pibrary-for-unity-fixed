using System;
using UniRx;
using UnityEngine;

namespace Pibrary.Config
{
    public class ResourceConfigLoader : SingletonMonoBehaviour<ResourceConfigLoader>, IConfigLoader
    {
        [SerializeField] private Environment environment = Environment.Development;
        private Subject<LoadingState> stateSubject = new Subject<LoadingState>();
        public IObservable<LoadingState> OnStateChanged 
        { 
            get { return stateSubject; }
        }

        private PibraryConfig config;
        public PibraryConfig Config
        {
            get { return config ?? (config = LoadConfig()); }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        private PibraryConfig LoadConfig()
        {
            switch (environment)
            {
                case Environment.Development:
                    Debug.Log("Load 'Development' conf");
                    return Resources.Load<PibraryConfig>(Constant.getAssetPath("Config/Development"));
                case Environment.Production:
                    return Resources.Load<PibraryConfig>(Constant.getAssetPath("Config/Production"));
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}