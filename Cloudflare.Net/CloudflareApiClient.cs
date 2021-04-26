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
    public class CloudflareApiClient
    {

        internal const string DefaultBaseUrl = "https://api.cloudflare.com/client/v4/";
        internal RestClient Client { get; set; }

        internal bool LoggingIn { get; set; } = true;

        public CloudflareUser CurrentUser { get; set; }

        private UserEndpoints _user;
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
        public DnsEndpoints Dns
        {
            get
            {
                Verify();
                return _dns;
            }
            set => _dns = value;
        }

        public CloudflareApiClient(Action<CloudflareApiClientOptions> optionsAction)
        {
            var options = new CloudflareApiClientOptions();
            optionsAction.Invoke(options);

            Checks.NotNull(options.ApiKey, nameof(options.ApiKey));
            Checks.NotNull(options.Email, nameof(options.ApiKey));

            Client = new RestClient(options.BaseUrl ?? DefaultBaseUrl)
            {
                Proxy = new WebProxy("127.0.0.1", 8888)
            };
            Client.UseSerializer<NewtonsoftSerializer>();
            Client.AddDefaultHeader("X-Auth-Email", options.Email);
            Client.AddDefaultHeader("X-Auth-Key", options.ApiKey);

            _user = new UserEndpoints(this);
            _zone = new ZoneEndpoints(this);
            _dns = new DnsEndpoints(this);
        }

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
