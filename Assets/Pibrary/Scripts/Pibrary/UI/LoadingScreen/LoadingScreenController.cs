using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI.LoadingScreen
{
    public class LoadingScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject veil;
        [SerializeField] private GameObject circularProgress;
        [SerializeField] private Text text;

        [SerializeField] private float transition = 0.4f;

        public void SetEnable(bool enable)
        {
            SetEnable(enable, "");
        }
        
        public void SetEnable(String message)
        {
            SetEnable(true, message);
        }

        public void SetEnable(bool enable, string message)
        {
            veil.SetActive(enable);
            circularProgress.SetActive(enable);
            text.gameObject.SetActive(enable);
            
            text.text = message;
            text.DOFade(enable ? 1f : 0f, transition).SetEase(Ease.OutCubic);
        }
    }
}