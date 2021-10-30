using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pibrary.UI
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string animationName = "Expand";
        [SerializeField] private string closeAnimationName = "";
        [SerializeField] private double delay = 2000;

        public async void LoadScene(string sceneName)
        {
            DontDestroyOnLoad(this);
            animator.gameObject.SetActive(true);
            animator.enabled = true;
            await UniTask.Delay(TimeSpan.FromMilliseconds(delay));
            await SceneManager.LoadSceneAsync(sceneName);

            if (!String.IsNullOrEmpty(closeAnimationName))
            {
                animator.Play(closeAnimationName);
            }

            Destroy(this);
        }
    }
}