using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Pibrary.Config;

namespace Pibrary.UI.Alert
{
    public enum AlertType
    {
        error,
        warning,
        success
    }
    
    public class AlertController : MonoBehaviour
    {
        [SerializeField] private Text text;
        private RectTransform rect;
        
        [SerializeField] private float speed = 0.4f;
        [SerializeField] private float wait = 3f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        [SerializeField] private float defaultPosY = 30f;
        [SerializeField] private float movePosY = -50f;
        
        private void Start()
        {
            rect = GetComponent<RectTransform>();
        }

        public void Alert(string text, AlertType type)
        {
            this.text.text = text;
            this.text.color = ConfigProvider.ThemeConfig.GetAlertColor(type).main;
            
            var sequence = DOTween.Sequence();
            sequence.Append(rect.DOLocalMoveY(movePosY, speed).SetEase(ease));
            sequence.AppendInterval(wait);
            sequence.Append(rect.DOLocalMoveY(defaultPosY, speed).SetEase(ease));
            sequence.Play();
        }
    }
}