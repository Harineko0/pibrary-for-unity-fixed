using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.UI
{
    public class ShapeColorSetter : MonoBehaviour, IColorSetter
    {
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void SetColor(Material material)
        {
            image.color = material.color;
        }
    }
}