using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI.CircularProgress
{
    public class CircularProgressController : MonoBehaviour
    {
        [SerializeField] private ColorType type = ColorType.primary;
        [SerializeField] private float speed = 1.5f;
        private Image image;

        private void Start()
        {
            image = GetComponent<Image>();

            var loader = ColorLoader.Instance;
            image.material = loader.ThemeMaterial.GetObjectParams(type).main;
            
            var sequence = DOTween.Sequence();
            sequence.Append(image.DOFillAmount(1f, speed))
                .AppendCallback(() => image.fillClockwise = true)
                .Append(image.DOFillAmount(0f, speed))
                .AppendCallback(() => image.fillClockwise = false)
                .SetLoops(-1, LoopType.Restart);
            sequence.Play();
        }
    }
}