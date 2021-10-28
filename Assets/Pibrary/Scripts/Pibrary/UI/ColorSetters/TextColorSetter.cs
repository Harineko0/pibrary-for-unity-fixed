using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI
{
    public class TextColorSetter : MonoBehaviour, IColorSetter
    {
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        public void SetColor(Material material)
        {
            Debug.Log(material.color);
            text.color = material.color;
        }
    }
}