using System;
using Pibrary.UI.Alert;
using Pibrary.Utils;
using UnityEngine;
using Object = System.Object;

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
        public ObjectColor error = new ObjectColor
        {
            main = ColorUtil.GetColor("#f44336"),
            light = ColorUtil.GetColor("#e57373"),
            dark = ColorUtil.GetColor("#d32f2f"),
            contrastText = ColorUtil.GetColor("#fff"),
        };
        public ObjectColor warning = new ObjectColor
        {
            main = ColorUtil.GetColor("#ffa726"),
            light = ColorUtil.GetColor("#ffb74d"),
            dark = ColorUtil.GetColor("#f57c00"),
            contrastText = ColorUtil.GetColor("#fff"),
        };
        public ObjectColor success = new ObjectColor
        {
            main = ColorUtil.GetColor("#66bb6a"),
            light = ColorUtil.GetColor("#81c784"),
            dark = ColorUtil.GetColor("#388e3c"),
            contrastText = ColorUtil.GetColor("#fff"),
        };
        public TextColor text;
        public BackgroundColor background;

        public ObjectColor GetAlertColor(AlertType type)
        {
            switch (type)
            {
                case AlertType.error:
                    return error;
                case AlertType.warning:
                    return warning;
                case AlertType.success:
                    return success;
            }

            return primary;
        }
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