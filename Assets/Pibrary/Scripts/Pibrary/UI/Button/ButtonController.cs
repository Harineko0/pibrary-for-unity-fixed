using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI.Button
{
    public class ButtonController : MonoBehaviour
    {
        [SerializeField, Multiline(2)] private string text;
        [SerializeField] private float transitionSpeed = 0.7f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        [SerializeField] private ColorType colorType = ColorType.primary;
        
        [SerializeField] private ButtonEffectController buttoArea;
        [SerializeField] private Text buttonText;
        [SerializeField] private SizeController size;

        private void OnValidate()
        {
            if (buttonText != null)
            {
                buttonText.text = text;
            }
            
            if (size != null)
            {
                size.Resize();
            }
        }
        
        private void Awake()
        {
            buttoArea.SetParameter(transitionSpeed, colorType);
        }
    }
}