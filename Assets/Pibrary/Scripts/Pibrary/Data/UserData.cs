using System.Collections.Generic;
using Firebase.Firestore;

namespace Pibrary.Data
{
    public class UserData
    {
        public List<DocumentReference> accessibleAuthorRef;
        public List<DocumentReference> purchasedContentsRef;
    }
}