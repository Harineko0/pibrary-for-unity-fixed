using Pibrary.Config;
using UnityEngine;

namespace Pibrary.UI
{
    public class ColorLoader : SingletonMonoBehaviour<ColorLoader>
    {
        [SerializeField] public ThemeParams<Material> ThemeMaterial;
        
        private void Start()
        {
            ThemeConfig config = ConfigProvider.ThemeConfig;

            ThemeMaterial.primary.main.color = config.primary.main;
            ThemeMaterial.primary.light.color = config.primary.light;
            ThemeMaterial.primary.dark.color = config.primary.dark;
            ThemeMaterial.primary.contrastText.color = config.primary.contrastText;
            
            ThemeMaterial.secondary.main.color = config.secondary.main;
            ThemeMaterial.secondary.light.color = config.secondary.light;
            ThemeMaterial.secondary.dark.color = config.secondary.dark;
            ThemeMaterial.secondary.contrastText.color = config.secondary.contrastText;

            ThemeMaterial.text.primary.color = config.text.primary;
            ThemeMaterial.text.secondary.color = config.text.secondary;
            ThemeMaterial.text.disabled.color = config.text.disabled;

            ThemeMaterial.background.Default.color = config.background.Default;
            ThemeMaterial.background.paper.color = config.background.paper;
        }
    }
}