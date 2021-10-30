using System.Collections.Generic;
using Pibrary.Config;
using Pibrary.UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Pibrary.Presenters
{
    public class HomePresenter : MonoBehaviour
    {
        [SerializeField] private GameObject purchasedCanvas;
        [SerializeField] private GameObject notPurchasedCanvas;

        [SerializeField] private SceneLoader sceneLoader;
        [SerializeField] private List<Button> titleButton;
        
        private void Start()
        {
            var pibrary = Pibrary.DefaultInstance;
            var dataStore = pibrary.DataStore;
            dataStore.SaveData.Subscribe(saveData =>
            {
                Debug.Log(saveData);
                if (saveData != null && saveData.purchased)
                {
                    purchasedCanvas.SetActive(true);
                    notPurchasedCanvas.SetActive(false);
                }
                else
                {
                    purchasedCanvas.SetActive(false);
                    notPurchasedCanvas.SetActive(true);
                }
            }).AddTo(this);

            foreach (var button in titleButton)
            {
                button.OnClickAsObservable()
                    .Subscribe(_ =>
                    {
                        sceneLoader.LoadScene(ConfigProvider.SceneConfig.titleScene);
                    }).AddTo(this);
            }
        }
    }
}