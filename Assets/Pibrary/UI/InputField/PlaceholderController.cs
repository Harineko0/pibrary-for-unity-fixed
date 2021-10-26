using System;
using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

namespace Pibrary.UI.InputField
{
    public class PlaceholderController : MonoBehaviour
    {
        [SerializeField] private TMP_Text placeholder;
        [SerializeField] private Transform textArea;
        private TMP_InputField input;
        private GameObject permanentPlaceholder;
        
        private float transitionSpeed;
        private Ease ease;
        private float scale = 0.5f;
        private Vector3 moveTo;
        
        private void Start()
        {
            var controller = GetComponent<InputFieldController>();
            transitionSpeed = controller.TransitionSpeed;
            ease = controller.Ease;
            
            var rect = GetComponent<RectTransform>();
            moveTo = new Vector3(-rect.sizeDelta.x * 0.225f, placeholder.fontSize * 1.26f, 0f);
            
            input = GetComponent<TMP_InputField>();
            permanentPlaceholder = Instantiate(placeholder.gameObject);
            permanentPlaceholder.transform.SetParent(textArea);
            var instancedRect = permanentPlaceholder.GetComponent<RectTransform>();
            var originalRect = placeholder.GetComponent<RectTransform>();

            instancedRect.localPosition    = originalRect.localPosition;
            instancedRect.localRotation    = originalRect.localRotation;
            instancedRect.localScale       = originalRect.localScale;
            instancedRect.pivot            = originalRect.pivot;
            instancedRect.anchorMin        = originalRect.anchorMin;
            instancedRect.anchorMax        = originalRect.anchorMax;
            instancedRect.anchoredPosition = originalRect.anchoredPosition;
            instancedRect.sizeDelta        = originalRect.sizeDelta;

            placeholder.gameObject.SetActive(false);

            input
                .OnSelectAsObservable()
                .Subscribe(data => OnSelect())
                .AddTo(this);
            
            input
                .onEndEdit.AddListener(OnEndEdit);
        }

        void OnSelect()
        {
            var placeholderTransform = permanentPlaceholder.transform;
            placeholderTransform.DOScale(scale, transitionSpeed).SetEase(ease);
            placeholderTransform.DOLocalMove(moveTo, transitionSpeed).SetEase(ease);
        }

        void OnEndEdit(string text)
        {
            if (String.IsNullOrEmpty(input.text))
            {
                var placeholderTransform = permanentPlaceholder.transform;
                placeholderTransform.DOScale(1f, transitionSpeed).SetEase(ease);
                placeholderTransform.DOLocalMove(placeholder.transform.localPosition, transitionSpeed).SetEase(ease);
            }
        }
    }
}