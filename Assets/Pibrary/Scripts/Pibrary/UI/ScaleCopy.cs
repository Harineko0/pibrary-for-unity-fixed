using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Pibrary.UI
{
    public class ScaleCopy : MonoBehaviour
    {
        [SerializeField] private RectTransform copyFrom;

        private async void Start()
        {
            await UniTask.WaitUntilValueChanged(copyFrom, x => x.localScale);
            RectTransform rect = GetComponent<RectTransform>();
            rect.localScale = copyFrom.localScale;
        }
    }
}