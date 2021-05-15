using Cloudflare.Net.Endpoints;
using Cloudflare.Net.Exceptions;
using Cloudflare.Net.Objects.User;

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
        internal RestClient Client { get; set; }
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
        {
            var options = new CloudflareApiClientOptions();
            optionsAction.Invoke(options);
            
            Initialize(options.ApiKey, options.Email, null);
        }

        internal CloudflareApiClient(string apiKey, string email, Action<RestClient> clientAction)
        {
            Initialize(apiKey, email, clientAction);
        }

        internal void Initialize(string apiKey, string email, Action<RestClient> clientAction)
        {
            Client = new RestClient();
            clientAction?.Invoke(Client);

            Client.BaseUrl = new Uri(DefaultBaseUrl);
            Client.UseSerializer<NewtonsoftSerializer>();
            Client.AddDefaultHeader("X-Auth-Email", email);
            Client.AddDefaultHeader("X-Auth-Key", apiKey);

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
