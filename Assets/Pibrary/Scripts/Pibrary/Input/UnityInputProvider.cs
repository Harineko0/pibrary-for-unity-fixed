using UnityEngine;

namespace Pibrary.Input
{
    public class UnityInputProvider : IInputProvider
    {
        public Vector3 GetMousePosition()
        {
            return UnityEngine.Input.mousePosition;
        }
    }
}