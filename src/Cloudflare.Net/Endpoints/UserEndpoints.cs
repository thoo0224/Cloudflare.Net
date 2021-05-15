using Cloudflare.Net.Objects;
using Cloudflare.Net.Objects.User;
using Cloudflare.Net.Objects.User.Token;

using RestSharp;

using System.Collections.Generic;
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

        /// <summary>
        /// Gets the permission of the token that is used.
        /// </summary>
        /// <returns></returns>
        public async Task<CloudflareResponse<List<TokenPermission>>> GetPermissionsAsync()
        {
            var request = new RestRequest("/user/tokens/permission_groups", Method.GET);
            var response = await ExecuteRequestAsync<List<TokenPermission>>(request);

            return response;
        }

    }
}
