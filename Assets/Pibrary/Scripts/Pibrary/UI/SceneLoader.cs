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

        public async void LoadScene(string sceneName)
        {
            DontDestroyOnLoad(this);
            animator.gameObject.SetActive(true);
            animator.enabled = true;
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            
            await UniTask.WaitUntilValueChanged(operation, x => x.isDone);
            
            if (!String.IsNullOrEmpty(closeAnimationName))
            {
                animator.Play(closeAnimationName);
            }
            
            Destroy(this);
        }
    }
}