using System;
using System.Collections.Generic;
using Pibrary.PiMaterial;
using UniRx;
using UnityEngine;
using Zenject;

namespace Pibrary.UI
{
    public class ColorController : MonoBehaviour
    {
        [SerializeField] private bool enableOverrideColor;
        public bool EnableOverrideColor => enableOverrideColor;
        [SerializeField] private ObjectColor overrideColor;
        public ObjectColor OverrideColor => overrideColor;
        [SerializeField] private PartParams<List<GameObject>> setters;

        private ReactiveProperty<ColorType> colorType
            = new ReactiveProperty<ColorType>(UI.ColorType.primary);
        IReadOnlyReactiveProperty<ColorType> ColorType { get { return colorType; }}

        private ColorLoader loader;
        
        private void Start()
        {
            loader = ColorLoader.Instance;
            ColorType.Subscribe(OnChangeColorType);
        }

        private void OnValidate()
        {
            OnChangeColorType(ColorType.Value);
        }

        private void OnChangeColorType(ColorType type)
        {
            ObjectParams<Material> mParams;

            if (enableOverrideColor)
            {
                mParams = ThemeParamFactory.createObjectParamsMaterial(overrideColor);
            }
            else
            {
                mParams = loader.ThemeMaterial.GetObjectParams(type);   
            }
            
            foreach (var setter in setters.Object.main)
            {
                setter.GetComponent<IColorSetter>().SetColor(mParams.main);
            }

            foreach (var setter in setters.Object.dark)
            {
                setter.GetComponent<IColorSetter>().SetColor(mParams.dark);
            }

            foreach (var setter in setters.Object.light)
            {
                setter.GetComponent<IColorSetter>().SetColor(mParams.light);
            }
                
            foreach (var setter in setters.Object.contrastText)
            {
                setter.GetComponent<IColorSetter>().SetColor(mParams.contrastText);
            }
        }

        public void SetObjectColor(ColorType type)
        {
            this.colorType.Value = type;
        }
    }
}