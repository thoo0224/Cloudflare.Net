using Cloudflare.Net.Utils;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Cloudflare.Net
{
    public class CloudflareApiClientBuilder
    {

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
        /// Creates the api client.
        /// </summary>
        /// <returns></returns>
        public CloudflareApiClient Create()
        {
            return new CloudflareApiClient(_apiKey, _email);
        }

        /// <summary>
        /// Creates the api client and logs in.
        /// </summary>
        /// <returns></returns>
        public async Task<CloudflareApiClient> CreateAndLoginAsync()
        {
            var client = new CloudflareApiClient(_apiKey, _email);
            await client.LoginAsync();

            return client;
        }

    }
}
