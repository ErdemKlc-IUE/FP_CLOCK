using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace FP_CLOCK
{
    public static class FirebaseHelper
    {
        public static void InitializeFirebase()
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("firebase-adminsdk.json")
            });
        }
    }
}
