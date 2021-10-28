using System;
using UnityEngine;

namespace Pibrary.UI
{
    public class SizeAdjuster : MonoBehaviour
    {
        private void Start()
        {
            var rect = GetComponent<RectTransform>();
            float rate = Screen.height * 0.00092f; // divides 1080
            if (rate < 1)
            {
                rect.localScale = new Vector3(rate, rate, rate);
            }
        }
    }
}