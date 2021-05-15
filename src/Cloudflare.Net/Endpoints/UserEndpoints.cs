using Cloudflare.Net.Objects;
using Cloudflare.Net.Objects.User;

using RestSharp;

using System.Threading.Tasks;

namespace Cloudflare.Net.Endpoints
{
    public class UserEndpoints : BaseEndpoints
    {

        internal UserEndpoints(CloudflareApiClient client)
            : base(client) { }

        /// <summary>
        /// Gets the current user of the api key provided.
        /// </summary>
        /// <returns>The cloudflare response with the current user as result.</returns>
        public async Task<CloudflareResponse<CloudflareUser>> GetCurrentUserAsync()
        {
            var request = new RestRequest("/user", Method.GET);
            var response = await ExecuteRequestAsync<CloudflareUser>(request);

            return response;
        }

    }
}
