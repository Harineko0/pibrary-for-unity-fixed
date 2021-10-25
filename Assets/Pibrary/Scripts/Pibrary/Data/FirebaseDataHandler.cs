using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Firestore;
using Pibrary.Config;
using UniRx;
using UnityEngine;

namespace Pibrary.Data
{
    public class FirebaseDataHandler : IDataHandler
    {
        private Subject<LoadingState> stateSubject = new Subject<LoadingState>();
        public IObservable<LoadingState> OnStateChanged { get; }

        private ReactiveProperty<UserData> userData = new ReactiveProperty<UserData>();
        public IReadOnlyReactiveProperty<UserData> UserData { get { return userData; } }
        
        private FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        
        public async Task<UserData> FetchUserData(string uid)
        {
            DocumentReference reference = db.Collection("users").Document(uid);
            DocumentSnapshot snapshot = await reference.GetSnapshotAsync();
            Dictionary<string, object> dictionary = snapshot.ToDictionary();

            List<DocumentReference> accessibleAuthorRef = null;
            List<DocumentReference> purchasedContentsRef = null;
            if (dictionary.ContainsKey("accessibleAuthorRef"))
            {
                accessibleAuthorRef = (List<DocumentReference>) Convert.ChangeType(dictionary["accessibleAuthorRef"],
                    typeof(List<DocumentReference>));
            }

            if (dictionary.ContainsKey("purchasedContentsRef"))
            {
                purchasedContentsRef = (List<DocumentReference>) Convert.ChangeType(dictionary["purchasedContentsRef"],
                    typeof(List<DocumentReference>));
            }

            if (accessibleAuthorRef != null && purchasedContentsRef != null)
            {
                UserData data = new UserData
                {
                    accessibleAuthorRef = accessibleAuthorRef,
                    purchasedContentsRef = purchasedContentsRef,
                };
                userData.Value = data;
                return data;
            }

            Debug.LogError("Firestore returns illegal data!");
            return null;
        }
    }
}