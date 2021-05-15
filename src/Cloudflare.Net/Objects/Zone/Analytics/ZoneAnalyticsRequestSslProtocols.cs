// ReSharper disable InconsistentNaming

using J = Newtonsoft.Json.JsonPropertyAttribute;

namespace Cloudflare.Net.Objects.Zone.Analytics
{
    public class ZoneAnalyticsRequestSslProtocols
    {

        [J("TLSv1")] public int TlsV1 { get; set; }
        [J("TLSv1.1")] public int TlsV1_1 { get; set; }
        [J("TLSv1.2")] public int TlsV1_2 { get; set; }
        [J("TLSv1.3")] public int TlsV1_3 { get; set; }
        public int None { get; set; }

    }
}
