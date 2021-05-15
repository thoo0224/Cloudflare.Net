using Cloudflare.Net.Utils;

using RestSharp;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Cloudflare.Net
{
    public class CloudflareApiClientBuilder
    {

        private Action<RestClient> _clientAction;
        private string _apiKey;
        private string _email;

        /// <summary>
        /// Sets the api key of the cloudflare account.
        /// </summary>
        /// <param name="apiKey">Api key of the cloudflare account.</param>
        /// <returns></returns>
        public CloudflareApiClientBuilder WithApiKey([NotNull] string apiKey)
        {
            Checks.NotNull(apiKey, nameof(apiKey));

            _apiKey = apiKey;
            return this;
        }

        /// <summary>
        /// Sets the email of the cloudflare account.
        /// </summary>
        /// <param name="email">Email of the cloudflare account.</param>
        /// <returns></returns>
        public CloudflareApiClientBuilder WithEmail([NotNull] string email)
        {
            Checks.NotNull(email, nameof(email));

            _email = email;
            return this;
        }

        /// <summary>
        /// Modifies the rest client used to send all the requests.
        /// </summary>
        /// <param name="clientAction">The action to modify the rest client, this will be executed on the api client's initialization.</param>
        /// <returns></returns>
        public CloudflareApiClientBuilder ModifyRestClient(Action<RestClient> clientAction)
        {
            _clientAction = clientAction;
            return this;
        }

        /// <summary>
        /// Creates the api client.
        /// </summary>
        /// <returns></returns>
        public CloudflareApiClient Create()
        {
            return new CloudflareApiClient(_apiKey, _email, _clientAction);
        }

        /// <summary>
        /// Creates the api client and logs in.
        /// </summary>
        /// <returns></returns>
        public async Task<CloudflareApiClient> CreateAndLoginAsync()
        {
            var client = Create();
            await client.LoginAsync();

            return client;
        }

    }
}
