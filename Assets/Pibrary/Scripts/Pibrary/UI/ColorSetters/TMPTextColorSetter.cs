using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI
{
    public class TMPTextColorSetter : MonoBehaviour, IColorSetter
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        public void SetColor(Material material)
        {
            text.color = material.color;
        }
    }
}