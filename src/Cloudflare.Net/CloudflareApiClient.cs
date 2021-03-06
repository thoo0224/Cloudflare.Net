using Cloudflare.Net.Endpoints;
using Cloudflare.Net.Enums;
using Cloudflare.Net.Exceptions;
using Cloudflare.Net.Objects.User;
using Cloudflare.Net.Utils;

using RestSharp;

using System;
using System.Threading.Tasks;

namespace Cloudflare.Net
{
    /// <summary>
    /// Api client
    /// </summary>
    public class CloudflareApiClient
    {

        internal const string DefaultBaseUrl = "https://api.cloudflare.com/client/v4/";
        internal RestClient RestClient { get; set; }
        internal bool LoggingIn { get; set; }

        /// <summary>
        /// The current logged in user
        /// </summary>
        public CloudflareUser CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        private UserEndpoints _user;
        /// <summary>
        /// All the user endpoints
        /// </summary>
        public UserEndpoints User
        {
            get
            {
                Verify();
                return _user;
            }
            set => _user = value;
        }

        private ZoneEndpoints _zone;
        /// <summary>
        /// All the zone endpoints
        /// </summary>
        public ZoneEndpoints Zone
        {
            get
            {
                Verify();
                return _zone;
            }
            set => _zone = value;
        }

        private DnsEndpoints _dns;
        /// <summary>
        /// All the dns endpoints
        /// </summary>
        public DnsEndpoints Dns
        {
            get
            {
                Verify();
                return _dns;
            }
            set => _dns = value;
        }

        [Obsolete("Please use CloudflareApiClientBuilder#create to create an api client. (This constructor will be removed within the next few updates.)")]
        public CloudflareApiClient(Action<CloudflareApiClientOptions> optionsAction)
        { }

        internal CloudflareApiClient(
            string apiToken,
            string apiKey, 
            string email, 
            Action<RestClient> clientAction,
            AuthenticationType authType)
        {
            RestClient = new RestClient();
            clientAction?.Invoke(RestClient);

            RestClient.BaseUrl = new Uri(DefaultBaseUrl);
            RestClient.UseSerializer<NewtonsoftSerializer>();

            if (authType == AuthenticationType.ApiKey)
            {
                Checks.NotNull(apiKey, nameof(apiKey));
                Checks.NotNull(email, nameof(email));

                RestClient.AddDefaultHeader("X-Auth-Email", email);
                RestClient.AddDefaultHeader("X-Auth-Key", apiKey);
            }
            else
            {
                Checks.NotNull(apiToken, nameof(apiToken));

                RestClient.AddDefaultHeader("Authorization", $"Bearer {apiToken}");
            }

            _user = new UserEndpoints(this);
            _zone = new ZoneEndpoints(this);
            _dns = new DnsEndpoints(this);
        }

        /// <summary>
        /// Logs in to set <see cref="CurrentUser"/> which is used for some endpoints.
        /// </summary>
        /// <returns></returns>
        public async Task LoginAsync()
        {
            if(IsLoggedIn)
            {
                throw new CloudflareException("Already logged in.");
            }

            LoggingIn = true;
            var response = await User.GetCurrentUserAsync();
            if (!response.Success)
            {
                throw new CloudflareException("You have provided an invalid api key or email.");
            }

            CurrentUser = response.Result;
            LoggingIn = false;
        }

        private void Verify()
        {
            if (CurrentUser == null && !LoggingIn)
            {
                throw new CloudflareException("You must be logged in to use the endpoints.");
            }
        }

    }
}
