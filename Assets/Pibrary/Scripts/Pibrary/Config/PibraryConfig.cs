using System;
using Pibrary.UI;
using UnityEngine;

namespace Pibrary.Config
{
    [CreateAssetMenu(fileName = "Data", menuName = "Pibrary/Scriptable/Create FirebaseAuthConfig")]
    [Serializable]
    public class PibraryConfig : ScriptableObject
    {
        [SerializeField] public OAuthConfig OAuthConfig = new OAuthConfig();
        [SerializeField] public ContentConfig ContentConfig = new ContentConfig();
        [SerializeField] public ThemeConfig ThemeConfig = new ThemeConfig();
        [SerializeField] public SceneConfig SceneConfig = new SceneConfig();
    }
    
    [Serializable]
    public class OAuthConfig
    {
        public string cliendID = "";
        public string clientSecret = "";
    }

    [Serializable]
    public class ContentConfig
    {
        public string ContentID = "";
    }

    [Serializable]
    public class SceneConfig
    {
        public string titleScene = "PibraryTitleScene";
        public string authScene = "PibraryAuthScene";
        public string homeScene = "PibraryHomeScene";
    }
}