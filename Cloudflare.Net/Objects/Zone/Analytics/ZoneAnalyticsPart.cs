using System;

namespace Cloudflare.Net.Objects.Zone.Analytics
{
    public class ZoneAnalyticsPart
    {

        public DateTime Since {get;set;}
        public DateTime Until {get;set;}
        public ZoneAnalyticsRequests Requests { get; set; }

    }
}
