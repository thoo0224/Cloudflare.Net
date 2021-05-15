using System.Diagnostics;

namespace Cloudflare.Net.Objects.Zone.Analytics
{
    [DebuggerDisplay("{" + nameof(Cached) + "}")]
    public class ZoneAnalyticsRequests
    {

        public int All { get; set; }
        public int Cached { get; set; }
        public int Uncached { get; set; }
        public ZoneAnalyticRequestContentType ContentType { get; set; }
        public ZoneAnalyticsRequestCountry Country { get; set; }
        public ZoneAnalyticsRequestSsl Ssl { get; set; }
        public ZoneAnalyticsRequestSslProtocols SslProtocols { get; set; }

    }
}
