using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI.Veil
{
    public class VeilController : MonoBehaviour
    {
        [SerializeField] private float alpha = 0.2f;
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        
        private Image image;
        private void OnEnable()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }

            image.DOFade(alpha, duration).SetEase(ease);
        }

        private void OnDisable()
        {
            image.DOFade(0, duration).SetEase(ease);
        }
    }
}