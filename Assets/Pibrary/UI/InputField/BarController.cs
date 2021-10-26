using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using DG.Tweening;

namespace Pibrary.UI.InputField
{
    public class BarController : MonoBehaviour
    {
        [SerializeField] private Image normalBar;
        [SerializeField] private Image highlightedBar;
        [SerializeField] private Image selectedBar;
        private RectTransform selectedTransform;
        [SerializeField] private Image selectedHighlightedBar;

        private float transitionSpeed;
        private Ease ease;
        
        private bool isSelected;

        void Start()
        {
            var controller = GetComponent<InputFieldController>();
            transitionSpeed = controller.TransitionSpeed;
            ease = controller.Ease;
            
            var input = GetComponent<TMP_InputField>();
            var trigger = gameObject.AddComponent<ObservableEventTrigger>();
            selectedTransform = selectedBar.gameObject.GetComponent<RectTransform>();

            trigger
                .OnPointerEnterAsObservable()
                .Subscribe(data => OnPointerEnter())
                .AddTo(this);

            trigger
                .OnPointerExitAsObservable()
                .Subscribe(data => OnPointerExit())
                .AddTo(this);

            input
                .OnSelectAsObservable()
                .Subscribe(data => OnSelect())
                .AddTo(this);

            input
                .onEndEdit.AddListener(OnEndEdit);
        }

        void OnPointerEnter()
        {
            if (!isSelected)
            {
                normalBar.DOFade(endValue: 0f, duration: transitionSpeed).SetEase(ease);
                highlightedBar.DOFade(endValue: 1f, duration: transitionSpeed).SetEase(ease);
            }
            else
            {
                selectedHighlightedBar.DOFade(endValue: 1f, duration: transitionSpeed).SetEase(ease);
            }
        }

        void OnPointerExit()
        {
            if (!isSelected)
            {
                normalBar.DOFade(endValue: 1f, duration: transitionSpeed).SetEase(ease);
                highlightedBar.DOFade(endValue: 0f, duration: transitionSpeed).SetEase(ease);
            }
            else
            {
                selectedHighlightedBar.DOFade(endValue: 0f, duration: transitionSpeed).SetEase(ease);
            }
        }

        void OnSelect()
        {
            isSelected = true;
            selectedTransform.DOScale(new Vector3(1f, 1f, 1f), transitionSpeed).SetEase(ease);
            selectedHighlightedBar.DOFade(endValue: 1f, duration: transitionSpeed).SetEase(ease);
        }

        void OnEndEdit(string text)
        {
            isSelected = false;
            selectedTransform.DOScale(new Vector3(0f, 1f, 1f), transitionSpeed).SetEase(ease);
            selectedHighlightedBar.DOFade(endValue: 0f, duration: transitionSpeed).SetEase(ease);
        }
    }

}