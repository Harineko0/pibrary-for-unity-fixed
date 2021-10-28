using UnityEngine;

namespace Pibrary.PiMaterial
{
    public class UnityMaterialManager : IMaterialManager
    {
        public Material createUIMaterial()
        {
            return new Material(Shader.Find("UI/Default"));
        }
    }
}