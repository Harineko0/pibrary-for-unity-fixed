using System;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI.Copyright
{
    public class CopyrightController : MonoBehaviour
    {
        private void Start()
        {
            Text text = GetComponent<Text>();
            text.text = "Copyright © Pibrary " + DateTime.Now.Year + ".";
        }
    }
}