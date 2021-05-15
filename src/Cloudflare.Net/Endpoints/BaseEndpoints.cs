using System.Threading.Tasks;
using Cloudflare.Net.Objects;
using RestSharp;

namespace Cloudflare.Net.Endpoints
{
    public class BaseEndpoints
    {

        internal CloudflareApiClient Client { get; set; }

        internal BaseEndpoints(CloudflareApiClient client)
        {
            Client = client;
        }

        internal async Task<CloudflareResponse<T>> ExecuteRequestAsync<T>(RestRequest request)
        {
            var response = await Client.RestClient.ExecuteAsync<CloudflareResponse<T>>(request)
                .ConfigureAwait(false);

            var cloudflareResponse = response.Data;
            cloudflareResponse.HttpStatusCode = response.StatusCode;

            return cloudflareResponse;
        }

        internal async Task<CloudflareResponse> ExecuteRequestAsync(RestRequest request)
        {
            return await ExecuteRequestAsync<object>(request);
        }

    }
}
