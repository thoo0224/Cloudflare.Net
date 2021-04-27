using Cloudflare.Net.Objects;
using Cloudflare.Net.Objects.Zone;
using Cloudflare.Net.Utils;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Cloudflare.Net.Objects.Zone.Analytics;
using Cloudflare.Net.Options;

namespace Cloudflare.Net.Endpoints
{
    public class ZoneEndpoints
    {

        private readonly CloudflareApiClient _client;
        private readonly Regex _zoneNameRegex;

        public ZoneEndpoints(CloudflareApiClient client)
        {
            _client = client;
            _zoneNameRegex = new Regex(@"^([a-zA-Z0-9][\-a-zA-Z0-9]*\.)+[\-a-zA-Z0-9]{2,20}$");
        }

        /// <summary>
        /// Gets all the zones.
        /// </summary>
        /// <returns>The cloudflare response with all the zones as result.</returns>
        public async Task<CloudflareResponse<List<Zone>>> GetZonesAsync()
        {
            var request = new RestRequest("/zones", Method.GET);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<List<Zone>>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Creates a new zone.
        /// </summary>
        /// <param name="name">Name of the zone.</param>
        /// <param name="bodyAction">Options for the zone such as jump start and type.</param>
        /// <returns>Returns the zone which has been created.</returns>
        public async Task<CloudflareResponse<Zone>> CreateZoneAsync(
            [NotNull] string name, 
            Action<ZoneProperties> bodyAction = null)
        {
            Checks.NotNull(name, nameof(name));
            Checks.StringLength(name, nameof(name), 253);
            Checks.Regex(name, nameof(name), _zoneNameRegex, $"{name} is not a valid zone name!");

            var body = new ZoneProperties
            {
                Name = name
            };
            bodyAction?.Invoke(body);
            var request = new RestRequest("/zones", Method.POST);
            request.AddJsonBody(body);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<Zone>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Gets details about the zone.
        /// </summary>
        /// <param name="id">The zoneId of the zone.</param>
        /// <returns>The details about the zone</returns>
        public async Task<CloudflareResponse<Zone>> GetZoneAsync([NotNull] string id)
        {
            Checks.NotNull(id, nameof(id));

            var request = new RestRequest($"/zones/{id}");
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<Zone>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Deleted the zone.
        /// </summary>
        /// <param name="zone">The zone that will get deleted.</param>
        /// <returns>an object with the zoneId.</returns>
        public async Task<CloudflareResponse<object>> DeleteZoneAsync([NotNull] Zone zone)
        {
            return await DeleteZoneAsync(zone.Id);
        }

        /// <summary>
        /// Deleted the zone.
        /// </summary>
        /// <param name="zoneId">The zoneId of the zone that will get deleted.</param>
        /// <returns>an object with the zoneId.</returns>
        public async Task<CloudflareResponse<object>> DeleteZoneAsync([NotNull] string zoneId)
        {
            Checks.NotNull(zoneId, nameof(zoneId));

            var request = new RestRequest($"/zones/{zoneId}", Method.DELETE);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<object>>(request).ConfigureAwait(false);

            return response.Data;
        }


    }
}
