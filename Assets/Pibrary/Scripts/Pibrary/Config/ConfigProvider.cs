using Pibrary.UI;
using UnityEngine;

namespace Pibrary.Config
{
    public static class ConfigProvider
    {
        private static IConfigLoader configLoader = null;

        private static IConfigLoader Loader
        {
            get
            {
                if (configLoader != null)
                {
                    return configLoader;
                }
                else
                {
                    Debug.Log("Loading ConfigLoader");
                    GameObject gameObject = Resources.Load<GameObject>(Constant.getAssetPath("Prefabs/ConfigLoader")); 
                    GameObject instance = GameObject.Instantiate(gameObject);
                    configLoader = instance.GetComponent<IConfigLoader>();
                    return Loader;
                }
            }
        }

        public static OAuthConfig OAuthConfig
        {
            get { return Loader.Config.OAuthConfig; }
        }

        public static ContentConfig ContentConfig
        {
            get { return Loader.Config.ContentConfig; }
        }

        public static ThemeConfig ThemeConfig
        {
            get { return Loader.Config.ThemeConfig; }
        }

        public static SceneConfig SceneConfig
        {
            get { return Loader.Config.SceneConfig; }
        }
    }
}