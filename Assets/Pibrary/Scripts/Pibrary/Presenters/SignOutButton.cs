using System;
using Firebase.Auth;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.Presenters
{
    public class SignOutButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        private void Start()
        {
            var auth = FirebaseAuth.DefaultInstance;

            button.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Debug.Log("Sign out.");
                    auth.SignOut();
                }).AddTo(this);
        }
    }
}