using System.Diagnostics;

namespace Cloudflare.Net.Objects.Zone
{
    [DebuggerDisplay("{" + nameof(Name) + "}")]
    public class ZonePlan
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public string Frequency { get; set; }
        public string LegacyId { get; set; }
        public bool IsSubscribed { get; set; }
        public bool CanSubscribe { get; set; }

    }
}
