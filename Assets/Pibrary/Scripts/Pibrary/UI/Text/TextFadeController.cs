using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI.Button
{
    public class TextFadeController : MonoBehaviour
    {
        [SerializeField] private Text text;
        
        [SerializeField] private float endValue = 1f;
        [SerializeField] private float duration = 0.4f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        
        private void OnEnable()
        {
            text.DOFade(endValue, duration).SetEase(ease);
        }
    }
}