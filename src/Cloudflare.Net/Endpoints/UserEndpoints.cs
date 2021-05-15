using Cloudflare.Net.Objects;
using Cloudflare.Net.Objects.User;

using RestSharp;

using System.Threading.Tasks;

namespace Cloudflare.Net.Endpoints
{
    public class UserEndpoints
    {

        private readonly CloudflareApiClient _client;

        public UserEndpoints(CloudflareApiClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets the current user of the api key provided.
        /// </summary>
        /// <returns>The cloudflare response with the current user as result.</returns>
        public async Task<CloudflareResponse<CloudflareUser>> GetCurrentUserAsync()
        {
            var request = new RestRequest("/user", Method.GET);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<CloudflareUser>>(request).ConfigureAwait(false);

            return response.Data;
        }

    }
}
