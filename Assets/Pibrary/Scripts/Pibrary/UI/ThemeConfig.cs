using System;
using Pibrary.Utils;
using UnityEngine;

namespace Pibrary.UI
{
    [Serializable]
    public class ThemeConfig
    {
        public ObjectColor primary = new ObjectColor
        {
            main = ColorUtil.GetColor("#1976d2"),
            light = ColorUtil.GetColor("#42a5f5"),
            dark = ColorUtil.GetColor("#1565c0"),
            contrastText = ColorUtil.GetColor("#fff"),
        };
        public ObjectColor secondary = new ObjectColor
        {
            main = ColorUtil.GetColor("#81d4fa"),
            light = ColorUtil.GetColor("#b6ffff"),
            dark = ColorUtil.GetColor("#4ba3c7"),
            contrastText = ColorUtil.GetColor("#fff"),
        };
        public TextColor text;
        public BackgroundColor background;
    }

    [Serializable]
    public class ObjectColor
    {
        public Color main;
        public Color light;
        public Color dark;
        public Color contrastText;
    }

    [Serializable]
    public class TextColor
    {
        public Color primary = new Color(0f, 0f, 0f, 0.87f);
        public Color secondary = new Color(0f, 0f, 0f, 0.6f);
        public Color disabled = new Color(0f, 0f, 0f, 0.12f);
    }

    [Serializable]
    public class BackgroundColor
    {
        public Color paper = Color.white;
        public Color Default = Color.white;
    }
}