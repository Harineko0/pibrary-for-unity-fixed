using System;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using Pibrary.Auth;
using Pibrary.Config;
using Pibrary.Data;
using UnityEngine;
using UniRx;

namespace Pibrary
{
    public class Pibrary
    {
        #region Singleton
        
        private Pibrary() {}
        private static Pibrary instance = new Pibrary();
        public static Pibrary DefaultInstance { get => instance; }
        
        #endregion

        private IAuthHandler authHandler;
        public IAuthHandler AuthHandler { get { return authHandler; } }

        private IDataStore<SaveData> dataStore;
        public IDataStore<SaveData> DataStore { get { return dataStore; } }

        private IDataHandler dataHandler;
        public IDataHandler DataHandler { get { return dataHandler; } }
        
        private FirebaseAuth auth;
        private FirebaseUser user;
         
        public void Initialize()
        {
            authHandler = new FirebaseAuthHandler();
            dataStore = new SerialDataStore<SaveData>();
            dataHandler = new FirebaseDataHandler();
            auth = FirebaseAuth.DefaultInstance;
            
            auth.StateChanged += FetchUserData;
            FetchUserData(this, null);
            DataHandler.UserData.Where(data => data != null).Subscribe(SaveSaveData);
            
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                // FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                var dependencyStatus = task.Result;
                if (dependencyStatus != Firebase.DependencyStatus.Available) {
                    Debug.LogError(System.String.Format(
                        "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                }
            });

            # region Debug
            DataStore.SaveData.Subscribe(data =>
            {
                Debug.Log("SaveData.purchased: " + data.purchased);
            });
            DataHandler.FetchUserData("XxZgf3Ls3jP06oROfqfJcQdrnZ33");
            # endregion
        }
        
        void FetchUserData(object sender, System.EventArgs eventArgs) {
            if (auth.CurrentUser != user) {
                bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
                if (!signedIn && user != null) {
                    Debug.Log("Signed out " + user.UserId);
                }
                user = auth.CurrentUser;
                if (signedIn) {
                    Debug.Log("Signed in " + user.UserId);
                }

                if (user != null)
                {
                    DataHandler.FetchUserData(user.UserId);
                }
            }
        }

        void SaveSaveData(UserData data)
        {
            String contentId = ConfigProvider.ContentConfig.ContentID;
            bool purchased = false;
            foreach (DocumentReference docRef in data.purchasedContentsRef)
            {
                if (contentId == docRef.Id)
                {
                    purchased = true;
                    break;
                }
            }

            SaveData saveData = new SaveData
            {
                purchased = purchased,
            };
            Debug.Log("Purchased: " + purchased);
            DataStore.Save(saveData);
        }
    }
}