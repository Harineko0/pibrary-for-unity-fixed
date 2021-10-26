using UnityEngine;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class InputFieldController : MonoBehaviour
{
    [SerializeField] private Image normalBar;
    [SerializeField] private Image highlightedBar;
    [SerializeField] private Image selectedBar;
    private RectTransform selectedTransform;
    [SerializeField] private Image selectedHighlightedBar;

    [SerializeField] private float transitionSpeed = 0.5f;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private bool isSelected;
    
    void Start()
    {
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
