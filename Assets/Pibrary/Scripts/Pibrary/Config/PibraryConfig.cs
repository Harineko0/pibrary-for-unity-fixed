using System;
using UnityEngine;

namespace Pibrary.Config
{
    [CreateAssetMenu(fileName = "Data", menuName = "Pibrary/Scriptable/Create FirebaseAuthConfig")]
    [Serializable]
    public class PibraryConfig : ScriptableObject
    {
        [SerializeField] public OAuthConfig OAuthConfig = new OAuthConfig();
        [SerializeField] public ContentConfig ContentConfig = new ContentConfig();
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
}