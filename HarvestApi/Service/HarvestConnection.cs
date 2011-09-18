namespace HarvestApi.Service
{
    public class HarvestConnection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri">"https://example.harvestapp.com/</param>
        /// <param name="username">username.is@email.address</param>
        /// <param name="password">password</param>
        public HarvestConnection(string uri, string username, string password)
        {
            if (!uri.EndsWith("/")) uri += "/";
            Uri = uri;
            Username = username;
            Password = password;
        }

        public string Password { get; private set; } // = "password";
        public string Uri { get; private set; } // = "https://example.harvestapp.com/";
        public string Username { get; private set; } // = "username.is@email.address";
    }
}