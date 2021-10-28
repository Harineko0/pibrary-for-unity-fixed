using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Pibrary.UI.Button
{
    [Serializable]
    public class CopyConfig
    {
        public bool copyWidth = true;
        public bool copyHeight = true;
        public bool square = false;
    }
    
    public class SizeController : MonoBehaviour
    {
        [SerializeField] private RectTransform copyFrom;
        [SerializeField] private CopyConfig copyConfig;

        // private bool isPlaying;
        
        private void Start()
        {
            // isPlaying = true;
            Resize();
        }

        public async void Resize()
        {
            // if (isPlaying)
            // {
            //     await UniTask.WaitUntilValueChanged(copyFrom, x => x.rect.width);
            // }
            await UniTask.DelayFrame(5);
            RectTransform rect = GetComponent<RectTransform>();
            float width = copyConfig.copyWidth ? copyFrom.rect.width
                : (copyConfig.square && copyConfig.copyHeight) ? copyFrom.rect.height : rect.rect.width;
            float height = copyConfig.copyHeight ? copyFrom.rect.height
                : (copyConfig.square && copyConfig.copyWidth) ? copyFrom.rect.width : rect.rect.height;
            rect.sizeDelta = new Vector2(width, height);
        }
    }
}