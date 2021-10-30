using System;
using UnityEngine;

namespace Pibrary
{
    public class Initializer : MonoBehaviour
    {
        private void Awake()
        {
            Pibrary.DefaultInstance.Initialize();
        }
    }
}