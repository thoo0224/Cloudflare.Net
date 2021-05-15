namespace Cloudflare.Net.Options
{
    public class DnsRecordOptions
    {

        public string Type { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public int Ttl { get; set; } = 1;
        public int Priority { get; set; } = 10;
        public bool Proxied { get; set; }

    }
}
