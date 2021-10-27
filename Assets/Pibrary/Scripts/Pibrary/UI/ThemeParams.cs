using System;
using UnityEngine;

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
}