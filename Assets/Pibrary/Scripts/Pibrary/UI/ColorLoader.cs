using System;
using Pibrary.Config;
using UnityEngine;

namespace Pibrary.UI
{
    public class ColorLoader : MonoBehaviour
    {
        [SerializeField] private ThemeMaterial material;
        
        private void Start()
        {
            ThemeConfig config = ConfigProvider.ThemeConfig;

            material.primary.main.color = config.primary.main;
            material.primary.light.color = config.primary.light;
            material.primary.dark.color = config.primary.dark;
            material.primary.contrastText.color = config.primary.contrastText;
            
            material.secondary.main.color = config.secondary.main;
            material.secondary.light.color = config.secondary.light;
            material.secondary.dark.color = config.secondary.dark;
            material.secondary.contrastText.color = config.secondary.contrastText;

            material.text.primary.color = config.text.primary;
            material.text.secondary.color = config.text.secondary;
            material.text.disabled.color = config.text.disabled;

            material.background.Default.color = config.background.Default;
            material.background.paper.color = config.background.paper;
        }
    }

    [Serializable]
    public class ThemeMaterial
    {
        public ObjectMaterial primary;
        public ObjectMaterial secondary;
        public TextMaterial text;
        public BackgroundMaterial background;
    }

    [Serializable]
    public class ObjectMaterial
    {
        public Material main;
        public Material light;
        public Material dark;
        public Material contrastText;
    }
    
    [Serializable]
    public class TextMaterial
    {
        public Material primary;
        public Material secondary;
        public Material disabled;
    }

    [Serializable]
    public class BackgroundMaterial
    {
        public Material paper;
        public Material Default;
    }
}