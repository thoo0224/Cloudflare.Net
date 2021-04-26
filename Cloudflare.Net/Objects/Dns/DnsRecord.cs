using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cloudflare.Net.Objects.Dns
{
    public class DnsRecord
    {

        public string Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public DnsRecordType Type { get; set; }

        public string Name { get; set; }
        public string Content { get; set; }
        public bool Proxiable { get; set; }
        public bool Proxied { get; set; }
        public int Ttl { get; set; }
        public bool Locked { get; set; }
        public string ZoneId { get; set; }
        public string ZoneName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DnsRecordMeta Meta { get; set; }

    }
}
