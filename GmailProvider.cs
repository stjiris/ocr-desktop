using Google.Apis.Gmail.v1;
using GData = Google.Apis.Gmail.v1.Data;
using Google.Apis.Auth.OAuth2;
using System.Text;


namespace Tesseract_UI_Tools
{
    public static class GmailProvider
    {
        private static readonly string ClientID = "194488678419-drtq9t4ki2v55ba7uelle38kp4rk4j4t.apps.googleusercontent.com";
        private static readonly string EncriptedClientSecret = "R09DU1BYLXd1dU1ORlFCczlVM1NWQW1kcUVDbEZyNW1JRVk=";
        private static readonly string ClientSecret = Encoding.UTF8.GetString(Convert.FromBase64String(EncriptedClientSecret));
        private static readonly string[] Scopes = { GmailService.Scope.GmailSend };

        private static UserCredential? credential = null;
        private static GmailService? service = null;

        public delegate void LoginStatusUpdateHandler();
        public static event LoginStatusUpdateHandler? LoginStatusUpdate; 

        public static bool IsReady() => service != null;
        public static async void Initialize()
        {
            if (service != null) return;
            ClientSecrets secret = new()
            {
                ClientId = ClientID,
                ClientSecret = ClientSecret
            };
            try
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(secret, Scopes, "user", CancellationToken.None);
                if (credential == null) return;
                await credential.RefreshTokenAsync(CancellationToken.None);
                service = new GmailService(new Google.Apis.Services.BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Tesseract UI Tools"
                });
                LoginStatusUpdate?.Invoke();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                System.Diagnostics.Debug.WriteLine(credential);
                System.Diagnostics.Debug.WriteLine(service);
            }
            
        }

        public static void SendMail(string to, string subject, string htmlBody)
        {
            if (service == null) return;

            string mailString = $"To: {to}\r\nSubject: {subject}\r\nContent-Type: text/html;charset=utf-8\r\n\r\n{htmlBody}";
            string mailRaw = Convert.ToBase64String(Encoding.UTF8.GetBytes(mailString)).Replace('+', '-').Replace('/', '_').Replace("=", "");

            GData.Message msg = new()
            {
                Raw = mailRaw
            };

            service.Users.Messages.Send(msg, "me").Execute();
        }

        public static async void LogOut()
        {
            if ( credential == null ) return;
            await credential.RevokeTokenAsync(CancellationToken.None);
            credential = null;
            service = null;
            LoginStatusUpdate?.Invoke();
        }
    }
}
