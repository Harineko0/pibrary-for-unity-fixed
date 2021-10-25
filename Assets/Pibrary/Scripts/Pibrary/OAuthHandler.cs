using Pibrary.Config;
using UnityEngine;

namespace Pibrary
{
    public class OAuthHandler : MonoBehaviour
    {
        private void Start()
        {
            Pibrary.DefaultInstance.Initialize();
        }

        // Start is called before the first frame update
        public void OnClick()
        {
            var cliendID = ConfigProvider.OAuthConfig.cliendID;
            Debug.Log(cliendID);
        }
    }
}
