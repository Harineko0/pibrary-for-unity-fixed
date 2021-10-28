using System;
using UnityEngine;
using Object = System.Object;

namespace Pibrary.UI
{
    [Serializable]
    public class ThemeParams<T>
    {
        public ObjectParams<T> primary;
        public ObjectParams<T> secondary;
        public TextParams<T> text;
        public BackgroundParams<T> background;

        public ObjectParams<T> GetObjectParams(ColorType type)
        {
            if (type == ColorType.primary)
            {
                return primary;
            }

            if (type == ColorType.secondary)
            {
                return secondary;
            }
            
            return primary;
        }
    }
    
    [Serializable]
    public class PartParams<T>
    {
        public ObjectParams<T> Object;
        public TextParams<T> text;
        public BackgroundParams<T> background;
    }
    
    [Serializable]
    public class ObjectParams<T>
    {
        public T main;
        public T light;
        public T dark;
        public T contrastText;
    }
    
    [Serializable]
    public class TextParams<T>
    {
        public T primary;
        public T secondary;
        public T disabled;
    }

    [Serializable]
    public class BackgroundParams<T>
    {
        public T paper;
        public T Default;
    }

    public class ThemeParamFactory
    {
        public static Material createUIMaterial()
        {
            return new Material(Shader.Find("UI/Default"));
        }
        
        public static ObjectParams<Material> createObjectParamsMaterial(
            Color main, Color light, Color dark, Color contrastText)
        {
            var param = new ObjectParams<Material>
            {
                main = createUIMaterial(),
                light = createUIMaterial(),
                dark = createUIMaterial(),
                contrastText = createUIMaterial(),
            };
            param.main.color = main;
            param.light.color = light;
            param.dark.color = dark;
            param.contrastText.color = contrastText;

            return param;
        }

        public static ObjectParams<Material> createObjectParamsMaterial(ObjectColor color)
        {
            return createObjectParamsMaterial(color.main, color.light, color.dark, color.contrastText);
        }

        public static ObjectColor convertObjectColor(ObjectParams<Material> param)
        {
            return new ObjectColor
            {
                main = param.main.color,
                light = param.light.color,
                dark = param.dark.color,
                contrastText = param.contrastText.color,
            };
        }
    }
}