using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Pibrary.UI
{
    public class ColorController : MonoBehaviour
    {
        private ReactiveProperty<ColorType> colorType
            = new ReactiveProperty<ColorType>(UI.ColorType.primary);
        IReadOnlyReactiveProperty<ColorType> ColorType { get { return colorType; }}

        [SerializeField] private PartParams<List<GameObject>> setters;

        private ColorLoader loader;
        
        private void Start()
        {
            loader = ColorLoader.Instance;
            ColorType.Subscribe(OnChangeColorType);
        }

        private void OnChangeColorType(ColorType type)
        {
            foreach (var setter in setters.Object.main)
            {
                setter.GetComponent<IColorSetter>().SetColor(loader.ThemeMaterial.GetObjectParams(type).main);
            }

            foreach (var setter in setters.Object.dark)
            {
                setter.GetComponent<IColorSetter>().SetColor(loader.ThemeMaterial.GetObjectParams(type).dark);
            }

            foreach (var setter in setters.Object.light)
            {
                setter.GetComponent<IColorSetter>().SetColor(loader.ThemeMaterial.GetObjectParams(type).light);
            }
                
            foreach (var setter in setters.Object.contrastText)
            {
                setter.GetComponent<IColorSetter>().SetColor(loader.ThemeMaterial.GetObjectParams(type).contrastText);
            }
        }

        public void SetObjectColor(ColorType type)
        {
            this.colorType.Value = type;
        }
    }
}