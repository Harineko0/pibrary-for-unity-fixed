using System.Collections.Generic;
using Firebase.Firestore;

namespace Pibrary.Data
{
    [FirestoreData]
    public class UserData
    {
        [FirestoreProperty] public List<DocumentReference> accessibleAuthorRef { get; set; }
        [FirestoreProperty] public List<DocumentReference> purchasedContentsRef { get; set; }
    }
}