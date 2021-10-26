using DG.Tweening;
using UnityEngine;

namespace Pibrary.UI.InputField
{
    public class InputFieldController : MonoBehaviour
    {
        [SerializeField] public float TransitionSpeed = 0.5f;
        [SerializeField] public Ease Ease = Ease.OutCubic;
    }
}