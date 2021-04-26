using Cloudflare.Net.Endpoints;
using Cloudflare.Net.Exceptions;
using Cloudflare.Net.Objects.User;
using Cloudflare.Net.Utils;

using RestSharp;

using System;
using System.Net;
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

        internal bool LoggingIn { get; set; } = true;

        /// <summary>
        /// The current logged in user
        /// </summary>
        public CloudflareUser CurrentUser { get; set; }

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
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="optionsAction">Options for the client; ApiKey, Email are required.</param>
        public CloudflareApiClient(Action<CloudflareApiClientOptions> optionsAction)
        {
            var options = new CloudflareApiClientOptions();
            optionsAction.Invoke(options);

            Checks.NotNull(options.ApiKey, nameof(options.ApiKey));
            Checks.NotNull(options.Email, nameof(options.ApiKey));

            Client = new RestClient(options.BaseUrl ?? DefaultBaseUrl);
            Client.UseSerializer<NewtonsoftSerializer>();
            Client.AddDefaultHeader("X-Auth-Email", options.Email);
            Client.AddDefaultHeader("X-Auth-Key", options.ApiKey);

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
            var response = await User.GetCurrentUserAsync().ConfigureAwait(false);
            if (!response.Success)
            {
                throw new CloudflareException("Invalid email or api key.");
            }

            CurrentUser = response.Result;
            LoggingIn = false;
            return;

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
