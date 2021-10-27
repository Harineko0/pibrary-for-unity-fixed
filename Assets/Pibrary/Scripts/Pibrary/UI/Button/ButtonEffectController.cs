using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Pibrary.Input;
using UnityEngine.EventSystems;
using Zenject;

namespace Pibrary.UI.Button
{
    public class ButtonEffectController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Inject] private IInputProvider inputProvider;
        
        [SerializeField] private GameObject parent;
        [SerializeField] private GameObject clickEffect;
        private float transitionSpeed = 0.4f;
        private ColorType type = ColorType.primary;
        private RectTransform rect;
        
        public void SetParameter(float transitionSpeed, ColorType type)
        {
            this.transitionSpeed = transitionSpeed;
            this.type = type;
            if (type != ColorType.primary)
            {
                Start();   
            }
        }
        
        private void Start()
        {
            rect = parent.GetComponent<RectTransform>();
            var image = GetComponent<Image>();
            var button = GetComponent<UnityEngine.UI.Button>();
            var trigger = parent.AddComponent<ObservableEventTrigger>();
            
            var clickEffectRect = clickEffect.GetComponent<RectTransform>();
            var clickEffectCanvas = clickEffect.GetComponent<CanvasGroup>();
            
            var colorLoader = ColorLoader.Instance;
            var colors = colorLoader.ThemeMaterial.GetObjectParams(type);
            
            image.color = colors.main.color;
            
            trigger
                .OnPointerEnterAsObservable()
                .Subscribe(_ =>
                {
                    image.DOColor(colors.dark.color, transitionSpeed);
                })
                .AddTo(this);

            trigger
                .OnPointerExitAsObservable()
                .Subscribe(_ =>
                {
                    image.DOColor(colors.main.color, transitionSpeed);
                })
                .AddTo(this);

            button.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(transitionSpeed * 1.5f))
                .Subscribe(_ =>
                {
                    clickEffectRect.position = inputProvider.GetMousePosition();
                    
                    var sequence = DOTween.Sequence();
                    sequence.Append(clickEffectRect.DOScale(2f, transitionSpeed));
                    sequence.Append(clickEffectCanvas.DOFade(0f, transitionSpeed * 0.3f));
                    sequence.Append(clickEffectRect.DOScale(0f, 0));
                    sequence.Join(clickEffectCanvas.DOFade(1f, 0));
                    sequence.Play();
                }).AddTo(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            rect.DOScale(0.97f, transitionSpeed);
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            rect.DOScale(1f, transitionSpeed);
        }
    }
}