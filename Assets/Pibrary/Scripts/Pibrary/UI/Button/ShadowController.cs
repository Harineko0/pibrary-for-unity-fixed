using System;
using UnityEngine;

namespace Pibrary.UI.Button
{
    public class ShadowController : MonoBehaviour
    {
        [SerializeField] private RectTransform copyFrom;
        private void Start()
        {
            Debug.Log(copyFrom.rect.width);
            Debug.Log(copyFrom.rect.height);
            RectTransform rect = GetComponent<RectTransform>();    

            rect.sizeDelta = new Vector2(copyFrom.rect.width, copyFrom.rect.height);
            // rect.offsetMin = new Vector2(0, -6f);
            // rect.offsetMax = new Vector2(0, 0);
        }

        private void Update()
        {
            Debug.Log(copyFrom.rect);
        }
    }
}