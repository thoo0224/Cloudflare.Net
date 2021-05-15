using Cloudflare.Net.Exceptions;
using Cloudflare.Net.Objects;
using Cloudflare.Net.Objects.Dns;
using Cloudflare.Net.Objects.Zone;
using Cloudflare.Net.Options;
using Cloudflare.Net.Utils;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace Cloudflare.Net.Endpoints
{
    public class DnsEndpoints
    {

        private readonly CloudflareApiClient _client;

        public DnsEndpoints(CloudflareApiClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Gets all the dns records of a zone
        /// </summary>
        /// <param name="zone">The zone</param>
        /// <returns>The cloudflare response with all the dns records of the zone as result.</returns>
        public async Task<CloudflareResponse<List<DnsRecord>>> GetDnsRecordsAsync([NotNull] Zone zone)
        {
            Checks.NotNull(zone, nameof(zone));

            return await GetDnsRecordsAsync(zone.Id);
        }

        /// <summary>
        /// Gets all the dns records of a zone
        /// </summary>
        /// <param name="id">The id of the zone</param>
        /// <returns>The cloudflare response with all the dns records of the zone as result.</returns>
        public async Task<CloudflareResponse<List<DnsRecord>>> GetDnsRecordsAsync([NotNull] string id)
        {
            Checks.NotNull(id, nameof(id));

            var request = new RestRequest($"/zones/{id}/dns_records");
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<List<DnsRecord>>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Create a new dns record for the zone.
        /// </summary>
        /// <param name="zone">The zone</param>
        /// <param name="name">The name for the dns record.</param>
        /// <param name="content">The content of the dns record.</param>
        /// <param name="type">The dns record type.</param>
        /// <param name="bodyAction">Options for the dns record such as ttl, proxied, priority</param>
        /// <returns>The dns record that has been created.</returns>
        public async Task<CloudflareResponse<DnsRecord>> CreateDnsRecordAsync(
            [NotNull] Zone zone,
            [NotNull] string name, 
            [NotNull] string content, 
            DnsRecordType type, 
            Action<DnsRecordOptions> bodyAction = null)
        {
            Checks.NotNull(zone, nameof(zone));

            return await CreateDnsRecordAsync(zone.Id, name, content, type, bodyAction);
        }

        /// <summary>
        /// Create a new dns record for the zone.
        /// </summary>
        /// <param name="id">The id of the zone.</param>
        /// <param name="name">The name for the dns record.</param>
        /// <param name="content">The content of the dns record.</param>
        /// <param name="type">The dns record type.</param>
        /// <param name="bodyAction">Options for the dns record such as ttl, proxied, priority</param>
        /// <returns>The dns record that has been created.</returns>
        public async Task<CloudflareResponse<DnsRecord>> CreateDnsRecordAsync(
            [NotNull] string id,
            [NotNull] string name, 
            [NotNull] string content, 
            DnsRecordType type, 
            Action<DnsRecordOptions> bodyAction = null)
        {
            var options = new DnsRecordOptions
            {
                Name = name,
                Content = content,
                Type = type.ToString()
            };
            bodyAction?.Invoke(options);

            Checks.NotNull(options.Name, nameof(options.Name));
            Checks.NotNull(id, nameof(id));
            Checks.NotNull(content, nameof(content));
            Checks.StringLength(options.Content, nameof(options.Content), 255);

            var request = new RestRequest($"/zones/{id}/dns_records", Method.POST);
            request.AddJsonBody(options);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<DnsRecord>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Get the information about a dns record by id in a zone.
        /// </summary>
        /// <param name="zone">The zone</param>
        /// <param name="dnsRecordId">The id of the dns record</param>
        /// <returns>Cloudflare response with the dns record as result.</returns>
        public async Task<CloudflareResponse<DnsRecord>> GetDnsRecordAsync(
            [NotNull] Zone zone, 
            [NotNull] string dnsRecordId)
        {
            Checks.NotNull(zone, nameof(zone));
            Checks.NotNull(dnsRecordId, nameof(dnsRecordId));

            var request = new RestRequest($"/zones/{zone.Id}/dns_records/{dnsRecordId}");
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<DnsRecord>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Updates the dns record
        /// </summary>
        /// <param name="zone">The zone</param>
        /// <param name="record">The record</param>
        /// <param name="optionsAction">Options for the dns record such as ttl, proxied, priority</param>
        /// <returns>The dns record that has been modified.</returns>
        public async Task<CloudflareResponse<DnsRecord>> UpdateDnsRecordAsync(
            [NotNull] Zone zone, 
            [NotNull] DnsRecord record,
            [NotNull] Action<DnsRecordOptions> optionsAction)
        {
            return await UpdateDnsRecordAsync(zone.Id, record, optionsAction);
        }

        /// <summary>
        /// Updates the dns record
        /// </summary>
        /// <param name="zoneId">The id of the zone</param>
        /// <param name="record">The record</param>
        /// <param name="optionsAction">Options for the dns record such as ttl, proxied, priority</param>
        /// <returns>The dns record that has been modified.</returns>
        public async Task<CloudflareResponse<DnsRecord>> UpdateDnsRecordAsync(
            [NotNull] string zoneId, 
            [NotNull] DnsRecord record,
            [NotNull] Action<DnsRecordOptions> optionsAction)
        {
            Checks.NotNull(zoneId, nameof(zoneId));
            Checks.NotNull(record, nameof(record));
            Checks.NotNull(optionsAction, nameof(optionsAction));

            var options = new DnsRecordOptions
            {
                Type = record.Type.ToString(),
                Content = record.Content,
                Name = record.Name,
                Proxied = record.Proxied,
                Ttl = record.Ttl
            };
            optionsAction.Invoke(options);

            var request = new RestRequest($"/zones/{zoneId}/dns_records/{record.Id}", Method.PATCH);
            request.AddJsonBody(options);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<DnsRecord>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Deletes the dns record
        /// </summary>
        /// <param name="zoneId">The id of the zone.</param>
        /// <param name="record">The dns record.</param>
        /// <returns>An object with the id of the dns record that has been deleted.</returns>

        public async Task<CloudflareResponse<object>> DeleteDnsRecordAsync(
            [NotNull] string zoneId,
            [NotNull] DnsRecord record)
        {
            return await DeleteDnsRecordAsync(zoneId, record.Id);
        }

        /// <summary>
        /// Deletes the dns record
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="recordId">The id of the dns record.</param>
        /// <returns>An object with the id of the dns record that has been deleted.</returns>
        public async Task<CloudflareResponse<object>> DeleteDnsRecordAsync(
            [NotNull] Zone zone,
            [NotNull] string recordId)
        {
            return await DeleteDnsRecordAsync(zone.Id, recordId);
        }

        /// <summary>
        /// Deletes the dns record
        /// </summary>
        /// <param name="zone">The zone.</param>
        /// <param name="record">The dns record.</param>
        /// <returns>An object with the id of the dns record that has been deleted.</returns>
        public async Task<CloudflareResponse<object>> DeleteDnsRecordAsync(
            [NotNull] Zone zone,
            [NotNull] DnsRecord record)
        {
            return await DeleteDnsRecordAsync(zone.Id, record.Id);
        }

        /// <summary>
        /// Deletes the dns record
        /// </summary>
        /// <param name="zoneId">The id of the zone.</param>
        /// <param name="recordId">The id of the dns record.</param>
        /// <returns>An object with the id of the dns record that has been deleted.</returns>
        public async Task<CloudflareResponse<object>> DeleteDnsRecordAsync(
            [NotNull] string zoneId,
            [NotNull] string recordId)
        {
            Checks.NotNull(zoneId, nameof(zoneId));
            Checks.NotNull(recordId, nameof(recordId));

            var request = new RestRequest($"/zones/{zoneId}/dns_records/{recordId}", Method.DELETE);
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<object>>(request).ConfigureAwait(false);

            return response.Data;
        }

        /// <summary>
        /// Imports your bind config.
        /// </summary>
        /// <param name="file">The bind config.</param>
        /// <param name="zone">The zone.</param>
        /// <param name="proxied">Proxied.</param>
        /// <returns>Returns the result of the import.</returns>
        public async Task<CloudflareResponse<DnsRecordImport>> ImportDnsRecordsAsync(
            [NotNull] string file,
            [NotNull] Zone zone,
            bool proxied = false)
        {
            return await ImportDnsRecordsAsync(file, zone.Id, proxied);
        }

        /// <summary>
        /// Imports your bind config.
        /// </summary>
        /// <param name="file">The bind config.</param>
        /// <param name="zoneId">The zone id.</param>
        /// <param name="proxied">Proxied.</param>
        /// <returns>Returns the result of the import.</returns>
        public async Task<CloudflareResponse<DnsRecordImport>> ImportDnsRecordsAsync(
            [NotNull] string file,
            [NotNull] string zoneId,
            bool proxied = false)
        {
            if (!File.Exists(file))
            {
                throw new CloudflareException($"The file {file} doesn't exist.");
            }

            var request = new RestRequest($"/zones/{zoneId}/dns_records/import", Method.POST);
            request.AddFile("file", file);
            request.AddParameter("proxied", proxied, ParameterType.RequestBody); 
            var response = await _client.Client.ExecuteAsync<CloudflareResponse<DnsRecordImport>>(request).ConfigureAwait(false);

            return response.Data;
        }

    }
}
