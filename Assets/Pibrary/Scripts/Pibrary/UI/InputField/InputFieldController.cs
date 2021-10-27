using System;
using DG.Tweening;
using UnityEngine;

namespace Pibrary.UI.InputField
{
    public class InputFieldController : MonoBehaviour
    {
        [SerializeField] private float transitionSpeed = 0.5f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        [SerializeField] private ColorType colorType = ColorType.primary;

        private void Start()
        {
            var bar = GetComponent<BarController>();
            var placeholder = GetComponent<PlaceholderController>();
            var colorController = GetComponent<ColorController>();
            
            bar.SetParameter(transitionSpeed, ease);
            placeholder.SetParameter(transitionSpeed, ease);
            colorController.SetObjectColor(colorType);
        }

        private void OnValidate()
        {
            var colorController = GetComponent<ColorController>();
            colorController.SetObjectColor(colorType);
        }
    }
}