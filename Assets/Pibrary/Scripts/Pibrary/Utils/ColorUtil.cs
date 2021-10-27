using UnityEngine;

namespace Pibrary.Utils
{
    public class ColorUtil
    {
        public static Color GetColor(string colorCode)
        {
            Color color = default(Color);
            if (ColorUtility.TryParseHtmlString(colorCode, out color))
            {
                return color;
            }
            else
            { 
                return Color.white;
            }
        }
    }
}