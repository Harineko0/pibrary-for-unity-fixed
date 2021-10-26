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
        public IObservable<LoadingState> OnStateChanged { get { return stateSubject; }}

        private ReactiveProperty<UserData> userData = new ReactiveProperty<UserData>();
        public IReadOnlyReactiveProperty<UserData> UserData { get { return userData; } }
        
        private FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        public async void FetchUserData(string uid)
        {
            DocumentReference reference = db.Collection("users").Document(uid);
            DocumentSnapshot snapshot = await reference.GetSnapshotAsync();
            
            UserData data = snapshot.ConvertTo<UserData>();
            userData.Value = data;
        }
    }
}